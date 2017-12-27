using DocDbChangeFeedConsole;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.ChangeFeedProcessor;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using XamarinChatWithCosmosOnly.AppSettings;
using XamarinChatWithCosmosOnly.Cosmos;
using XamarinChatWithCosmosOnly.Ioc;
using XamarinChatWithCosmosOnly.Models;

namespace XamarinChatWithCosmosOnly.Services
{
    class DocumentObserverChatClient : IChatClient
    {
        public event Action<Message> MessageReceived;

        protected readonly DocumentClientConfig config;
        protected readonly DocumentDb<Message> db;
        protected readonly ISettings settings;

        public DocumentObserverChatClient(DocumentClientConfig config, DocumentDb<Message> db)
        {
            this.config = config;
            this.db = db;

            this.settings = IocServices.Resolve<ISettings>();
        }

        public async Task Start()
        {
            try
            {
                await this.db.Create();

                var host = new ChangeFeedEventHost($"{this.config.LeaseCollection}-Host", new DocumentCollectionInfo
                {
                    Uri = new Uri(this.config.Url),
                    MasterKey = this.config.Key,
                    DatabaseName = this.config.Database,
                    CollectionName = this.config.Collection
                }, new DocumentCollectionInfo
                {
                    Uri = new Uri(this.config.Url),
                    MasterKey = this.config.Key,
                    DatabaseName = this.config.Database,
                    CollectionName = this.config.LeaseCollection
                }, new ChangeFeedOptions
                {
                    MaxItemCount = this.config.OldMessagesCount
                }, new ChangeFeedHostOptions
                {
                    CheckpointFrequency = new CheckpointFrequency
                    {
                        ProcessedDocumentCount = 1,
                    },
                    FeedPollDelay = this.config.PollDelay,
                    LeasePrefix = this.config.LeaseCollection
                });

                var factory = new DocumentChangeObserverFactory();

                var observer = factory.Observer;

                observer.DocumentReceived += Observer_DocumentReceived;

                await host.RegisterObserverFactoryAsync(factory);

                Debug.WriteLine("Running...");
            }
            catch(Exception ex)
            {
                Debug.Write(ex.Message);
                throw;
            }
        }

        private void Observer_DocumentReceived(Document doc)
        {
            var json = JsonConvert.SerializeObject(doc);
            var msg = JsonConvert.DeserializeObject<Message>(json);

            if (msg.UserId == this.settings.UserId)
                return;


            this.MessageReceived?.Invoke(msg);
        }

        public virtual Task SendMessage(Message msg)
        {
            return this.db.Insert(msg);
        }
    }
}

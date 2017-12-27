using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinChatWithCosmosOnly.AppSettings;
using XamarinChatWithCosmosOnly.Cosmos;
using XamarinChatWithCosmosOnly.Ioc;
using XamarinChatWithCosmosOnly.Mocks;
using XamarinChatWithCosmosOnly.Models;
using XamarinChatWithCosmosOnly.Services;

namespace XamarinChatWithCosmosOnly
{
    class Startup
    {
        public async Task Init(Application app)
        {
            IocServices.AddSingleton<ISettings>(new Settings());

            var config = new DocumentClientConfig
            {
                Collection = "TestCollection",
                Database = "TestDatabase",
                Url = "https://localhost:8081",

                //DocumentDb Emulator Token
                Key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",

                LeaseCollection = "TestCollectionLease",
                OldMessagesCount = 10,
                PollDelay = TimeSpan.FromSeconds(1)
            };

            var uri = new Uri(config.Url);
            var key = config.Key;
            var client = new DocumentClient(uri, key);

            var db = new DocumentDb<Message>(client, config.Database, config.Collection, config.LeaseCollection);

            var chatCleint = new MockObserverChatClient(config, db);

            await chatCleint.Start();

            IocServices.AddSingleton<IChatClient>(chatCleint);

            app.MainPage = new XamarinChatWithCosmosOnly.MainPage();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using System.Linq.Expressions;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents;
using System.Diagnostics;

namespace XamarinChatWithCosmosOnly.Cosmos
{
    public class DocumentDb<T> : IDocumentDb<T> where T: DocumentId
    {
        protected readonly DocumentClient Client;
        protected readonly string Database;
        protected readonly string Collection;
        protected readonly string LeaseCollection;
        protected readonly Uri CollectionUri;

        public DocumentDb(DocumentClient client, string database, string collection, string leaseCollection)
        {
            this.Client = client;
            this.Database = database;
            this.Collection = collection;
            this.LeaseCollection = leaseCollection;

            this.CollectionUri = UriFactory.CreateDocumentCollectionUri(this.Database, this.Collection);
        }

        public async Task Create()
        {
            try
            {
                var result = await this.Client.CreateDatabaseIfNotExistsAsync(new Database
                {
                    Id = Database
                });

                var databaseLink = result.Resource.SelfLink;

                await this.Client.CreateDocumentCollectionIfNotExistsAsync(databaseLink, new DocumentCollection
                {
                    Id = Collection
                });

                await this.Client.CreateDocumentCollectionIfNotExistsAsync(databaseLink, new DocumentCollection
                {
                    Id = LeaseCollection
                });
            }
            catch(Exception ex)
            {
                Debug.Write(ex.Message);
                throw;
            }
        }

        Uri DocumentUri(string id) => UriFactory.CreateDocumentUri(this.Database, this.Collection, id);

        public async Task<T> GetOne(Expression<Func<T, bool>> predicate)
        {
            var query = this.Query(new FeedOptions { MaxItemCount = 1 })
                            .Where(predicate)
                            .AsDocumentQuery();

            var result = await query.ExecuteNextAsync<T>();
            return result.FirstOrDefault();
        }
       

        public IOrderedQueryable<T> Query(FeedOptions options = null)
        {
            var query = Client.CreateDocumentQuery<T>(CollectionUri, options);
            return query;
        }

        public Task Insert(T document)
        {
            ThrowIfNotValid(document);
            return Client.CreateDocumentAsync(CollectionUri, document);
        }

        public Task Upsert(T document)
        {
            ThrowIfNotValid(document);
            return Client.UpsertDocumentAsync(CollectionUri, document);
        }

        void ThrowIfNotValid(T document)
        {
            //TODO
        }
    }
}

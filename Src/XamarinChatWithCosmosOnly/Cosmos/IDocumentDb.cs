using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XamarinChatWithCosmosOnly.Cosmos
{
    public interface IDocumentDb<T> where T: DocumentId
    {
        Task<T> GetOne(Expression<Func<T, bool>> predicate);
        IOrderedQueryable<T> Query(FeedOptions options = null);
        Task Insert(T document);
        Task Upsert(T document);
    }
}

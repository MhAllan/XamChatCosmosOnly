using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.ChangeFeedProcessor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocDbChangeFeedConsole
{
    class DocumentChangeObserver : IChangeFeedObserver
    {
        public event Action<Document> DocumentReceived;

        public Task CloseAsync(ChangeFeedObserverContext context, ChangeFeedObserverCloseReason reason)
        {
            return Task.CompletedTask;
        }

        public Task OpenAsync(ChangeFeedObserverContext context)
        {
            return Task.CompletedTask;
        }

        public Task ProcessChangesAsync(ChangeFeedObserverContext context, IReadOnlyList<Document> docs)
        {
            foreach(var doc in docs)
            {
                this.DocumentReceived(doc);
            }
            return Task.CompletedTask;
        }
    }
}

using Microsoft.Azure.Documents.ChangeFeedProcessor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocDbChangeFeedConsole
{
    class DocumentChangeObserverFactory : IChangeFeedObserverFactory
    {
        public DocumentChangeObserver Observer { get; set; } = new DocumentChangeObserver();

        public IChangeFeedObserver CreateObserver()
        {
            return Observer;
        }
    }
}

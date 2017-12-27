using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinChatWithCosmosOnly.Services
{
    class DocumentClientConfig
    {
        public string Url { get; set; }
        public string Key { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
        public string LeaseCollection { get; set; }
        public int OldMessagesCount { get; set; }
        public TimeSpan PollDelay { get; set; }
    }
}

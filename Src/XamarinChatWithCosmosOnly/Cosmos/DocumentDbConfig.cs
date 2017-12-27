using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinChatWithCosmosOnly.Cosmos
{
    public class DocumentDbConfig
    {
        public string Url { get; set; }
        public string Key { get; set; }
        public bool CreateSingleClient { get; set; } = true;
    }
}

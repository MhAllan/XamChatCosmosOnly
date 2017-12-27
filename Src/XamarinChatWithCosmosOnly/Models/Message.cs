using System;
using System.Collections.Generic;
using System.Text;
using XamarinChatWithCosmosOnly.Cosmos;

namespace XamarinChatWithCosmosOnly.Models
{
    public class Message : DocumentId
    {
        //User should be another model
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPicture { get; set; }
        public string Text { get; set; }
    }
}

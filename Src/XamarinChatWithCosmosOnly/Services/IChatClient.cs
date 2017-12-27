using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinChatWithCosmosOnly.Models;

namespace XamarinChatWithCosmosOnly.Services
{
    interface IChatClient
    {
        event Action<Message> MessageReceived;
        Task SendMessage(Message msg);
    }
}

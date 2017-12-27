using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinChatWithCosmosOnly.Cosmos;
using XamarinChatWithCosmosOnly.Models;
using XamarinChatWithCosmosOnly.Services;

namespace XamarinChatWithCosmosOnly.Mocks
{
    class MockObserverChatClient : DocumentObserverChatClient
    {
        public MockObserverChatClient(DocumentClientConfig config, DocumentDb<Message> db) : base(config, db)
        {
        }

        public override async Task SendMessage(Message msg)
        {
            await base.SendMessage(msg);
            await FakeReply();
        }

        async Task FakeReply()
        {
            await Task.Delay(1000);
            var msg = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Text = "This is mocked reply",
                UserId = Guid.NewGuid().ToString(),
                UserName = "Angelina Jolie",
                UserPicture = "https://c.tribune.com.pk/2017/04/1389986-angie-1492763306-905-640x480.gif"
            };

            await this.db.Insert(msg);
        }
    }
}

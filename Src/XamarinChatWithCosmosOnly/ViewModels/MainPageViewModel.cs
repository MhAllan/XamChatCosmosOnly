using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinChatWithCosmosOnly.AppSettings;
using XamarinChatWithCosmosOnly.Ioc;
using XamarinChatWithCosmosOnly.Models;
using XamarinChatWithCosmosOnly.Services;

namespace XamarinChatWithCosmosOnly.ViewModels
{
    class MainPageViewModel : ObservableObject
    {
        public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();


        string _MessageToSend;
        public string MessageToSend
        {
            get { return _MessageToSend; }
            set { SetProperty(ref _MessageToSend, value); }
        }

        readonly IChatClient chatClient;
        readonly ISettings settings;

        public MainPageViewModel()
        {
            this.settings = IocServices.Resolve<ISettings>();
            this.chatClient = IocServices.Resolve<IChatClient>();
            this.chatClient.MessageReceived += ChatClient_MessageReceived;
        }

        private void ChatClient_MessageReceived(Message msg)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.Messages.Add(msg);
            });
        }

        public virtual ICommand SendMessage => new Command(async () =>
        {
            var text = this.MessageToSend?.Trim();
            if (string.IsNullOrEmpty(text))
                return;

            this.MessageToSend = null;

            var msg = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Text = text,
                UserId = this.settings.UserId,
                UserName = this.settings.UserName,
                UserPicture = this.settings.UserPicture
            };

            //add it, so if send fail you have "Resend Button"
            this.Messages.Add(msg);

            //send it
            await this.chatClient.SendMessage(msg);
        });

    }
}

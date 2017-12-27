using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinChatWithCosmosOnly.AppSettings
{
    interface ISettings
    {
        //user should be new model
        string UserId { get; }
        string UserPicture { get; }
        string UserName { get; }
    }
}

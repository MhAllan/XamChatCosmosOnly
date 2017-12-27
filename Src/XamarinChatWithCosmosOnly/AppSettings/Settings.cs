using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinChatWithCosmosOnly.AppSettings
{
    class Settings : ISettings
    {
        public string UserId { get; } = "Current User Id Here";

        public string UserPicture { get; } = "https://www.famousbirthdays.com/faces/pitt-brad-image.jpg";

        public string UserName { get; } = "Brad Pitt";
    }
}

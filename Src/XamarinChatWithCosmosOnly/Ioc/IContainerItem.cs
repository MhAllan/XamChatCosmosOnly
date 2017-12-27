using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinChatWithCosmosOnly.Ioc
{
    public interface IContainerItem
    {
        object CreateInstance();
    }
}

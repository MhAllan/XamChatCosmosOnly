using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinChatWithCosmosOnly.Ioc
{
    public class ContainerItem<T> : IContainerItem
    {
        public T Instance { get; private set; }
        public readonly Func<T> Factory;
        public readonly IocMode Mode;

        public ContainerItem(IocMode mode, Func<T> factory)
        {
            this.Mode = mode;
            this.Factory = factory;
        }

        public ContainerItem(T instance)
        {
            this.Mode = IocMode.Singleton;
            this.Instance = instance;
        }

        public virtual object CreateInstance()
        {
            if(this.Mode == IocMode.Singleton)
            {
                if(this.Instance == null)
                {
                    lock(this)
                    {
                        if(this.Instance == null)
                        {
                            this.Instance = this.Factory();
                        }
                    }
                }
                return this.Instance;
            }
            else if(this.Mode == IocMode.Transient)
            {
                return this.Factory();
            }
            else
            {
                throw new NotSupportedException(this.Mode.ToString());
            }
        }
    }
}

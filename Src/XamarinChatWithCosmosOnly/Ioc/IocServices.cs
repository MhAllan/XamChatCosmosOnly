using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinChatWithCosmosOnly.Ioc
{
    public static class IocServices
    {
        static ConcurrentDictionary<Type, IContainerItem> container = new ConcurrentDictionary<Type, IContainerItem>();

        static void AddItem(Type intfType, IContainerItem item)
        {
            var added = container.TryAdd(intfType, item);
            if (!added)
            {
                throw new Exception($"Abstraction type {intfType.FullName} is already registered" +
                    $" in {nameof(IocServices)}");
            }
        }

        public static void AddSingleton<T>(T instance)
        {
            var item = new ContainerItem<T>(instance);
            AddItem(typeof(T), item);
        }

        public static void AddSingleton<T, TImpl>() where TImpl : T, new()
        {
            AddSingleton<T>(new TImpl());
        }

        public static void AddSingleton<T>(Func<T> creator)
        {
            var item = new ContainerItem<T>(IocMode.Singleton, creator);
            AddItem(typeof(T), item);
        }

        public static void AddTransient<T, TImpl>() where TImpl : T, new()
        {
            var item = new ContainerItem<TImpl>(IocMode.Transient, () => new TImpl());
            AddItem(typeof(T), item);
        }

        public static void AddTransient<T>(Func<T> creator)
        {
            var item = new ContainerItem<T>(IocMode.Transient, creator);
            AddItem(typeof(T), item);
        }

        public static T Resolve<T>()
        {
            try
            {
                var item = container[typeof(T)];
                return (T)item.CreateInstance();
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not resolve {typeof(T)}, see inner exception", ex);
            }
        }
    }
}

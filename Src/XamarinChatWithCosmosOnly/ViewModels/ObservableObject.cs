using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinChatWithCosmosOnly.ViewModels
{
    public class ObservableObject : INotifyPropertyChanged
    {
        protected bool SetProperty<T>(ref T prop, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(prop, value))
                return false;

            prop = value;
            onChanged?.Invoke();

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
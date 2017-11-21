using System;
using System.ComponentModel;

namespace WpfApp.EntitiesVM
{
    class MagicAttribute : Attribute { }

    [Magic]
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        protected virtual void RaisePropertyChanged(string propName)
        {
            var e = PropertyChanged;
            e?.Invoke(this, new PropertyChangedEventArgs(propName)); // некоторые из нас здесь используют Dispatcher, для безопасного взаимодействия с UI thread
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
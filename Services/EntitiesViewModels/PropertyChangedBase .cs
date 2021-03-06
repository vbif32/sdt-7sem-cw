﻿using System;
using System.ComponentModel;

namespace Services.EntitiesViewModels
{
    internal class MagicAttribute : Attribute
    {
    }

    [Magic]
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        public bool IsChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propName)
        {
            var e = PropertyChanged;
            e?.Invoke(this,
                new PropertyChangedEventArgs(
                    propName)); // некоторые из нас здесь используют Dispatcher, для безопасного взаимодействия с UI thread
            IsChanged = true;
        }
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;

// ReSharper disable InconsistentNaming

namespace EntitiesViewModels
{
    public class SettingsVM : PropertyChangedBase
    {
        public SettingsVM()
        {
        }

        public SettingsVM(Setting setting)
        {
            Setting = setting;
        }

        public Setting Setting { get; }

        public int Id
        {
            get => Setting.Id;
            set => Setting.Id = value;
        }

        public string Name
        {
            get => Setting.Name;
            set => Setting.Name = value;
        }

        public string Patronymic
        {
            get => Setting.Value;
            set => Setting.Value = value;
        }
    }
}
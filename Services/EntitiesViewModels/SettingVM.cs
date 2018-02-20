using Entities;

// ReSharper disable InconsistentNaming


namespace Services.EntitiesViewModels
{
    public class SettingVM : PropertyChangedBase
    {
        public SettingVM()
        {
        }

        public SettingVM(Setting setting)
        {
            Setting = setting;
        }

        public Setting Setting { get; }

        public int Id
        {
            get => Setting.Id;
            set => Setting.Id = value;
        }

        public Settings Name
        {
            get => Setting.Name;
            set => Setting.Name = value;
        }

        public string Value
        {
            get => Setting.Value;
            set => Setting.Value = value;
        }

        public int IntValue => int.Parse(Value);

        public float FloatValue => float.Parse(Value);
    }
}
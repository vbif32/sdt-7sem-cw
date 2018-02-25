using Entities;

// ReSharper disable InconsistentNaming


namespace Services.EntitiesViewModels
{
    public class SettingVM : VMBase<Setting>
    {
        public SettingVM()
        {
        }

        public SettingVM(Setting setting)
        {
            ModelObject = setting;
        }

        public int Id
        {
            get => ModelObject.Id;
            set => ModelObject.Id = value;
        }

        public Settings Name
        {
            get => ModelObject.Name;
            set => ModelObject.Name = value;
        }

        public string Value
        {
            get => ModelObject.Value;
            set => ModelObject.Value = value;
        }

        public int IntValue => int.Parse(Value);

        public float FloatValue => float.Parse(Value);
    }
}
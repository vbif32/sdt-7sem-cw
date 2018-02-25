using Entities;

namespace Services.EntitiesViewModels
{
    public class SpecialtyVM : VMBase<Specialty>
    {
        public SpecialtyVM()
        {
        }

        public SpecialtyVM(Specialty setting)
        {
            ModelObject = setting;
        }

        public int Id
        {
            get => ModelObject.Id;
            set => ModelObject.Id = value;
        }

        public string Name
        {
            get => ModelObject.Name;
            set => ModelObject.Name = value;
        }

        public string Value
        {
            get => ModelObject.Value;
            set => ModelObject.Value = value;
        }
    }
}
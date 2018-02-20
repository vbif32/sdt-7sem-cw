using Entities;

namespace Services.EntitiesViewModels
{
    public class SpecialtyVM : PropertyChangedBase
    {
        public SpecialtyVM()
        {
        }

        public SpecialtyVM(Specialty setting)
        {
            Specialty = setting;
        }

        public Specialty Specialty { get; }

        public int Id
        {
            get => Specialty.Id;
            set => Specialty.Id = value;
        }

        public string Name
        {
            get => Specialty.Name;
            set => Specialty.Name = value;
        }

        public string Value
        {
            get => Specialty.Value;
            set => Specialty.Value = value;
        }
    }
}
using Entities;
using LiteDB;

namespace Dao
{
    public class SpecialtyDao : DaoBase<Specialty>
    {
        public SpecialtyDao(LiteDbModel model) : base(model)
        {
            if (GetCollection().Count() == 0)
                InitialValues();
        }

        protected override LiteCollection<Specialty> GetCollection()
        {
            return _model.GetCollection<Specialty>(Specialty.CollectionName);
        }

        private void InitialValues()
        {
            Insert(new[]
            {
                new Specialty {Name = "ИВБ", Value = "09.03.01"},
                new Specialty {Name = "ИСБ", Value = "09.03.02"},
                new Specialty {Name = "ИНБ", Value = "09.03.03"},
                new Specialty {Name = "ИКБ", Value = "09.03.04"},
                new Specialty {Name = "ИАБ", Value = "15.03.04"},
                new Specialty {Name = "ИКМ", Value = "09.04.04"},
                new Specialty {Name = "ИВМ", Value = "09.04.01"},
                new Specialty {Name = "ИСМ", Value = "09.04.02"},
                new Specialty {Name = "ИАМ", Value = "15.04.04"},
                new Specialty {Name = "ИРБ", Value = "15.03.06"},
                new Specialty {Name = "ИУБ", Value = "27.03.04"},
                new Specialty {Name = "ИНМ", Value = "09.04.03"},
                new Specialty {Name = "ВТБ", Value = "0"},
                new Specialty {Name = "ИхА", Value = "0"}
            });
        }
    }
}
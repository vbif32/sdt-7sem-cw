using Entities;
using LiteDB;

namespace Dao
{
    public class SettingsDao : DaoBase<Setting>
    {
        public SettingsDao(LiteDbModel model) : base(model)
        {
            if (GetCollection().Count() == 0)
                InitialValues();
        }

        protected override LiteCollection<Setting> GetCollection()
        {
            return _model.GetCollection<Setting>(Setting.CollectionName);
        }

        private void InitialValues()
        {
            Insert(new[]
            {
                new Setting {Name = "FullDepartmentName", Value = "инструментального и прикладного программного обеспечения"},
                new Setting {Name = "ShortDepartmentName", Value = "ИППО"},
                new Setting {Name = "StartYear", Value = "2017"},
                new Setting {Name = "EndYear", Value = "2018"},
            });
        }
    }
}
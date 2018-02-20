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
                new Setting
                {
                    Name = Settings.FullDepartmentName,
                    Value = "инструментального и прикладного программного обеспечения"
                },
                new Setting {Name = Settings.ShortDepartmentName, Value = "ИППО"},
                new Setting {Name = Settings.StartYear, Value = "2017"},
                new Setting {Name = Settings.EndYear, Value = "2018"},
                new Setting {Name = Settings.SubgroupSize, Value = "15"},
                new Setting {Name = Settings.ZachMultiplayer, Value = "0.25"},
                new Setting {Name = Settings.ConsMultiplayer, Value = "0.07"},
                new Setting {Name = Settings.ExamMultiplayer, Value = "0.35"},
                new Setting {Name = Settings.CourseWorkMultiplayer, Value = "2"},
                new Setting {Name = Settings.CourseProjectMultiplayer, Value = "3"}
            });
        }
    }
}
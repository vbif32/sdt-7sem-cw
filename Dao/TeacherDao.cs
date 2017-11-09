using Entities;
using LiteDB;

namespace Dao
{
    public class TeacherDao : DaoBase<Преподаватель>
    {
        public TeacherDao(LiteDbModel model) : base(model)
        {
        }

        protected override LiteCollection<Преподаватель> GetCollection() => _model.GetCollection<Преподаватель>(Преподаватель.CollectionName).Include(x => x.Должность);
    }
}
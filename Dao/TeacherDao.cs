using Entities;
using LiteDB;

namespace Dao
{
    public class TeacherDao : DaoBase<Преподаватель>
    {
        public TeacherDao(LiteDatabase model) : base(model)
        {
        }

        protected override string CollectionName => Преподаватель.CollectionName;
    }
}
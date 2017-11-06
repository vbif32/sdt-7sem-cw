using Entities;
using LiteDB;

namespace Dao
{
    public class SubjectDao : DaoBase<Предмет>
    {
        public SubjectDao(LiteDatabase model) : base(model)
        {
        }

        protected override string CollectionName => Предмет.CollectionName;
    }
}
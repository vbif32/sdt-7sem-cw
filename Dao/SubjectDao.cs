using Entities;
using LiteDB;

namespace Dao
{
    public class SubjectDao : DaoBase<�������>
    {
        public SubjectDao(LiteDatabase model) : base(model)
        {
        }

        protected override string CollectionName => �������.CollectionName;
    }
}
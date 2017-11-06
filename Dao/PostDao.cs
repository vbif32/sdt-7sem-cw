using Entities;
using LiteDB;

namespace Dao
{
    public class PostDao : DaoBase<Должность>
    {
        public PostDao(LiteDatabase model) : base(model)
        {
        }

        protected override string CollectionName => Должность.CollectionName;
    }
}
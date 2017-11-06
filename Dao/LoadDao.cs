using Entities;
using LiteDB;

namespace Dao
{
    public class LoadDao : DaoBase<Нагрузка>
    {
        public LoadDao(LiteDatabase model) : base(model)
        {
        }

        protected override string CollectionName => Нагрузка.CollectionName;
    }
}
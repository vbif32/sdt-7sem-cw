using Entities;
using LiteDB;

namespace Dao
{
    public class EntryDao : DaoBase<Запись>
    {
        public EntryDao(LiteDatabase model) : base(model)
        {
        }

        protected override string CollectionName => Запись.CollectionName;
    }
}
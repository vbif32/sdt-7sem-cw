using Entities;
using LiteDB;

namespace Dao
{
    public class EntryDao : DaoBase<Запись>
    {
        public EntryDao(LiteDbModel model) : base(model)
        {
        }

        protected override LiteCollection<Запись> GetCollection()
            => _model.GetCollection<Запись>(Запись.CollectionName)
            .Include(x => x.Предмет)
            .Include(x => x.Преподаватель)
            .Include(x => x.Нагрузка);
    }
}
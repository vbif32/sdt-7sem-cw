using System.Collections.Generic;
using System.Linq;
using Entities;
using LiteDB;

namespace Dao
{
    public class EntryDao : DaoBase<Запись>
    {
        public EntryDao(LiteDbModel model) : base(model)
        {
        }

        public override void Insert(Запись entry)
        {
            if (entry.Предмет != null && entry.Преподаватель != null)
                base.Insert(entry);
        }

        public override void Insert(IEnumerable<Запись> entries)
        {
            base.Insert(entries.Where(entry => entry.Предмет != null && entry.Преподаватель != null));
        }

        protected override LiteCollection<Запись> GetCollection()
            => _model.GetCollection<Запись>(Запись.CollectionName)
            .Include(x => x.Предмет)
            .Include(x => x.Преподаватель)
            .Include(x => x.Нагрузка);
    }
}
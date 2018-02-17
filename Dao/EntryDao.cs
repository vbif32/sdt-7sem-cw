using System.Collections.Generic;
using System.Linq;
using Entities;
using LiteDB;

namespace Dao
{
    public class EntryDao : DaoBase<Entry>
    {
        public EntryDao(LiteDbModel model) : base(model)
        {
        }

        public override void Insert(Entry entry)
        {
            if (entry.Subject != null && entry.Teacher != null)
                base.Insert(entry);
        }

        public override void Insert(IEnumerable<Entry> entries)
        {
            base.Insert(entries.Where(entry => entry.Subject != null && entry.Teacher != null));
        }

        protected override LiteCollection<Entry> GetCollection()
        {
            return _model.GetCollection<Entry>(Entry.CollectionName)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                .Include(x => x.Load);
        }
    }
}
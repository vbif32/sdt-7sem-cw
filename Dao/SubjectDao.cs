using Entities;
using LiteDB;

namespace Dao
{
    public class SubjectDao : DaoBase<Предмет>
    {
        public SubjectDao(LiteDbModel model) : base(model)
        {
        }

        protected override LiteCollection<Предмет> GetCollection()
        {
            return _model.GetCollection<Предмет>(Предмет.CollectionName)
                .Include(x => x.ПлановаяНагрузка);
        }
    }
}
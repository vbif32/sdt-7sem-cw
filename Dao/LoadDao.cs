using Entities;
using LiteDB;

namespace Dao
{
    public class LoadDao : DaoBase<Load>
    {
        public LoadDao(LiteDbModel model) : base(model)
        {
        }

        protected override LiteCollection<Load> GetCollection()
        {
            return _model.GetCollection<Load>(Load.CollectionName);
        }
    }
}
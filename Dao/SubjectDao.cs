using Entities;
using LiteDB;

namespace Dao
{
    public class SubjectDao : DaoBase<�������>
    {
        public SubjectDao(LiteDbModel model) : base(model)
        {
        }

        protected override LiteCollection<�������> GetCollection()
            => _model.GetCollection<�������>(�������.CollectionName)
            .Include(x => x.����������������);
    }
}
using Entities;
using LiteDB;

namespace Dao
{
    public class SubjectDao : DaoBase<Subject>
    {
        public SubjectDao(LiteDbModel model) : base(model)
        {
        }

        protected override LiteCollection<Subject> GetCollection()
        {
            return _model.GetCollection<Subject>(Subject.CollectionName)
                .Include(x => x.PlannedLoad);
        }
    }
}
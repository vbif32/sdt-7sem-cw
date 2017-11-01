using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities;
using LiteDB;

namespace Dao
{
    public class PostDao
    {
        private readonly LiteDatabase _model;
        public PostDao(LiteDatabase model)
        {
            _model = model;
        }

        private LiteCollection<Должность> GetCollection() => _model.GetCollection<Должность>("posts");

        public void Insert(Должность post) => GetCollection().Insert(post);

        public IEnumerable<Должность> Find(Expression<Func<Должность,bool>> func) => GetCollection().Find(func);
        public Должность FindById(int id) => GetCollection().FindById(id);
        public bool Update(Должность post) => GetCollection().Update(post);
        public bool Delete(int id) => GetCollection().Delete(id);
    }
}

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
    public class LoadingDao
    {
        private readonly LiteDatabase _model;
        public LoadingDao(LiteDatabase model)
        {
            _model = model;
        }

        private LiteCollection<Нагрузка> GetCollection() => _model.GetCollection<Нагрузка>("loading");

        public void Insert(Нагрузка post) => GetCollection().Insert(post);

        public IEnumerable<Нагрузка> Find(Expression<Func<Нагрузка,bool>> func) => GetCollection().Find(func);
        public Нагрузка FindById(int id) => GetCollection().FindById(id);
        public bool Update(Нагрузка post) => GetCollection().Update(post);
        public bool Delete(int id) => GetCollection().Delete(id);
    }
}

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
    public class EntryDao
    {
        private readonly LiteDatabase _model;
        public EntryDao(LiteDatabase model)
        {
            _model = model;
        }

        private LiteCollection<Запись> GetCollection() => _model.GetCollection<Запись>("entries");

        public void Insert(Запись post) => GetCollection().Insert(post);

        public IEnumerable<Запись> Find(Expression<Func<Запись,bool>> func) => GetCollection().Find(func);
        public Запись FindById(int id) => GetCollection().FindById(id);
        public bool Update(Запись post) => GetCollection().Update(post);
        public bool Delete(int id) => GetCollection().Delete(id);
    }
}

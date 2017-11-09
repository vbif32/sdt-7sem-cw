using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entities;
using LiteDB;

namespace Dao
{
    public abstract class DaoBase<T>
    {
        protected readonly LiteDbModel _model;

        protected DaoBase(LiteDbModel model) => _model = model;

        protected abstract LiteCollection<T> GetCollection();

        public void Insert(T entry) => GetCollection().Insert(entry);
        public IEnumerable<T> Find(Expression<Func<T, bool>> func) => GetCollection().Find(func);
        public T FindById(int id) => GetCollection().FindById(id);
        public IEnumerable<T> FindAll() => GetCollection().FindAll();
        public bool Update(T o) => GetCollection().Update(o);
        public bool Delete(int id) => GetCollection().Delete(id);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual void Insert(T entry) => GetCollection().Insert(entry);
        public virtual void Insert(IEnumerable<T> entry) => GetCollection().Insert(entry);
        public IEnumerable<T> Find(Expression<Func<T, bool>> func) => GetCollection().Find(func);
        public T FindById(int id) => GetCollection().FindById(id);
        public IEnumerable<T> FindAll() => GetCollection().FindAll();
        public bool Update(T o) => GetCollection().Update(o);
        public void Update(IEnumerable<T> entry) => GetCollection().Update(entry);
        public bool Delete(int id) => GetCollection().Delete(id);
        public int Delete(T o)
        {
            return GetCollection().Delete(t => Equals(t, o));
        }
        public int Delete(IEnumerable<T> o)
        {
            return GetCollection().Delete(t => o.Contains(t));
        }

        public bool DeleteAll() => GetCollection().Delete(true);
    }
}
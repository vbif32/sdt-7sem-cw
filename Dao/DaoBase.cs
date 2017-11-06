using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiteDB;

namespace Dao
{
    public abstract class DaoBase<T>
    {
        protected readonly LiteDatabase _model;

        protected DaoBase(LiteDatabase model)
        {
            _model = model;
        }

        protected abstract string CollectionName { get; }

        protected LiteCollection<T> GetCollection()
        {
            return _model.GetCollection<T>(CollectionName);
        }

        public void Insert(T entry)
        {
            GetCollection().Insert(entry);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> func)
        {
            return GetCollection().Find(func);
        }

        public T FindById(int id)
        {
            return GetCollection().FindById(id);
        }

        public IEnumerable<T> FindAll()
        {
            return GetCollection().FindAll();
        }

        public bool Update(T o)
        {
            return GetCollection().Update(o);
        }

        public bool Delete(int id)
        {
            return GetCollection().Delete(id);
        }
    }
}
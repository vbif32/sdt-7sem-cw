using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entities;
using LiteDB;

namespace Dao
{
    public class PostDao : DaoBase<Должность>
    {
        public PostDao(LiteDbModel model) : base(model)
        {
        }

        protected override LiteCollection<Должность> GetCollection() => _model.GetCollection<Должность>(Должность.CollectionName);

    }
}
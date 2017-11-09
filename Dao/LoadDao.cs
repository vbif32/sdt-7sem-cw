﻿using Entities;
using LiteDB;

namespace Dao
{
    public class LoadDao : DaoBase<Нагрузка>
    {
        public LoadDao(LiteDbModel model) : base(model)
        {
        }

        protected override LiteCollection<Нагрузка> GetCollection() => _model.GetCollection<Нагрузка>(Нагрузка.CollectionName);
    }
}
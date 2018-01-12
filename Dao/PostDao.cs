using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entities;
using LiteDB;

namespace Dao
{
    public sealed class PostDao : DaoBase<Должность>
    {
        public PostDao(LiteDbModel model) : base(model)
        {
            if (GetCollection().Count() == 0)
                InitialValues();
        }

        protected override LiteCollection<Должность> GetCollection() => _model.GetCollection<Должность>(Должность.CollectionName);

        private void InitialValues()
        {
            Insert(new []
            {
                new Должность{ПолноеНазвание = "Ассистент", Название = "асс.", Часы = 880},
                new Должность{ПолноеНазвание = "Старший преподаватель", Название = "ст. п.", Часы = 880},
                new Должность{ПолноеНазвание = "Доцент", Название = "доц.", Часы = 810},
                new Должность{ПолноеНазвание = "Профессор", Название = "проф.", Часы = 720},
                new Должность{ПолноеНазвание = "Заведующий кафедрой", Название = "зав. каф.", Часы = 640},
            });
        }
    }
}
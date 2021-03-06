﻿using Entities;
using LiteDB;

namespace Dao
{
    public class TeacherDao : DaoBase<Teacher>
    {
        public TeacherDao(LiteDbModel model) : base(model)
        {
            if (GetCollection().Count() == 0)
                InitialValues();
        }

        protected override LiteCollection<Teacher> GetCollection()
        {
            return _model.GetCollection<Teacher>(Teacher.CollectionName).Include(x => x.Post);
        }


        private void InitialValues()
        {
            Insert(new[]
            {
                new Teacher
                {
                    Фамилия = "Богорадникова",
                    Имя = "Алиса",
                    Отчество = "Викторовна",
                    Ставка = 1,
                    Post = new Post()
                },
                new Teacher
                {
                    Фамилия = "Болбаков",
                    Имя = "Роман",
                    Отчество = "Геннадьевич",
                    Ставка = 1.4F,
                    Post = new Post()
                },
                new Teacher
                {
                    Фамилия = "Мордвинов",
                    Имя = "Вадим",
                    Отчество = "Александрович",
                    Ставка = 1.5F,
                    Post = new Post()
                },
                new Teacher
                {
                    Фамилия = "Чумак",
                    Имя = "Борис",
                    Отчество = "Борисович",
                    Ставка = 1.2F,
                    Post = new Post()
                },
                new Teacher
                {
                    Фамилия = "Шмелева",
                    Имя = "Дарья",
                    Отчество = "Викторовна",
                    Ставка = 1.1F,
                    Post = new Post()
                }
            });
        }
    }
}
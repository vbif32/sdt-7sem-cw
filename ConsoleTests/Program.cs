﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converting;
using LiteDB;
using Entities;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var ф101 = new Ф101
            {
                Курс = 1,
                Семестр = 2,
                НомерПотока = 39,
                ИмяПотока = "ИКБО-01-02-03-04-05-13-14-15-16-17",
                Дисциплина = "Объектно-ориентированное программирование (семестр 1)",
                Кафедра = 7,
                ЛекцииВНеделю = 1,
                ЛабораторныеВНеделю = 2,
                ПрактическиеВНеделю = 2,
                Экзамен = true,
                Зачет = false,
                Кп = false,
                Кр = true,
                НедельТо = 16,
                Трудоемкость = 5,
                ТрудоемкостьГода = 63,
                Численность = "253 (170)",
                ЧислоГрупп = 9
            };
            var expected = new Нагрузка
            {
                Лекции = 16,
                Лабораторные = 576,
                Практические = 288,
                Зачеты = 0,
                Консультации = 2,
                Экзамены = 88.55f,
                КурсовоеПроектирование = 0,
                ПрактикиНир = 506,
                Вкр = 0,
                ГэкГак = 0,
                ДопЗащ = 0,
                Рма = 0,
                Рмп = 0
            };

            var actual = Ф101ToПредмет.CalcLoad(ф101);
        }

        static void LiteDbBasicExample()
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Get customer collection
                var customers = db.GetCollection<Customer>("customers");

                // Create your new customer instance
                var customer = new Customer
                {
                    Name = "John Doe",
                    Phones = new string[] { "8000-0000", "9000-0000" },
                    IsActive = true
                };

                // Insert new customer document (Id will be auto-incremented)
                customers.Insert(customer);

                // Update a document inside a collection
                customer.Name = "Joana Doe";

                customers.Update(customer);

                // Index document using a document property
                customers.EnsureIndex(x => x.Name);

                // Use Linq to query documents
                var results = customers.Find(x => x.Name.StartsWith("Jo"));
            }
        }
    }
}

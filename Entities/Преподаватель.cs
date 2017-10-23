using System;
using System.Collections.Generic;

namespace Entities
{
    public class Преподаватель
    {

            public Guid Id { get; set; }
            public string ПолноеИмя => $"{Имя} {Отчество} {Фамилия}";
            public string Имя { get; set; }
            public string Отчество { get; set; }
            public string Фамилия { get; set; }
            public float Ставка { get; set; }

            public List<Предмет> Предметы { get; set; }
    }
}
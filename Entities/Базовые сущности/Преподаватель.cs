using System.Collections.Generic;
using System.Linq;
using LiteDB;

// ReSharper disable InconsistentNaming

namespace Entities
{
    public class Преподаватель
    {
        public const string CollectionName = "teachers";

        public int Id { get; set; }

        public string ПолноеИОФ => $"{Имя} {Отчество} {Фамилия}";
        public string ПолноеФИО => $"{Фамилия} {Имя} {Отчество}";
        public string ФамилияИИнициалы => $"{Фамилия} {Имя.First()}. {Отчество.First()}.";

        public string Имя { get; set; }
        public string Отчество { get; set; }
        public string Фамилия { get; set; }
        public Должность Должность { get; set; }
        public float Ставка { get; set; }

        public string УченаяСтепеньПолная { get; set; }
        public string УченаяСтепень { get; set; }

        public МестоРаботы МестоРаботы { get; set; }
        public List<Предмет> Предметы { get; set; }
    }
}
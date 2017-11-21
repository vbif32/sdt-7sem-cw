using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class Нагрузка
    {
        public const string CollectionName = "workload";

        public Нагрузка(){}

        public Нагрузка(float лекции, float лабораторные, float практические, float зачеты, float консультации,
            float экзамены, float нир, float курсовоеПроектирование, float вкр, float гэк, float гак,
            float рма, float рмп)
        {
            Лекции = лекции;
            Лабораторные = лабораторные;
            Практические = практические;
            Зачеты = зачеты;
            Консультации = консультации;
            Экзамены = экзамены;
            Нир = нир;
            КурсовоеПроектирование = курсовоеПроектирование;
            Вкр = вкр;
            Гэк = гэк;
            Гак = гак;
            Рма = рма;
            Рмп = рмп;
        }

        public int Id { get; set; }
        public float Лекции { get; set; }
        public float Лабораторные { get; set; }
        public float Практические { get; set; }
        public float Зачеты { get; set; }
        public float Консультации { get; set; }
        public float Экзамены { get; set; }
        public float Нир { get; set; }
        public float КурсовоеПроектирование { get; set; }
        public float Вкр { get; set; }
        public float Гэк { get; set; }
        public float Гак { get; set; }
        /// <summary>
        ///     Руководство магитрами аспирантами
        /// </summary>
        public float Рма { get; set; }
        /// <summary>
        ///     Руководство магистерскими программами
        /// </summary>
        public float Рмп { get; set; }

        public string ToStringDebug()
        {
            return
                $"{Лекции,3} {Лабораторные,3} {Практические,3} {Зачеты,3} {Консультации,3} {Экзамены,3} {Нир,3} {КурсовоеПроектирование,3} {Вкр,3} {Гэк,3} {Гак,3} {Рма,3}";
        }

        public bool Equals(Нагрузка нагрузка)
        {
            return Math.Abs(Лекции - нагрузка.Лекции) < 0.1 &&
                   Math.Abs(Лабораторные - нагрузка.Лабораторные) < 0.1 &&
                   Math.Abs(Практические - нагрузка.Практические) < 0.1 &&
                   Math.Abs(Зачеты - нагрузка.Зачеты) < 0.1 &&
                   Math.Abs(Консультации - нагрузка.Консультации) < 0.1 &&
                   Math.Abs(Экзамены - нагрузка.Экзамены) < 0.1 &&
                   Math.Abs(Нир - нагрузка.Нир) < 0.1 &&
                   Math.Abs(КурсовоеПроектирование - нагрузка.КурсовоеПроектирование) < 0.1 &&
                   Math.Abs(Вкр - нагрузка.Вкр) < 0.1 &&
                   Math.Abs(Гэк - нагрузка.Гэк) < 0.1 &&
                   Math.Abs(Гак - нагрузка.Гак) < 0.1 &&
                   Math.Abs(Рма - нагрузка.Рма) < 0.1;
        }
    }
}
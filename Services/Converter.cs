using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;
using Services.Converters;

namespace Services
{
    public static class Converter
    {
        public static IEnumerable<Предмет> Convert(IEnumerable<F101Entry> form) => form.Select(Convert).ToList();
        public static Предмет Convert(F101Entry entry)
        {
            return new Предмет
            {
                Название = entry.Дисциплина,
                Кафедра = entry.Кафедра,
                Специальность = entry.Специальность,
                ФормаОбучения = entry.ФормаОбучения,
                Курс = entry.Курс,
                Семестр = entry.Семестр,
                НедельВСем = entry.НедельВСем,
                Поток = entry.ИмяПотока,
                ЧислоГрупп = entry.ЧислоГрупп,
                ЧислоПодгрупп = entry.ЧислоПодгрупп,
                ГруппВПотоке = entry.ЧислоГрупп,
                Численность = entry.Численность,
                Трудоемкость = entry.Трудоемкость,
                ТрудоемкостьГода = entry.ТрудоемкостьГода,
                Лк = entry.Лк,
                Лаб = entry.Лаб,
                Пр = entry.Пр,
                Экзамен = entry.Экзамен,
                Зачет = entry.Зачет,
                КурсовоеПроектирование = entry.КурсовоеПроектирование,
                ПлановаяНагрузка = F101ToSubject.CalcLoad(entry)
            };
        }
        public static Нагрузка Convert(IReadOnlyCollection<Запись> записи)
        {
            if (записи == null)
                return new Нагрузка(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            return new Нагрузка(
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Лекции),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Лабораторные),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Практические),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Зачеты),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Консультации),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Экзамены),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Нир),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.КурсовоеПроектирование),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Вкр),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Гэк),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Гак),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Рма),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Рмп)
            );
        }

        public static List<F101Entry> F101FromExcel(string path) => ExcelToF101.LoadF101(path);
    }
}
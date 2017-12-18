﻿using System.Collections.Generic;
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
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Lectures),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Laboratory),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Practical),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Test),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Consultations),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Exams),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Nir),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.CourseDesigning),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Vkr),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Hack),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Hak),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Rma),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Нагрузка.Rmp)
            );
        }

        public static List<F101Entry> F101FromExcel(string path) => ExcelToF101.LoadF101(path);
    }
}
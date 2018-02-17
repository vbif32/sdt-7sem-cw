using System.Collections.Generic;
using System.Linq;
using Entities;
using EntitiesViewModels;
using Services.Converters;
using Services.Export;
using Services.Import;

namespace Services
{
    public static class Converter
    {
        public static IEnumerable<Subject> Convert(IEnumerable<F101Entry> form)
        {
            return form.Select(Convert).ToList();
        }

        public static Subject Convert(F101Entry entry)
        {
            return new Subject
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
                PlannedLoad = F101ToSubject.CalcLoad(entry)
            };
        }

        public static Load Convert(IReadOnlyCollection<Entry> записи)
        {
            if (записи == null)
                return new Load(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            return new Load(
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Lectures),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Laboratory),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Practical),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Test),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Consultations),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Exams),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Nir),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.CourseDesigning),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Vkr),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Hack),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Hak),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Rma),
                (float) записи.Aggregate(0.0, (s, a) => s + a.Load.Rmp)
            );
        }

        public static List<F101Entry> F101FromExcel(string path)
        {
            return ExcelToF101.LoadF101(path);
        }

        public static bool ToF106(string path)
        {
            return ExportToF106.Export(path);
        }

        public static bool ToF115(string path)
        {
            return ExportToF115.Export(path);
        }

        public static void ToF16(EntitiesVMRegistry entitiesVMRegistry, string path)
        {
            ExportToF16.Export(entitiesVMRegistry, path);
        }
    }
}
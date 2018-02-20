using System;
using Services;

namespace Entities
{
    public class F101Entry
    {
        private КурсовоеПроектирование _курсовоеПроектирование = КурсовоеПроектирование.Ошибка;
        private float _лабораторныеВНеделю = -1;
        private float _лекцииВНеделю = -1;

        private int _недельВСем = -1;
        private int _полнаяЧисленность = -1;
        private float _практическиеВНеделю = -1;

        private string _специальность;
        private ФормаОбучения _формаОбучения = ФормаОбучения.Ошибка;
        private int _числоПодгрупп = -1;


        public F101Entry(int курс, int семестр, int номерПотока, string имяПотока, string дисциплина, int кафедра,
            string лк, string лаб, string пр, bool экзамен, bool зачет, bool кп,
            bool кр, string недельТо, float трудоемкость, int трудоемкостьГода, string численность, int числоГрупп)
        {
            Курс = курс;
            Семестр = семестр;
            НомерПотока = номерПотока;
            ИмяПотока = имяПотока;
            Дисциплина = дисциплина;
            Кафедра = кафедра;
            Лк = лк;
            Лаб = лаб;
            Пр = пр;
            Экзамен = экзамен;
            Зачет = зачет;
            Кп = кп;
            Кр = кр;
            НедельТо = недельТо;
            Трудоемкость = трудоемкость;
            ТрудоемкостьГода = трудоемкостьГода;
            Численность = численность;
            ЧислоГрупп = числоГрупп;
        }

        public int Курс { get; set; }
        public int Семестр { get; set; }
        public int НомерПотока { get; set; }
        public string ИмяПотока { get; set; }
        public string Дисциплина { get; set; }
        public int Кафедра { get; set; }
        public string Лк { get; set; }
        public string Лаб { get; set; }
        public string Пр { get; set; }
        public bool Экзамен { get; set; }
        public bool Зачет { get; set; }
        public bool Кп { get; set; }
        public bool Кр { get; set; }
        public string НедельТо { get; set; }
        public float Трудоемкость { get; set; }
        public int ТрудоемкостьГода { get; set; }
        public string Численность { get; set; }
        public int ЧислоГрупп { get; set; }

        public КурсовоеПроектирование КурсовоеПроектирование => _курсовоеПроектирование != КурсовоеПроектирование.Ошибка
            ? _курсовоеПроектирование
            : (_курсовоеПроектирование = GetCourseDesign());

        public ФормаОбучения ФормаОбучения => _формаОбучения != ФормаОбучения.Ошибка
            ? _формаОбучения
            : (_формаОбучения = GetLearningForm());

        public string Специальность =>
            _специальность ?? (_специальность = ContextSingleton.Instance.GetSpecialty(ИмяПотока));

        public int ПолнаяЧисленность =>
            _полнаяЧисленность != -1 ? _полнаяЧисленность : (_полнаяЧисленность = GetStrength());

        public int ЧислоПодгрупп => _числоПодгрупп != -1 ? _числоПодгрупп : (_числоПодгрупп = GetSubgroupCount());
        public int НедельВСем => _недельВСем != -1 ? _недельВСем : (_недельВСем = GetWorkWeekCount());
        public float ЛекцииВНеделю => _лекцииВНеделю != -1 ? _лекцииВНеделю : (_лекцииВНеделю = GetLessonPerWeek(Лк));

        public float ЛабораторныеВНеделю => _лабораторныеВНеделю != -1
            ? _лабораторныеВНеделю
            : (_лабораторныеВНеделю = GetLessonPerWeek(Лаб));

        public float ПрактическиеВНеделю => _практическиеВНеделю != -1
            ? _практическиеВНеделю
            : (_практическиеВНеделю = GetLessonPerWeek(Пр));

        private КурсовоеПроектирование GetCourseDesign()
        {
            if (Кр && Кп) return КурсовоеПроектирование.Ошибка;
            if (Кр) return КурсовоеПроектирование.КурсоваяРабота;
            if (Кп) return КурсовоеПроектирование.КурсовойПроект;
            return КурсовоеПроектирование.Нет;
        }

        private ФормаОбучения GetLearningForm()
        {
            switch (ИмяПотока.Substring(3, 1))
            {
                case "О":
                    return ФормаОбучения.Очная;
                case "В":
                    return ФормаОбучения.Вечерняя;
                case "З":
                    return ФормаОбучения.Заочная;
                default:
                    return ФормаОбучения.Ошибка; // TODO: узнать какие еще варианты
            }
        }

        private string GetSpecialty()
        {
            switch (ИмяПотока.Substring(0, 3))
            {
                case "ИКБ":
                    return "09.03.04";
                case "ИВБ":
                    return "09.03.01";
                case "ИСБ":
                    return "09.03.02";
                case "ИНБ":
                    return "09.03.03";
                case "ИАБ":
                    return "15.03.04";
                case "ИКМ":
                    return "09.04.04";
                case "ИВМ":
                    return "09.04.01";
                case "ИСМ":
                    return "09.04.02";
                case "ИАМ":
                    return "15.04.04";
                case "ИРБ":
                    return "15.03.06";
                case "ИУБ":
                    return "27.03.04";
                case "ИНМ":
                    return "09.04.03";
                case "ВТБ":
                    return "0"; // TODO: узнать направление 
                case "ИхА":
                    return "0"; // TODO: узнать направление 
                default:
                    return "0";
            }
        }

        private int GetStrength()
        {
            return int.Parse(Численность.Split('(')[0]);
        }

        private int GetSubgroupCount()
        {
            if (GetStrength() / ЧислоГрупп <= ContextSingleton.Instance.SubgroupSize)
                return ЧислоГрупп;
            return ЧислоГрупп * 2;
        }

        private int GetWorkWeekCount()
        {
            return int.TryParse(НедельТо, out var result) ? result : 0;
        }

        private float GetLessonPerWeek(string lessonPerWeek)
        {
            return float.TryParse(lessonPerWeek?.Replace("н", "").Replace(",", "."), out var res) ? res : 0;
        }


        public string ToStringDebug()
        {
            return
                $"{Курс} {Семестр} {НомерПотока} {ИмяПотока} {ФормаОбучения} {Специальность} {Дисциплина} {Кафедра} {Лк} {Лаб} {Пр} {Экзамен} {Зачет} {Кп} {Кр} {КурсовоеПроектирование} {НедельТо} {НедельВСем} {Трудоемкость} {ТрудоемкостьГода} {Численность} {ПолнаяЧисленность} {ЧислоГрупп} {ЧислоПодгрупп}";
        }

        public bool Equals(F101Entry entry)
        {
            return Курс == entry.Курс &&
                   Семестр == entry.Семестр &&
                   НомерПотока == entry.НомерПотока &&
                   ИмяПотока == entry.ИмяПотока &&
                   НомерПотока == entry.НомерПотока &&
                   Кафедра == entry.Кафедра &&
                   Лк == entry.Лк &&
                   Лаб == entry.Лаб &&
                   Пр == entry.Пр &&
                   Экзамен == entry.Экзамен &&
                   Зачет == entry.Зачет &&
                   Кр == entry.Кр &&
                   НедельТо == entry.НедельТо &&
                   Math.Abs(Трудоемкость - entry.Трудоемкость) < 0.1 &&
                   ТрудоемкостьГода == entry.ТрудоемкостьГода &&
                   Численность == entry.Численность &&
                   ЧислоГрупп == entry.ЧислоГрупп;
        }
    }
}
namespace Entities
{
    public class Ф101
    {
        public const int SubgroupStrigth = 15;
        public const float ZachMultiplayer = 0.25f;
        public const float ExamMultiplayer = 0.35f;

        public int Курс { get; set; }
        public int Семестр { get; set; }
        public int НомерПотока { get; set; }
        public string ИмяПотока { get; set; }
        public string Дисциплина { get; set; }
        public int Кафедра { get; set; }
        public dynamic ЛекцииВНеделю { get; set; }
        public dynamic ЛабораторныеВНеделю { get; set; }
        public dynamic ПрактическиеВНеделю { get; set; }
        // public float Срс { get; set; } // depricated столбец
        public bool Экзамен { get; set; }
        public bool Зачет { get; set; }
        public bool Кп { get; set; }
        public bool Кр { get; set; }
        // public float Unknown { get; set; } // пустой столбец
        public dynamic НедельТо { get; set; }
        public float Трудоемкость { get; set; }
        public int ТрудоемкостьГода { get; set; }
        public string Численность { get; set; }
        public int ЧислоГрупп { get; set; }

        public string GetSpecialty()
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

        public ФормаОбучения GetLearningForm()
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

        public int GetStrength() => int.Parse(Численность.Split()[0]);

        public int GetSubgroupCount()
        {
            if (GetStrength() / ЧислоГрупп <= SubgroupStrigth)
                return ЧислоГрупп;
            return ЧислоГрупп * 2;
        }

        public КурсовоеПроектирование GetCourseDesign()
        {
            if (Кр) return КурсовоеПроектирование.КурсоваяРабота;
            if (Кп) return КурсовоеПроектирование.КурсовойПроект;
            return КурсовоеПроектирование.Нет;
        }
    }
}
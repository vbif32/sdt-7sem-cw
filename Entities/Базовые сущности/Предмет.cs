namespace Entities
{
    public class Предмет
    {
        public const string CollectionName = "subjects";

        public int Id { get; set; }
        public string Название { get; set; }
        public int Кафедра { get; set; }
        public string Специальность { get; set; } // Вычисляется из группы
        public ФормаОбучения ФормаОбучения { get; set; } // Вычисляется из группы
        public int Курс { get; set; }
        public int Семестр { get; set; }
        public int НедельВСем { get; set; }
        public int Поток { get; set; }
        public int ЧислоГрупп { get; set; }
        public int ЧислоПодгрупп { get; set; } // Вычисляется из числа групп в потоке
        public int ГруппВПотоке { get; set; } // Дублирует Число групп??
        public string Численность { get; set; }
        public float Трудоемкость { get; set; }
        public float ТрудоемкостьГода { get; set; }
        public string Лк { get; set; }
        public string Лаб { get; set; }
        public string Пр { get; set; }
        public bool Экзамен { get; set; }
        public bool Зачет { get; set; }
        public КурсовоеПроектирование КурсовоеПроектирование { get; set; }
        public Нагрузка ПлановаяНагрузка { get; set; } // Сложные вычисления
    }
}
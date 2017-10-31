namespace Entities
{
    public class Ф101
    {
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
    }
}
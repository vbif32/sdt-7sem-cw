namespace Entities
{
    public class Ф115
    {
        public int Id { get; set; }
        public int Поток { get; set; }
        public string Специальность { get; set; }
        public string Дисциплина { get; set; }
        public Кафедра Кафедра { get; set; }
        public int Курс { get; set; }
        public int Семестр { get; set; }
        public string Численность { get; set; }
        public int ЧислоГрупп { get; set; }
        public int ЧислоПодгрупп { get; set; } // Вычисляется из числа групп в потоке
        public Load PlannedLoad { get; set; } // Сложные вычисления
    }
}
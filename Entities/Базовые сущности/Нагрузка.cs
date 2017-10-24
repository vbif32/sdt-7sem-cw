namespace Entities
{
    public class Нагрузка
    {
        public int Id { get; set; }
        public float Лекции { get; set; }
        public float Лабораторные { get; set; }
        public float Практические { get; set; }
        public float Зачеты { get; set; }
        public float Консультации { get; set; }
        public float Экзамены { get; set; }
        public float КурсовоеПроектирование { get; set; }
        public float ПрактикиНир { get; set; }
        public float Вкр { get; set; }
        public float ГэкГак { get; set; }
        public float ДопЗащ { get; set; }
        public float Рма { get; set; }
        public float Рмп { get; set; }

        public float Сумма => Лекции + Лабораторные + Практические + Зачеты + Консультации + Экзамены +
                              КурсовоеПроектирование + ПрактикиНир + Вкр + ГэкГак + ДопЗащ + Рма + Рмп;
    }
}
namespace Entities
{
    internal class Поток
    {
        public int Номер { get; set; }
        public int Группа { get; set; }
        public int Бюджет { get; set; }
        public int Договор { get; set; }
        public int Всего => Бюджет + Договор;
        public string Численность { get; set; }
        public int КоличествоГрупп { get; set; }
        public Кафедра Кафедра { get; set; }
        public string Направление { get; set; }
    }
}
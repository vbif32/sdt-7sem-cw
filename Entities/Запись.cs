namespace Entities
{
    internal class Запись
    {
        public int Id { get; set; }
        public Предмет Предмет { get; set; }
        public Преподаватель Преподаватель { get; set; }
        public Нагрузка Нагрузка { get; set; }
    }
}
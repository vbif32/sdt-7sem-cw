using LiteDB;

namespace Entities
{
    public class Запись
    {
        public const string CollectionName = "entries";

        public Запись()
        {
            Нагрузка  = new Нагрузка();
        }

        public int Id { get; set; }
        public Предмет Предмет { get; set; }
        public Преподаватель Преподаватель { get; set; }
        public Нагрузка Нагрузка { get; set; }
    }
}
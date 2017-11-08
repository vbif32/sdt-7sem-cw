using LiteDB;

namespace Entities
{
    public class Запись
    {
        public const string CollectionName = "entries";

        public int Id { get; set; }
        [BsonRef(Предмет.CollectionName)]
        public Предмет Предмет { get; set; }
        [BsonRef(Преподаватель.CollectionName)]
        public Преподаватель Преподаватель { get; set; }
        [BsonRef(Нагрузка.CollectionName)]
        public Нагрузка ФактическаяНагрузка { get; set; }
    }
}
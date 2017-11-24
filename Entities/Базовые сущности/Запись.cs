namespace Entities
{
    public class Запись
    {
        public const string CollectionName = "entries";

        public Запись()
        {
            Нагрузка = new Нагрузка();
        }

        public int Id { get; set; }
        public Предмет Предмет { get; set; }
        public Преподаватель Преподаватель { get; set; }
        public Нагрузка Нагрузка { get; set; }

        protected bool Equals(Запись other)
        {
            return Id == other.Id && Equals(Предмет, other.Предмет) && Equals(Преподаватель, other.Преподаватель) &&
                   Equals(Нагрузка, other.Нагрузка);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Запись) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Предмет != null ? Предмет.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Преподаватель != null ? Преподаватель.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Нагрузка != null ? Нагрузка.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
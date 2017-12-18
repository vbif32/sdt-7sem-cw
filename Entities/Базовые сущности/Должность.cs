namespace Entities
{
    public class Должность
    {

        public const string CollectionName = "posts";
        public int Id { get; set; }
        public string Название { get; set; }
        public string ПолноеНазвание { get; set; }
        public int Часы { get; set; }

        protected bool Equals(Должность other)
        {
            return Id == other.Id && string.Equals(Название, other.Название) &&
                   string.Equals(ПолноеНазвание, other.ПолноеНазвание) && Часы == other.Часы;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Должность) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Название != null ? Название.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ПолноеНазвание != null ? ПолноеНазвание.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Часы;
                return hashCode;
            }
        }
    }
}
namespace Entities
{
    public class Entry
    {
        public const string CollectionName = "entries";

        public Entry()
        {
            Load = new Load();
        }

        public int Id { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public Load Load { get; set; }

        protected bool Equals(Entry other)
        {
            return Id == other.Id && Equals(Subject, other.Subject) && Equals(Teacher, other.Teacher) &&
                   Equals(Load, other.Load);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Subject != null ? Subject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Teacher != null ? Teacher.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Load != null ? Load.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
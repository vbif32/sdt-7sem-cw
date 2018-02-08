namespace Entities
{
    public class Post
    {
        public const string CollectionName = "posts";
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public int Hours { get; set; }

        protected bool Equals(Post other)
        {
            return Id == other.Id && string.Equals(ShortName, other.ShortName) &&
                   string.Equals(LongName, other.LongName) && Hours == other.Hours;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Post) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (ShortName != null ? ShortName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LongName != null ? LongName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Hours;
                return hashCode;
            }
        }
    }
}
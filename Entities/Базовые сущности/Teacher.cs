namespace Entities
{
    public class Teacher
    {
        public const string CollectionName = "teachers";

        public Teacher()
        {
            Фамилия = Имя = Отчество = УченаяСтепень = УченаяСтепеньПолная = string.Empty;
        }

        public Teacher(Post post)
        {
            Post = post;
        }

        public int Id { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }

        public Post Post { get; set; }
        public float Ставка { get; set; }

        public string УченаяСтепеньПолная { get; set; }
        public string УченаяСтепень { get; set; }

        public МестоРаботы МестоРаботы { get; set; }

        protected bool Equals(Teacher other)
        {
            return Id == other.Id && string.Equals(Фамилия, other.Фамилия) && string.Equals(Имя, other.Имя) &&
                   string.Equals(Отчество, other.Отчество) && Equals(Post, other.Post) &&
                   Ставка.Equals(other.Ставка) && string.Equals(УченаяСтепеньПолная, other.УченаяСтепеньПолная) &&
                   string.Equals(УченаяСтепень, other.УченаяСтепень) && МестоРаботы == other.МестоРаботы;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Teacher) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Фамилия != null ? Фамилия.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Имя != null ? Имя.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Отчество != null ? Отчество.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Post != null ? Post.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Ставка.GetHashCode();
                hashCode = (hashCode * 397) ^ (УченаяСтепеньПолная != null ? УченаяСтепеньПолная.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (УченаяСтепень != null ? УченаяСтепень.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) МестоРаботы;
                return hashCode;
            }
        }
    }
}
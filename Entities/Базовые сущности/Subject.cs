namespace Entities
{
    public class Subject
    {
        public const string CollectionName = "subjects";

        public Subject()
        {
            PlannedLoad = new Load();
        }

        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int Department { get; set; }
        public string Specialty { get; set; }
        public ФормаОбучения EducationForm { get; set; }
        public int Course { get; set; }
        public int Semester { get; set; }
        public int НедельВСем { get; set; }
        public string Flow { get; set; }
        public int GroupsCount { get; set; }
        public int SubgroupsCount { get; set; }
        public int ГруппВПотоке { get; set; } // Дублирует Число групп??
        public string Численность { get; set; }
        public float Трудоемкость { get; set; }
        public float ТрудоемкостьГода { get; set; }
        public string Lectures { get; set; }
        public string Laboratory { get; set; }
        public string Practical { get; set; }
        public bool Exam { get; set; }
        public bool Test { get; set; }
        public КурсовоеПроектирование CourseDesigning { get; set; }
        public Load PlannedLoad { get; set; }

        protected bool Equals(Subject other)
        {
            return Id == other.Id && string.Equals(Name, other.Name) && Department == other.Department &&
                   string.Equals(Specialty, other.Specialty) && EducationForm == other.EducationForm &&
                   Course == other.Course && Semester == other.Semester && НедельВСем == other.НедельВСем &&
                   string.Equals(Flow, other.Flow) && GroupsCount == other.GroupsCount &&
                   SubgroupsCount == other.SubgroupsCount && ГруппВПотоке == other.ГруппВПотоке &&
                   string.Equals(Численность, other.Численность) && Трудоемкость.Equals(other.Трудоемкость) &&
                   ТрудоемкостьГода.Equals(other.ТрудоемкостьГода) && string.Equals(Lectures, other.Lectures) &&
                   string.Equals(Laboratory, other.Laboratory) && string.Equals(Practical, other.Practical) &&
                   Exam == other.Exam &&
                   Test == other.Test && CourseDesigning == other.CourseDesigning &&
                   Equals(PlannedLoad, other.PlannedLoad);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Subject) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Department;
                hashCode = (hashCode * 397) ^ (Specialty != null ? Specialty.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) EducationForm;
                hashCode = (hashCode * 397) ^ Course;
                hashCode = (hashCode * 397) ^ Semester;
                hashCode = (hashCode * 397) ^ НедельВСем;
                hashCode = (hashCode * 397) ^ (Flow != null ? Flow.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ GroupsCount;
                hashCode = (hashCode * 397) ^ SubgroupsCount;
                hashCode = (hashCode * 397) ^ ГруппВПотоке;
                hashCode = (hashCode * 397) ^ (Численность != null ? Численность.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Трудоемкость.GetHashCode();
                hashCode = (hashCode * 397) ^ ТрудоемкостьГода.GetHashCode();
                hashCode = (hashCode * 397) ^ (Lectures != null ? Lectures.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Laboratory != null ? Laboratory.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Practical != null ? Practical.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Exam.GetHashCode();
                hashCode = (hashCode * 397) ^ Test.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) CourseDesigning;
                hashCode = (hashCode * 397) ^ (PlannedLoad != null ? PlannedLoad.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
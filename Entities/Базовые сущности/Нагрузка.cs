namespace Entities
{
    public class Нагрузка
    {
        public const string CollectionName = "workload";

        public Нагрузка()
        {
        }


        public Нагрузка(float lectures, float laboratory, float practical, float test, float consultations,
            float exams, float nir, float courseDesigning, float vkr, float hack, float hak,
            float rma, float rmp)
        {
            Lectures = lectures;
            Laboratory = laboratory;
            Practical = practical;
            Test = test;
            Consultations = consultations;
            Exams = exams;
            Nir = nir;
            CourseDesigning = courseDesigning;
            Vkr = vkr;
            Hack = hack;
            Hak = hak;
            Rma = rma;
            Rmp = rmp;
        }

        public int Id { get; set; }
        /// <summary>
        /// Лекции
        /// </summary>
        public float Lectures { get; set; }
        /// <summary>
        /// Лабораторные
        /// </summary>
        public float Laboratory { get; set; }
        /// <summary>
        /// Практические
        /// </summary>
        public float Practical { get; set; }
        /// <summary>
        /// Зачеты
        /// </summary>
        public float Test { get; set; }
        /// <summary>
        /// Консультации
        /// </summary>
        public float Consultations { get; set; }
        /// <summary>
        /// Экзамен
        /// </summary>
        public float Exams { get; set; }
        /// <summary>
        /// НИР
        /// </summary>
        public float Nir { get; set; }
        /// <summary>
        /// Курсовое Проектирование
        /// </summary>
        public float CourseDesigning { get; set; }
        /// <summary>
        /// ВКР
        /// </summary>
        public float Vkr { get; set; }
        /// <summary>
        /// ГЭК
        /// </summary>
        public float Hack { get; set; }
        /// <summary>
        /// ГАК
        /// </summary>
        public float Hak { get; set; }
        /// <summary>
        ///     Руководство магитрами аспирантами
        /// </summary>
        public float Rma { get; set; }
        /// <summary>
        ///     Руководство магистерскими программами
        /// </summary>
        public float Rmp { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Нагрузка) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ Lectures.GetHashCode();
                hashCode = (hashCode * 397) ^ Laboratory.GetHashCode();
                hashCode = (hashCode * 397) ^ Practical.GetHashCode();
                hashCode = (hashCode * 397) ^ Test.GetHashCode();
                hashCode = (hashCode * 397) ^ Consultations.GetHashCode();
                hashCode = (hashCode * 397) ^ Exams.GetHashCode();
                hashCode = (hashCode * 397) ^ Nir.GetHashCode();
                hashCode = (hashCode * 397) ^ CourseDesigning.GetHashCode();
                hashCode = (hashCode * 397) ^ Vkr.GetHashCode();
                hashCode = (hashCode * 397) ^ Hack.GetHashCode();
                hashCode = (hashCode * 397) ^ Hak.GetHashCode();
                hashCode = (hashCode * 397) ^ Rma.GetHashCode();
                hashCode = (hashCode * 397) ^ Rmp.GetHashCode();
                return hashCode;
            }
        }

        public string ToStringDebug()
        {
            return
                $"{Lectures,3} {Laboratory,3} {Practical,3} {Test,3} {Consultations,3} {Exams,3} {Nir,3} {CourseDesigning,3} {Vkr,3} {Hack,3} {Hak,3} {Rma,3}";
        }

        protected bool Equals(Нагрузка нагрузка)
        {
            return Id == нагрузка.Id && Lectures.Equals(нагрузка.Lectures) && Laboratory.Equals(нагрузка.Laboratory) &&
                   Practical.Equals(нагрузка.Practical) && Test.Equals(нагрузка.Test) &&
                   Consultations.Equals(нагрузка.Consultations) && Exams.Equals(нагрузка.Exams) &&
                   Nir.Equals(нагрузка.Nir) && CourseDesigning.Equals(нагрузка.CourseDesigning) &&
                   Vkr.Equals(нагрузка.Vkr) && Hack.Equals(нагрузка.Hack) && Hak.Equals(нагрузка.Hak) &&
                   Rma.Equals(нагрузка.Rma) && Rmp.Equals(нагрузка.Rmp);
        }
    }
}
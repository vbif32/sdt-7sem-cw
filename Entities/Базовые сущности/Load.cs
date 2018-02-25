namespace Entities
{
    public class Load
    {
        public const string CollectionName = "workload";

        public Load()
        {
        }


        public Load(float lectures, float laboratory, float practical, float test, float consultations,
            float exams, float nir, float courseDesigning, float vkr, float gek, float gak,
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
            Gek = gek;
            Gak = gak;
            Rma = rma;
            Rmp = rmp;
        }

        public int Id { get; set; }

        /// <summary>
        ///     Лекции
        /// </summary>
        public float Lectures { get; set; }

        /// <summary>
        ///     Лабораторные
        /// </summary>
        public float Laboratory { get; set; }

        /// <summary>
        ///     Практические
        /// </summary>
        public float Practical { get; set; }

        /// <summary>
        ///     Зачеты
        /// </summary>
        public float Test { get; set; }

        /// <summary>
        ///     Консультации
        /// </summary>
        public float Consultations { get; set; }

        /// <summary>
        ///     Exam
        /// </summary>
        public float Exams { get; set; }

        /// <summary>
        ///     НИР
        /// </summary>
        public float Nir { get; set; }

        /// <summary>
        ///     Курсовое Проектирование
        /// </summary>
        public float CourseDesigning { get; set; }

        /// <summary>
        ///     ВКР
        /// </summary>
        public float Vkr { get; set; }

        /// <summary>
        ///     ГЭК
        /// </summary>
        public float Gek { get; set; }

        /// <summary>
        ///     ГАК
        /// </summary>
        public float Gak { get; set; }

        /// <summary>
        ///     Руководство магистрами и аспирантами
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
            return Equals((Load) obj);
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
                hashCode = (hashCode * 397) ^ Gek.GetHashCode();
                hashCode = (hashCode * 397) ^ Gak.GetHashCode();
                hashCode = (hashCode * 397) ^ Rma.GetHashCode();
                hashCode = (hashCode * 397) ^ Rmp.GetHashCode();
                return hashCode;
            }
        }

        public string ToStringDebug()
        {
            return
                $"{Lectures,3} {Laboratory,3} {Practical,3} {Test,3} {Consultations,3} {Exams,3} {Nir,3} {CourseDesigning,3} {Vkr,3} {Gek,3} {Gak,3} {Rma,3}";
        }

        protected bool Equals(Load load)
        {
            return Id == load.Id && Lectures.Equals(load.Lectures) && Laboratory.Equals(load.Laboratory) &&
                   Practical.Equals(load.Practical) && Test.Equals(load.Test) &&
                   Consultations.Equals(load.Consultations) && Exams.Equals(load.Exams) &&
                   Nir.Equals(load.Nir) && CourseDesigning.Equals(load.CourseDesigning) &&
                   Vkr.Equals(load.Vkr) && Gek.Equals(load.Gek) && Gak.Equals(load.Gak) &&
                   Rma.Equals(load.Rma) && Rmp.Equals(load.Rmp);
        }
    }
}
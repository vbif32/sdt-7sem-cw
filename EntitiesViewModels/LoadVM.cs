using Entities;

namespace WpfApp.EntitiesVM
{
    public class LoadVM : PropertyChangedBase
    {
        public LoadVM()
        {
            Load = new Нагрузка();
        }

        public LoadVM(float лекции, float лабораторные, float практические, float зачеты, float консультации,
            float экзамены, float нир, float курсовоеПроектирование, float вкр, float гэк, float гак,
            float рма, float рмп)
        {
            Load = new Нагрузка(лекции, лабораторные, практические, зачеты, консультации,
                экзамены, нир, курсовоеПроектирование, вкр, гэк, гак,
                рма, рмп);
        }

        public LoadVM(Нагрузка load)
        {
            Load = load;
        }

        public Нагрузка Load { get; }

        public int Id
        {
            get => Load.Id;
            set => Load.Id = value;
        }

        public float Lectures
        {
            get => Load.Lectures;
            set => Load.Lectures = value;
        }

        public float Laboratory
        {
            get => Load.Laboratory;
            set => Load.Laboratory = value;
        }

        public float Practical
        {
            get => Load.Practical;
            set => Load.Practical = value;
        }

        public float Test
        {
            get => Load.Test;
            set => Load.Test = value;
        }

        public float Consultations
        {
            get => Load.Consultations;
            set => Load.Consultations = value;
        }

        public float Exams
        {
            get => Load.Exams;
            set => Load.Exams = value;
        }

        public float Nir
        {
            get => Load.Nir;
            set => Load.Nir = value;
        }

        public float CourseDesigning
        {
            get => Load.CourseDesigning;
            set => Load.CourseDesigning = value;
        }

        public float Vkr
        {
            get => Load.Vkr;
            set => Load.Vkr = value;
        }

        public float Hack
        {
            get => Load.Hack;
            set => Load.Hack = value;
        }

        public float Hak
        {
            get => Load.Hak;
            set => Load.Hak = value;
        }

        /// <summary>
        ///     Руководство магитрами аспирантами
        /// </summary>
        public float Rma
        {
            get => Load.Rma;
            set => Load.Rma = value;
        }

        /// <summary>
        ///     руководство магистерскими программами
        /// </summary>
        public float Rmp
        {
            get => Load.Rmp;
            set => Load.Rmp = value;
        }

        public float Amount => Lectures + Laboratory + Practical + Test + Consultations + Exams +
                               Nir + CourseDesigning + Vkr + Hack + Hak + Rma + Rmp;
    }
}
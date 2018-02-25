using Entities;

namespace Services.EntitiesViewModels
{
    public class LoadVM : VMBase<Load>
    {
        public LoadVM()
        {
            ModelObject = new Load();
        }

        public LoadVM(float лекции, float лабораторные, float практические, float зачеты, float консультации,
            float экзамены, float нир, float курсовоеПроектирование, float вкр, float гэк, float гак,
            float рма, float рмп)
        {
            ModelObject = new Load(лекции, лабораторные, практические, зачеты, консультации,
                экзамены, нир, курсовоеПроектирование, вкр, гэк, гак,
                рма, рмп);
        }

        public LoadVM(Load load)
        {
            ModelObject = load;
        }

        public int Id
        {
            get => ModelObject.Id;
            set => ModelObject.Id = value;
        }

        public float Lectures
        {
            get => ModelObject.Lectures;
            set => ModelObject.Lectures = value;
        }

        public float Laboratory
        {
            get => ModelObject.Laboratory;
            set => ModelObject.Laboratory = value;
        }

        public float Practical
        {
            get => ModelObject.Practical;
            set => ModelObject.Practical = value;
        }

        public float Test
        {
            get => ModelObject.Test;
            set => ModelObject.Test = value;
        }

        public float Consultations
        {
            get => ModelObject.Consultations;
            set => ModelObject.Consultations = value;
        }

        public float Exams
        {
            get => ModelObject.Exams;
            set => ModelObject.Exams = value;
        }

        public float Nir
        {
            get => ModelObject.Nir;
            set => ModelObject.Nir = value;
        }

        public float CourseDesigning
        {
            get => ModelObject.CourseDesigning;
            set => ModelObject.CourseDesigning = value;
        }

        public float Vkr
        {
            get => ModelObject.Vkr;
            set => ModelObject.Vkr = value;
        }

        public float Hack
        {
            get => ModelObject.Gek;
            set => ModelObject.Gek = value;
        }

        public float Hak
        {
            get => ModelObject.Gak;
            set => ModelObject.Gak = value;
        }

        /// <summary>
        ///     Руководство магитрами аспирантами
        /// </summary>
        public float Rma
        {
            get => ModelObject.Rma;
            set => ModelObject.Rma = value;
        }

        /// <summary>
        ///     руководство магистерскими программами
        /// </summary>
        public float Rmp
        {
            get => ModelObject.Rmp;
            set => ModelObject.Rmp = value;
        }

        public float Amount => Lectures + Laboratory + Practical + Test + Consultations + Exams +
                               Nir + CourseDesigning + Vkr + Hack + Hak + Rma + Rmp;
    }
}
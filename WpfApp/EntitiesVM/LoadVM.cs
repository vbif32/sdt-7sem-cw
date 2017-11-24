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
            Lectures = лекции;
            Laboratory = лабораторные;
            Practical = практические;
            Test = зачеты;
            Consultations = консультации;
            Exams = экзамены;
            Nir = нир;
            CourseDesigning = курсовоеПроектирование;
            Vkr = вкр;
            Hack = гэк;
            Hak = гак;
            Rma = рма;
            Rmp = рмп;
        }

        public LoadVM(Нагрузка load) => Load = load;

        public Нагрузка Load { get; }

        public int Id { get =>Load.Id; set => Load.Id = value; }
        public float Lectures { get => Load.Лекции; set => Load.Лекции = value; }
        public float Laboratory { get => Load.Лабораторные; set => Load.Лабораторные = value; }
        public float Practical { get => Load.Практические; set => Load.Практические = value; }
        public float Test { get => Load.Зачеты; set => Load.Зачеты = value; }
        public float Consultations { get => Load.Консультации; set => Load.Консультации = value; }
        public float Exams { get => Load.Экзамены; set => Load.Экзамены = value; }
        public float Nir { get => Load.Нир; set => Load.Нир = value; }
        public float CourseDesigning { get => Load.КурсовоеПроектирование; set => Load.КурсовоеПроектирование = value; }
        public float Vkr { get => Load.Вкр; set => Load.Вкр = value; }
        public float Hack { get => Load.Гэк; set => Load.Гэк = value; }
        public float Hak { get => Load.Гак; set => Load.Гак = value; }
        /// <summary>
        ///     Руководство магитрами аспирантами
        /// </summary>
        public float Rma { get => Load.Рма; set => Load.Рма = value; }

        /// <summary>
        ///     руководство магистерскими программами
        /// </summary>
        public float Rmp { get => Load.Рмп; set => Load.Рмп = value; }

        public float Amount => Lectures + Laboratory + Practical + Test + Consultations + Exams +
                              Nir + CourseDesigning + Vkr + Hack + Hak + Rma + Rmp;

    }
}
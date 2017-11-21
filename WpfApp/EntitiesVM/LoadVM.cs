using Entities;

namespace WpfApp.EntitiesVM
{
    public class LoadVM : PropertyChangedBase
    {
        public LoadVM()
        {
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

        public LoadVM(Нагрузка load) => _load = load;

        private readonly Нагрузка _load;

        public int Id { get =>_load.Id; set => _load.Id = value; }
        public float Lectures { get => _load.Лекции; set => _load.Лекции = value; }
        public float Laboratory { get => _load.Лабораторные; set => _load.Лабораторные = value; }
        public float Practical { get => _load.Практические; set => _load.Практические = value; }
        public float Test { get => _load.Зачеты; set => _load.Зачеты = value; }
        public float Consultations { get => _load.Консультации; set => _load.Консультации = value; }
        public float Exams { get => _load.Экзамены; set => _load.Экзамены = value; }
        public float Nir { get => _load.Нир; set => _load.Нир = value; }
        public float CourseDesigning { get => _load.КурсовоеПроектирование; set => _load.КурсовоеПроектирование = value; }
        public float Vkr { get => _load.Вкр; set => _load.Вкр = value; }
        public float Hack { get => _load.Гэк; set => _load.Гэк = value; }
        public float Hak { get => _load.Гак; set => _load.Гак = value; }
        /// <summary>
        ///     Руководство магитрами аспирантами
        /// </summary>
        public float Rma { get => _load.Рма; set => _load.Рма = value; }

        /// <summary>
        ///     руководство магистерскими программами
        /// </summary>
        public float Rmp { get => _load.Рмп; set => _load.Рмп = value; }

        public float Amount => Lectures + Laboratory + Practical + Test + Consultations + Exams +
                              Nir + CourseDesigning + Vkr + Hack + Hak + Rma + Rmp;

    }
}
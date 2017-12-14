using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;

namespace WpfApp.EntitiesVM
{
    public class SubjectVM : PropertyChangedBase
    {
        public SubjectVM(Предмет subject)
        {
            Subject = subject;
            PlannedLoad = GetPlannedLoadVm(subject.ПлановаяНагрузка);
            Entries = new ObservableCollection<EntryVM>();
        }

        public SubjectVM()
        {
            Entries = new ObservableCollection<EntryVM>();
        }

        public Предмет Subject { get; }

        public int Id { get => Subject.Id; set => Subject.Id = value; }
        public string Название { get => Subject.Название; set => Subject.Название = value; }
        public int Кафедра { get => Subject.Кафедра; set => Subject.Кафедра = value; }
        public string Специальность { get => Subject.Специальность; set => Subject.Специальность = value; }
        public ФормаОбучения ФормаОбучения { get => Subject.ФормаОбучения; set => Subject.ФормаОбучения = value; }
        public int Курс { get => Subject.Курс; set => Subject.Курс = value; }
        public int Семестр { get => Subject.Семестр; set => Subject.Семестр = value; }
        public int НедельВСем { get => Subject.НедельВСем; set => Subject.НедельВСем = value; }
        public string Поток { get => Subject.Поток; set => Subject.Поток = value; }
        public int ЧислоГрупп { get => Subject.ЧислоГрупп; set => Subject.ЧислоГрупп = value; }
        public int ЧислоПодгрупп { get => Subject.ЧислоПодгрупп; set => Subject.ЧислоПодгрупп = value; }
        public int ГруппВПотоке { get => Subject.ГруппВПотоке; set => Subject.ГруппВПотоке = value; }
        public string Численность { get => Subject.Численность; set => Subject.Численность = value; }
        public float Трудоемкость { get => Subject.Трудоемкость; set => Subject.Трудоемкость = value; }
        public float ТрудоемкостьГода { get => Subject.ТрудоемкостьГода; set => Subject.ТрудоемкостьГода = value; }
        public string Lectures { get => Subject.Лк; set => Subject.Лк = value; }
        public string Laboratory { get => Subject.Лаб; set => Subject.Лаб = value; }
        public string Practical { get => Subject.Пр; set => Subject.Пр = value; }
        public bool Exams { get => Subject.Экзамен; set => Subject.Экзамен = value; }
        public bool Test { get => Subject.Зачет; set => Subject.Зачет = value; }
        public КурсовоеПроектирование CourseDesigning { get => Subject.КурсовоеПроектирование; set => Subject.КурсовоеПроектирование = value; }
        public LoadVM PlannedLoad { get ; set; }

        public LoadVM ActualLoad => Convert(Entries);

        public ObservableCollection<EntryVM> Entries { get; set; }

        public static LoadVM Convert(ObservableCollection<EntryVM> записи)
        {
            if (записи == null)
                return new LoadVM(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            return new LoadVM(
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Lectures),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Laboratory),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Practical),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Test),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Consultations),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Exams),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Nir),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.CourseDesigning),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Vkr),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Hack),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Hak),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Rma),
                (float)записи.Aggregate(0.0, (s, a) => s + a.Load.Rmp)
            );
        }

        private static LoadVM GetPlannedLoadVm(Нагрузка нагрузка)
        {
            return new LoadVM(
                нагрузка.Лекции,
                нагрузка.Лабораторные,
                нагрузка.Практические,
                нагрузка.Зачеты,
                нагрузка.Консультации,
                нагрузка.Экзамены,
                нагрузка.Нир,
                нагрузка.КурсовоеПроектирование,
                нагрузка.Вкр,
                нагрузка.Гэк,
                нагрузка.Гак,
                нагрузка.Рма,
                нагрузка.Рмп
            );
        }
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;

namespace WpfApp.EntitiesVM
{
    public class SubjectVM : PropertyChangedBase
    {
        public SubjectVM(Предмет subject, LoadVM plannedLoad)
        {
            _subject = subject;
            PlannedLoad = plannedLoad;
            Entries = new ObservableCollection<EntryVM>();
        }

        private readonly Предмет _subject;

        public int Id { get => _subject.Id; set => _subject.Id = value; }
        public string Название { get => _subject.Название; set => _subject.Название = value; }
        public int Кафедра { get => _subject.Кафедра; set => _subject.Кафедра = value; }
        public string Специальность { get => _subject.Специальность; set => _subject.Специальность = value; }
        public ФормаОбучения ФормаОбучения { get => _subject.ФормаОбучения; set => _subject.ФормаОбучения = value; }
        public int Курс { get => _subject.Курс; set => _subject.Курс = value; }
        public int Семестр { get => _subject.Семестр; set => _subject.Семестр = value; }
        public int НедельВСем { get => _subject.НедельВСем; set => _subject.НедельВСем = value; }
        public string Поток { get => _subject.Поток; set => _subject.Поток = value; }
        public int ЧислоГрупп { get => _subject.ЧислоГрупп; set => _subject.ЧислоГрупп = value; }
        public int ЧислоПодгрупп { get => _subject.ЧислоПодгрупп; set => _subject.ЧислоПодгрупп = value; }
        public int ГруппВПотоке { get => _subject.ГруппВПотоке; set => _subject.ГруппВПотоке = value; }
        public string Численность { get => _subject.Численность; set => _subject.Численность = value; }
        public float Трудоемкость { get => _subject.Трудоемкость; set => _subject.Трудоемкость = value; }
        public float ТрудоемкостьГода { get => _subject.ТрудоемкостьГода; set => _subject.ТрудоемкостьГода = value; }
        public string Lectures { get => _subject.Лк; set => _subject.Лк = value; }
        public string Laboratory { get => _subject.Лаб; set => _subject.Лаб = value; }
        public string Practical { get => _subject.Пр; set => _subject.Пр = value; }
        public bool Exams { get => _subject.Экзамен; set => _subject.Экзамен = value; }
        public bool Test { get => _subject.Зачет; set => _subject.Зачет = value; }
        public КурсовоеПроектирование CourseDesigning { get => _subject.КурсовоеПроектирование; set => _subject.КурсовоеПроектирование = value; }
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
    }
}
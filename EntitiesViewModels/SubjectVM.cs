using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;

namespace EntitiesViewModels
{
    public class SubjectVM : PropertyChangedBase
    {
        public SubjectVM(Предмет subject)
        {
            Subject = subject;
            Entries = new ObservableCollection<EntryVM>();
        }

        public SubjectVM()
        {
            Entries = new ObservableCollection<EntryVM>();
        }

        public Предмет Subject { get; }

        public int Id
        {
            get => Subject.Id;
            set => Subject.Id = value;
        }

        public string Название
        {
            get => Subject.Название;
            set => Subject.Название = value;
        }

        public int Кафедра
        {
            get => Subject.Кафедра;
            set => Subject.Кафедра = value;
        }

        public string Специальность
        {
            get => Subject.Специальность;
            set => Subject.Специальность = value;
        }

        public ФормаОбучения ФормаОбучения
        {
            get => Subject.ФормаОбучения;
            set => Subject.ФормаОбучения = value;
        }

        public int Курс
        {
            get => Subject.Курс;
            set => Subject.Курс = value;
        }

        public int Семестр
        {
            get => Subject.Семестр;
            set => Subject.Семестр = value;
        }

        public int НедельВСем
        {
            get => Subject.НедельВСем;
            set => Subject.НедельВСем = value;
        }

        public string Поток
        {
            get => Subject.Поток;
            set => Subject.Поток = value;
        }

        public int ЧислоГрупп
        {
            get => Subject.ЧислоГрупп;
            set => Subject.ЧислоГрупп = value;
        }

        public int ЧислоПодгрупп
        {
            get => Subject.ЧислоПодгрупп;
            set => Subject.ЧислоПодгрупп = value;
        }

        public int ГруппВПотоке
        {
            get => Subject.ГруппВПотоке;
            set => Subject.ГруппВПотоке = value;
        }

        public string Численность
        {
            get => Subject.Численность;
            set => Subject.Численность = value;
        }

        public float Трудоемкость
        {
            get => Subject.Трудоемкость;
            set => Subject.Трудоемкость = value;
        }

        public float ТрудоемкостьГода
        {
            get => Subject.ТрудоемкостьГода;
            set => Subject.ТрудоемкостьГода = value;
        }

        public string Lectures
        {
            get => Subject.Лк;
            set => Subject.Лк = value;
        }

        public string Laboratory
        {
            get => Subject.Лаб;
            set => Subject.Лаб = value;
        }

        public string Practical
        {
            get => Subject.Пр;
            set => Subject.Пр = value;
        }

        public bool Exams
        {
            get => Subject.Экзамен;
            set => Subject.Экзамен = value;
        }

        public bool Test
        {
            get => Subject.Зачет;
            set => Subject.Зачет = value;
        }

        public КурсовоеПроектирование CourseDesigning
        {
            get => Subject.КурсовоеПроектирование;
            set => Subject.КурсовоеПроектирование = value;
        }

        public Нагрузка PlannedLoad
        {
            get => Subject.ПлановаяНагрузка;
            set => Subject.ПлановаяНагрузка = value;
        }

        public float PlannedLoadSum => PlannedLoad.Lectures + PlannedLoad.Laboratory + PlannedLoad.Practical +
                                       PlannedLoad.Test + PlannedLoad.Consultations + PlannedLoad.Exams +
                                       PlannedLoad.Nir + PlannedLoad.CourseDesigning + PlannedLoad.Vkr +
                                       PlannedLoad.Hack + PlannedLoad.Hak + PlannedLoad.Rma + PlannedLoad.Rmp;

        public Нагрузка ActualLoad => Convert(Entries);

        public float ActualLoadSum => ActualLoad.Lectures + ActualLoad.Laboratory + ActualLoad.Practical +
                                      ActualLoad.Test +
                                      ActualLoad.Consultations + ActualLoad.Exams +
                                      ActualLoad.Nir + ActualLoad.CourseDesigning + ActualLoad.Vkr + ActualLoad.Hack +
                                      ActualLoad.Hak +
                                      ActualLoad.Rma + ActualLoad.Rmp;

        public ObservableCollection<EntryVM> Entries { get; set; }

        public static Нагрузка Convert(IEnumerable<EntryVM> entries)
        {
            if (entries == null || !entries.Any())
                return new Нагрузка(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            return new Нагрузка(
                (float) entries.Aggregate(0.0, (s, a) => s + a.Lectures),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Laboratory),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Practical),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Test),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Consultations),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Exams),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Nir),
                (float) entries.Aggregate(0.0, (s, a) => s + a.CourseDesigning),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Vkr),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Hack),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Hak),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Rma),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Rmp)
            );
        }

        public void UpdateActualLoad()
        {
            RaisePropertyChanged("ActualLoad");
            RaisePropertyChanged("ActualLoadSum");
        }
    }
}
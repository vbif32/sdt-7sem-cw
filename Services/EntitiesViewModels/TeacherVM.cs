using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;

// ReSharper disable InconsistentNaming

namespace EntitiesViewModels
{
    public class TeacherVM : PropertyChangedBase
    {
        public TeacherVM()
        {
            Teacher = new Teacher();
            Entries = new ObservableCollection<EntryVM>();
        }

        public TeacherVM(Teacher teacher)
        {
            Teacher = teacher;
            Entries = new ObservableCollection<EntryVM>();
        }

        public Teacher Teacher { get; }

        public int Id
        {
            get => Teacher.Id;
            set => Teacher.Id = value;
        }

        public string Name_Patronymic_Surname => $"{Name} {Patronymic} {Surname}";
        public string ФИО => $"{Surname} {Name} {Patronymic}";
        public string Surname_N_P => $"{Surname} {Name.First()}. {Patronymic.First()}.";

        public string Name
        {
            get => Teacher.Имя;
            set => Teacher.Имя = value;
        }

        public string Patronymic
        {
            get => Teacher.Отчество;
            set => Teacher.Отчество = value;
        }

        public string Surname
        {
            get => Teacher.Фамилия;
            set => Teacher.Фамилия = value;
        }

        public Post Post
        {
            get => Teacher.Post;
            set => Teacher.Post = value;
        }

        public float Rate
        {
            get => Teacher.Ставка;
            set => Teacher.Ставка = value;
        }

        public string AcademicDegreeFull
        {
            get => Teacher.УченаяСтепеньПолная;
            set => Teacher.УченаяСтепеньПолная = value;
        }

        public string AcademicDegree
        {
            get => Teacher.УченаяСтепень;
            set => Teacher.УченаяСтепень = value;
        }

        public МестоРаботы WorkPlace
        {
            get => Teacher.МестоРаботы;
            set => Teacher.МестоРаботы = value;
        }

        public float PlannedLoad => Rate * Post.Hours;

        public Load ActualLoad => Convert(Entries);

        public float ActualLoadSum => ActualLoad.Lectures + ActualLoad.Laboratory + ActualLoad.Practical +
                                      ActualLoad.Test + ActualLoad.Consultations + ActualLoad.Exams +
                                      ActualLoad.Nir + ActualLoad.CourseDesigning + ActualLoad.Vkr + ActualLoad.Hack +
                                      ActualLoad.Hak + ActualLoad.Rma + ActualLoad.Rmp;

        public ObservableCollection<EntryVM> Entries { get; set; }

        public static Load Convert(IEnumerable<EntryVM> entries)
        {
            if (entries == null || !entries.Any())
                return new Load(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            return new Load(
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
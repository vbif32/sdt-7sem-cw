using System.Collections.ObjectModel;
using System.Linq;
using Entities;

// ReSharper disable InconsistentNaming

namespace WpfApp.EntitiesVM
{
    public class TeacherVM : PropertyChangedBase
    {
        public TeacherVM()
        {
            Teacher = new Преподаватель();
            Entries = new ObservableCollection<EntryVM>();
        }

        public TeacherVM(Преподаватель teacher)
        {
            Teacher = teacher;
            Entries = new ObservableCollection<EntryVM>();
        }

        public Преподаватель Teacher { get; }

        public int Id
        {
            get => Teacher.Id;
            set => Teacher.Id = value;
        }

        public string Name_Patronymic_Surname => $"{Name} {Patronymic} {Surname}";
        public string Surname_Name_Patronymic => $"{Surname} {Name} {Patronymic}";
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

        public Должность Post
        {
            get => Teacher.Должность;
            set => Teacher.Должность = value;
        }

        public float Rate
        {
            get => Teacher.Ставка;
            set => Teacher.Ставка = value;
        }

        public float PlannedLoad => Rate * Post.Часы;
        public float ActualLoad => Entries != null ? (float) Entries.Aggregate(0.0, (s, a) => s + a.Amount) : 0f;

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

        public ObservableCollection<EntryVM> Entries { get; set; }

        public void UpdateActualLoad()
        {
            RaisePropertyChanged("ActualLoad");
        }
    }
}
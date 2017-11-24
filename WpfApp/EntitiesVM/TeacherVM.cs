using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;

// ReSharper disable InconsistentNaming

namespace WpfApp.EntitiesVM
{
    public class TeacherVM :PropertyChangedBase
    {
        public TeacherVM(Преподаватель teacher, PostVM post)
        {
            Teacher = teacher;
            Post = post;
            Entries = new ObservableCollection<EntryVM>();
        }

        public Преподаватель Teacher { get; }

        public TeacherVM()
        {
            Teacher = new Преподаватель();
        }

        public int Id { get => Teacher.Id; set => Teacher.Id = value; }

        public string Name_Patronymic_Surname => $"{Name} {Patronymic} {Surname}";
        public string Surname_Name_Patronymic => $"{Surname} {Name} {Patronymic}";
        public string Surname_N_P => $"{Surname} {Name.First()}. {Patronymic.First()}.";

        public string Name { get => Teacher.Имя; set => Teacher.Имя = value; }
        public string Patronymic { get => Teacher.Отчество; set => Teacher.Отчество = value; }
        public string Surname { get => Teacher.Фамилия; set => Teacher.Фамилия = value; }

        public PostVM Post { get; set; }
        public float Rate { get => Teacher.Ставка; set => Teacher.Ставка = value; }
        public float PlannedLoad => Rate * Post.Hours;
        public float ActualLoad => Entries != null ? (float)Entries.Aggregate(0.0, (s, a) => s + a.Load.Amount) : 0f;

        public string AcademicDegreeFull { get => Teacher.УченаяСтепеньПолная; set => Teacher.УченаяСтепеньПолная = value; }
        public string AcademicDegree { get => Teacher.УченаяСтепень; set => Teacher.УченаяСтепень = value; }

        public МестоРаботы WorkPlace{ get => Teacher.МестоРаботы; set => Teacher.МестоРаботы = value; }

        public ObservableCollection<EntryVM> Entries { get; set; }
    }
}
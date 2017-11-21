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
            _teacher = teacher;
            Post = post;
            Entries = new ObservableCollection<EntryVM>();
        }

        private readonly Преподаватель _teacher;

        public int Id { get => _teacher.Id; set => _teacher.Id = value; }

        public string ПолноеИОФ => $"{Name} {Patronymic} {Surname}";
        public string ПолноеФИО => $"{Surname} {Name} {Patronymic}";
        public string ФамилияИИнициалы => $"{Surname} {Name.First()}. {Patronymic.First()}.";

        public string Name { get => _teacher.Имя; set => _teacher.Имя = value; }
        public string Patronymic { get => _teacher.Отчество; set => _teacher.Отчество = value; }
        public string Surname { get => _teacher.Фамилия; set => _teacher.Фамилия = value; }

        public PostVM Post { get; set; }
        public float Rate { get => _teacher.Ставка; set => _teacher.Ставка = value; }
        public float PlannedLoad => Rate * Post.Hours;
        public float ActualLoad => Entries != null ? (float)Entries.Aggregate(0.0, (s, a) => s + a.Load.Amount) : 0f;

        public string AcademicDegreeFull { get => _teacher.УченаяСтепеньПолная; set => _teacher.УченаяСтепеньПолная = value; }
        public string AcademicDegree { get => _teacher.УченаяСтепень; set => _teacher.УченаяСтепень = value; }

        public МестоРаботы WorkPlace{ get => _teacher.МестоРаботы; set => _teacher.МестоРаботы = value; }

        public ObservableCollection<EntryVM> Entries { get; set; }
    }
}
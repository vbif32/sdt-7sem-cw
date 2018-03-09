using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;

// ReSharper disable InconsistentNaming

namespace Services.EntitiesViewModels
{
    public class TeacherVM : VMBase<Teacher>
    {
        public TeacherVM()
        {
            ModelObject = new Teacher();
            Entries = new ObservableCollection<EntryVM>();
        }

        public TeacherVM(Teacher teacher)
        {
            ModelObject = teacher;
            Entries = new ObservableCollection<EntryVM>();
        }

        public int Id
        {
            get => ModelObject.Id;
            set => ModelObject.Id = value;
        }

        public string Name_Patronymic_Surname => $"{Name} {Patronymic} {Surname}";
        public string ФИО => $"{Surname} {Name} {Patronymic}";
        public string Surname_N_P => $"{Surname} {Name.First()}. {Patronymic.First()}.";
        public string Surname_Name_Patronymic => $"{Surname} {Name}. {Patronymic}.";

        public string Name
        {
            get => ModelObject.Имя;
            set => ModelObject.Имя = value;
        }

        public string Patronymic
        {
            get => ModelObject.Отчество;
            set => ModelObject.Отчество = value;
        }

        public string Surname
        {
            get => ModelObject.Фамилия;
            set => ModelObject.Фамилия = value;
        }

        public Post Post
        {
            get => ModelObject.Post;
            set => ModelObject.Post = value;
        }

        public float Rate
        {
            get => ModelObject.Ставка;
            set => ModelObject.Ставка = value;
        }

        public string AcademicDegreeFull
        {
            get => ModelObject.УченаяСтепеньПолная;
            set => ModelObject.УченаяСтепеньПолная = value;
        }

        public string AcademicDegree
        {
            get => ModelObject.УченаяСтепень;
            set => ModelObject.УченаяСтепень = value;
        }

        public МестоРаботы WorkPlace
        {
            get => ModelObject.МестоРаботы;
            set => ModelObject.МестоРаботы = value;
        }

        public float PlannedLoad => (float) Math.Round((double) Rate * Post.Hours, 2);

        public Load ActualLoad => Convert(Entries.Where(e => e.Subject.IsActive));

        public float ActualLoadSum => ActualLoad.Lectures + ActualLoad.Laboratory + ActualLoad.Practical +
                                      ActualLoad.Test + ActualLoad.Consultations + ActualLoad.Exams +
                                      ActualLoad.Nir + ActualLoad.CourseDesigning + ActualLoad.Vkr + ActualLoad.Gek +
                                      ActualLoad.Gak + ActualLoad.Rma + ActualLoad.Rmp;

        public double ActualLectures2Week => Entries.Aggregate(0.0, (s, a) => s + a.Lectures2Week);
        public double ActualLaboratory2Week => Entries.Aggregate(0.0, (s, a) => s + a.Laboratory2Week);
        public double ActualPractical2Week => Entries.Aggregate(0.0, (s, a) => s + a.Practical2Week);

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
                (float) entries.Aggregate(0.0, (s, a) => s + a.Gek),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Gak),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Rma),
                (float) entries.Aggregate(0.0, (s, a) => s + a.Rmp)
            );
        }

        public void UpdateActualLoad()
        {
            RaisePropertyChanged("ActualLoad");
            RaisePropertyChanged("ActualLoadSum");
            RaisePropertyChanged("ActualLectures2Week");
            RaisePropertyChanged("ActualLaboratory2Week");
            RaisePropertyChanged("ActualPractical2Week");
        }
    }
}
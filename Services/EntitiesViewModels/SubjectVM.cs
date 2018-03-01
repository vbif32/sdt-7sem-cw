using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;

namespace Services.EntitiesViewModels
{
    public class SubjectVM : VMBase<Subject>
    {
        public SubjectVM(Subject subject)
        {
            ModelObject = subject;
            Entries = new ObservableCollection<EntryVM>();
        }

        public SubjectVM()
        {
            Entries = new ObservableCollection<EntryVM>();
        }

        public int Id
        {
            get => ModelObject.Id;
            set => ModelObject.Id = value;
        }

        public bool IsActive
        {
            get => ModelObject.IsActive;
            set => ModelObject.IsActive = value;
        }

        public string Название
        {
            get => ModelObject.Name;
            set => ModelObject.Name = value;
        }

        public int Department
        {
            get => ModelObject.Department;
            set => ModelObject.Department = value;
        }

        public string Специальность
        {
            get => ModelObject.Specialty;
            set => ModelObject.Specialty = value;
        }

        public ФормаОбучения ФормаОбучения
        {
            get => ModelObject.EducationForm;
            set => ModelObject.EducationForm = value;
        }

        public int Курс
        {
            get => ModelObject.Course;
            set => ModelObject.Course = value;
        }

        public int Семестр
        {
            get => ModelObject.Semester;
            set => ModelObject.Semester = value;
        }

        public int НедельВСем
        {
            get => ModelObject.НедельВСем;
            set => ModelObject.НедельВСем = value;
        }

        public string Поток
        {
            get => ModelObject.Flow;
            set => ModelObject.Flow = value;
        }

        public int GroupsCount
        {
            get => ModelObject.GroupsCount;
            set => ModelObject.GroupsCount = value;
        }

        public int SubgroupsCount
        {
            get => ModelObject.SubgroupsCount;
            set => ModelObject.SubgroupsCount = value;
        }

        public int ГруппВПотоке
        {
            get => ModelObject.ГруппВПотоке;
            set => ModelObject.ГруппВПотоке = value;
        }

        public string Численность
        {
            get => ModelObject.Численность;
            set => ModelObject.Численность = value;
        }

        public float Трудоемкость
        {
            get => ModelObject.Трудоемкость;
            set => ModelObject.Трудоемкость = value;
        }

        public float ТрудоемкостьГода
        {
            get => ModelObject.ТрудоемкостьГода;
            set => ModelObject.ТрудоемкостьГода = value;
        }

        public string Lectures
        {
            get => ModelObject.Lectures;
            set => ModelObject.Lectures = value;
        }

        public string Laboratory
        {
            get => ModelObject.Laboratory;
            set => ModelObject.Laboratory = value;
        }

        public string Practical
        {
            get => ModelObject.Practical;
            set => ModelObject.Practical = value;
        }

        public bool Exam
        {
            get => ModelObject.Exam;
            set => ModelObject.Exam = value;
        }

        public bool Test
        {
            get => ModelObject.Test;
            set => ModelObject.Test = value;
        }

        public КурсовоеПроектирование CourseDesigning
        {
            get => ModelObject.CourseDesigning;
            set => ModelObject.CourseDesigning = value;
        }

        public Load PlannedLoad
        {
            get => ModelObject.PlannedLoad;
            set => ModelObject.PlannedLoad = value;
        }

        public double PlannedLectures2Week => PlannedLoad.Lectures / НедельВСем;
        public double PlannedLaboratory2Week => PlannedLoad.Laboratory / НедельВСем;
        public double PlannedPractical2Week => PlannedLoad.Practical / НедельВСем;

        public float PlannedLoadSum => PlannedLoad.Lectures + PlannedLoad.Laboratory + PlannedLoad.Practical +
                                       PlannedLoad.Test + PlannedLoad.Consultations + PlannedLoad.Exams +
                                       PlannedLoad.Nir + PlannedLoad.CourseDesigning + PlannedLoad.Vkr +
                                       PlannedLoad.Gek + PlannedLoad.Gak + PlannedLoad.Rma + PlannedLoad.Rmp;

        public Load ActualLoad => Convert(Entries);

        public double ActualLectures2Week => ActualLoad.Lectures / НедельВСем;
        public double ActualLaboratory2Week => ActualLoad.Laboratory / НедельВСем;
        public double ActualPractical2Week => ActualLoad.Practical / НедельВСем;

        public float ActualLoadSum => ActualLoad.Lectures + ActualLoad.Laboratory + ActualLoad.Practical +
                                      ActualLoad.Test +
                                      ActualLoad.Consultations + ActualLoad.Exams +
                                      ActualLoad.Nir + ActualLoad.CourseDesigning + ActualLoad.Vkr + ActualLoad.Gek +
                                      ActualLoad.Gak +
                                      ActualLoad.Rma + ActualLoad.Rmp;

        public ObservableCollection<EntryVM> Entries { get; set; }


        public void UpdateActualLoad()
        {
            RaisePropertyChanged("ActualLoad");
            RaisePropertyChanged("ActualLoadSum");
            RaisePropertyChanged("ActualLectures2Week");
            RaisePropertyChanged("ActualLaboratory2Week");
            RaisePropertyChanged("ActualPractical2Week");
        }

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

        public bool IsSame(Subject subject)
        {
            return Название == subject.Name
                   && Поток.Substring(0, 6) == subject.Flow.Substring(0, 6)
                   && Поток.Substring(Поток.Length - 2, 2) == subject.Flow.Substring(subject.Flow.Length - 2, 2);
        }

        public void Update(Subject subject)
        {
            Название = subject.Name;
            IsActive = true;
            Department = subject.Department;
            Специальность = subject.Specialty;
            ФормаОбучения = subject.EducationForm;
            Курс = subject.Course;
            Семестр = subject.Semester;
            НедельВСем = subject.НедельВСем;
            Поток = subject.Flow;
            GroupsCount = subject.GroupsCount;
            SubgroupsCount = subject.SubgroupsCount;
            ГруппВПотоке = subject.ГруппВПотоке;
            Численность = subject.Численность;
            Трудоемкость = subject.Трудоемкость;
            ТрудоемкостьГода = subject.ТрудоемкостьГода;
            Lectures = subject.Lectures;
            Laboratory = subject.Laboratory;
            Practical = subject.Practical;
            Exam = subject.Exam;
            Test = subject.Test;
            CourseDesigning = subject.CourseDesigning;
            PlannedLoad = subject.PlannedLoad;
        }
    }
}
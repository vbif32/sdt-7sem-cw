using Entities;

namespace Services.EntitiesViewModels
{
    public class EntryVM : VMBase<Entry>
    {
        private SubjectVM _subject;
        private TeacherVM _teacher;

        public EntryVM()
        {
            ModelObject = new Entry();
            Subject = new SubjectVM();
            Teacher = new TeacherVM();
        }

        public EntryVM(Entry entry, SubjectVM subject, TeacherVM teacher)
        {
            ModelObject = entry;
            Subject = subject;
            Teacher = teacher;
        }

        public SubjectVM Subject
        {
            get => _subject;
            set
            {
                _subject?.Entries.Remove(this);
                _subject = value;
                if (_subject?.Entries.Contains(this) == false)
                    _subject?.Entries.Add(this);
            }
        }

        public TeacherVM Teacher
        {
            get => _teacher;
            set
            {
                _teacher?.Entries.Remove(this);
                _teacher = value;
                if (_teacher?.Entries.Contains(this) == false)
                    _teacher?.Entries.Add(this);
            }
        }

        public float Lectures
        {
            get => ModelObject.Load.Lectures;
            set
            {
                ModelObject.Load.Lectures = value;
                OnLoadChanged();
            }
        }
        public double Lectures2Week => Lectures / Subject.НедельВСем;

        public float Laboratory
        {
            get => ModelObject.Load.Laboratory;
            set
            {
                ModelObject.Load.Laboratory = value;
                OnLoadChanged();
            }
        }
        public double Laboratory2Week => Laboratory / Subject.НедельВСем;

        public float Practical
        {
            get => ModelObject.Load.Practical;
            set
            {
                ModelObject.Load.Practical = value;
                OnLoadChanged();
            }
        }
        public double Practical2Week => Practical / Subject.НедельВСем;

        public float Test
        {
            get => ModelObject.Load.Test;
            set
            {
                ModelObject.Load.Test = value;
                OnLoadChanged();
            }
        }

        public float Consultations
        {
            get => ModelObject.Load.Consultations;
            set
            {
                ModelObject.Load.Consultations = value;
                OnLoadChanged();
            }
        }

        public float Exams
        {
            get => ModelObject.Load.Exams;
            set
            {
                ModelObject.Load.Exams = value;
                OnLoadChanged();
            }
        }

        public float Nir
        {
            get => ModelObject.Load.Nir;
            set
            {
                ModelObject.Load.Nir = value;
                OnLoadChanged();
            }
        }

        public float CourseDesigning
        {
            get => ModelObject.Load.CourseDesigning;
            set
            {
                ModelObject.Load.CourseDesigning = value;
                OnLoadChanged();
            }
        }

        public float Vkr
        {
            get => ModelObject.Load.Vkr;
            set
            {
                ModelObject.Load.Vkr = value;
                OnLoadChanged();
            }
        }

        public float Gek
        {
            get => ModelObject.Load.Gek;
            set
            {
                ModelObject.Load.Gek = value;
                OnLoadChanged();
            }
        }

        public float Gak
        {
            get => ModelObject.Load.Gak;
            set
            {
                ModelObject.Load.Gak = value;
                OnLoadChanged();
            }
        }

        /// <summary>
        ///     Руководство магитрами аспирантам
        /// </summary>
        public float Rma
        {
            get => ModelObject.Load.Rma;
            set
            {
                ModelObject.Load.Rma = value;
                OnLoadChanged();
            }
        }

        /// <summary>
        ///     руководство магистерскими программами
        /// </summary>
        public float Rmp
        {
            get => ModelObject.Load.Rmp;
            set
            {
                ModelObject.Load.Rmp = value;
                OnLoadChanged();
            }
        }

        public float Amount => Lectures + Laboratory + Practical + Test + Consultations + Exams +
                               Nir + CourseDesigning + Vkr + Gek + Gak + Rma + Rmp;

        private void OnLoadChanged()
        {
            RaisePropertyChanged("Amount");
            Subject.UpdateActualLoad();
            Teacher.UpdateActualLoad();
        }
    }
}
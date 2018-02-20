using Entities;

namespace Services.EntitiesViewModels
{
    public class EntryVM : PropertyChangedBase
    {
        private SubjectVM _subject;
        private TeacherVM _teacher;

        public EntryVM()
        {
            Entry = new Entry();
            Subject = new SubjectVM();
            Teacher = new TeacherVM();
        }

        public EntryVM(Entry entry, SubjectVM subject, TeacherVM teacher)
        {
            Entry = entry;
            Subject = subject;
            Teacher = teacher;
        }


        public Entry Entry { get; set; }

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
            get => Entry.Load.Lectures;
            set
            {
                Entry.Load.Lectures = value;
                OnLoadChanged();
            }
        }

        public float Laboratory
        {
            get => Entry.Load.Laboratory;
            set
            {
                Entry.Load.Laboratory = value;
                OnLoadChanged();
            }
        }

        public float Practical
        {
            get => Entry.Load.Practical;
            set
            {
                Entry.Load.Practical = value;
                OnLoadChanged();
            }
        }

        public float Test
        {
            get => Entry.Load.Test;
            set
            {
                Entry.Load.Test = value;
                OnLoadChanged();
            }
        }

        public float Consultations
        {
            get => Entry.Load.Consultations;
            set
            {
                Entry.Load.Consultations = value;
                OnLoadChanged();
            }
        }

        public float Exams
        {
            get => Entry.Load.Exams;
            set
            {
                Entry.Load.Exams = value;
                OnLoadChanged();
            }
        }

        public float Nir
        {
            get => Entry.Load.Nir;
            set
            {
                Entry.Load.Nir = value;
                OnLoadChanged();
            }
        }

        public float CourseDesigning
        {
            get => Entry.Load.CourseDesigning;
            set
            {
                Entry.Load.CourseDesigning = value;
                OnLoadChanged();
            }
        }

        public float Vkr
        {
            get => Entry.Load.Vkr;
            set
            {
                Entry.Load.Vkr = value;
                OnLoadChanged();
            }
        }

        public float Hack
        {
            get => Entry.Load.Hack;
            set
            {
                Entry.Load.Hack = value;
                OnLoadChanged();
            }
        }

        public float Hak
        {
            get => Entry.Load.Hak;
            set
            {
                Entry.Load.Hak = value;
                OnLoadChanged();
            }
        }

        /// <summary>
        ///     Руководство магитрами аспирантам
        /// </summary>
        public float Rma
        {
            get => Entry.Load.Rma;
            set
            {
                Entry.Load.Rma = value;
                OnLoadChanged();
            }
        }

        /// <summary>
        ///     руководство магистерскими программами
        /// </summary>
        public float Rmp
        {
            get => Entry.Load.Rmp;
            set
            {
                Entry.Load.Rmp = value;
                OnLoadChanged();
            }
        }

        public float Amount => Lectures + Laboratory + Practical + Test + Consultations + Exams +
                               Nir + CourseDesigning + Vkr + Hack + Hak + Rma + Rmp;

        private void OnLoadChanged()
        {
            RaisePropertyChanged("Amount");
            Subject.UpdateActualLoad();
            Teacher.UpdateActualLoad();
        }
    }
}
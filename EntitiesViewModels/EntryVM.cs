using Entities;

namespace WpfApp.EntitiesVM
{
    public class EntryVM : PropertyChangedBase
    {
        private float _consultations;
        private float _courseDesigning;

        private float _exams;
        private float _hack;
        private float _hak;
        private float _laboratory;
        private float _lectures;
        private float _nir;
        private float _practical;
        private float _rma;
        private float _rmp;
        private SubjectVM _subject;
        private TeacherVM _teacher;
        private float _test;
        private float _vkr;

        public EntryVM()
        {
            Entry = new Запись();
            Subject = new SubjectVM();
            Teacher = new TeacherVM();
        }

        public EntryVM(Запись entry, SubjectVM subject, TeacherVM teacher)
        {
            Entry = entry;
            Subject = subject;
            Teacher = teacher;

            Lectures = Entry.Нагрузка.Lectures;
            Laboratory = Entry.Нагрузка.Laboratory;
            Practical = Entry.Нагрузка.Practical;
            Test = Entry.Нагрузка.Test;
            Consultations = Entry.Нагрузка.Consultations;
            Exams = Entry.Нагрузка.Exams;
            Nir = Entry.Нагрузка.Nir;
            CourseDesigning = Entry.Нагрузка.CourseDesigning;
            Vkr = Entry.Нагрузка.Vkr;
            Hack = Entry.Нагрузка.Hack;
            Hak = Entry.Нагрузка.Hak;
            Rma = Entry.Нагрузка.Rma;
            Rmp = Entry.Нагрузка.Rmp;
        }


        public Запись Entry { get; set; }

        public SubjectVM Subject
        {
            get => _subject;
            set
            {
                _subject?.Entries.Remove(this);
                _subject = value;
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
                _teacher?.Entries.Add(this);
            }
        }

        public float Lectures
        {
            get => _lectures;
            set
            {
                _lectures = value;
                OnLoadChanged();
            }
        }

        public float Laboratory
        {
            get => _laboratory;
            set
            {
                _laboratory = value;
                OnLoadChanged();
            }
        }

        public float Practical
        {
            get => _practical;
            set
            {
                _practical = value;
                OnLoadChanged();
            }
        }

        public float Test
        {
            get => _test;
            set
            {
                _test = value;
                OnLoadChanged();
            }
        }

        public float Consultations
        {
            get => _consultations;
            set
            {
                _consultations = value;
                OnLoadChanged();
            }
        }

        public float Exams
        {
            get => _exams;
            set
            {
                _exams = value;
                OnLoadChanged();
            }
        }

        public float Nir
        {
            get => _nir;
            set
            {
                _nir = value;
                OnLoadChanged();
            }
        }

        public float CourseDesigning
        {
            get => _courseDesigning;
            set
            {
                _courseDesigning = value;
                OnLoadChanged();
            }
        }

        public float Vkr
        {
            get => _vkr;
            set
            {
                _vkr = value;
                OnLoadChanged();
            }
        }

        public float Hack
        {
            get => _hack;
            set
            {
                _hack = value;
                OnLoadChanged();
            }
        }

        public float Hak
        {
            get => _hak;
            set
            {
                _hak = value;
                OnLoadChanged();
            }
        }

        /// <summary>
        ///     Руководство магитрами аспирантам
        /// </summary>
        public float Rma
        {
            get => _rma;
            set
            {
                _rma = value;
                OnLoadChanged();
            }
        }

        /// <summary>
        ///     руководство магистерскими программами
        /// </summary>
        public float Rmp
        {
            get => _rmp;
            set
            {
                _rmp = value;
                OnLoadChanged();
            }
        }

        public float Amount => Lectures + Laboratory + Practical + Test + Consultations + Exams +
                               Nir + CourseDesigning + Vkr + Hack + Hak + Rma + Rmp;

        public void Save()
        {
            Entry.Предмет = Subject.Subject;
            Entry.Преподаватель = Teacher.Teacher;
            Entry.Нагрузка.Lectures = Lectures;
            Entry.Нагрузка.Laboratory = Laboratory;
            Entry.Нагрузка.Practical = Practical;
            Entry.Нагрузка.Test = Test;
            Entry.Нагрузка.Consultations = Consultations;
            Entry.Нагрузка.Exams = Exams;
            Entry.Нагрузка.Nir = Nir;
            Entry.Нагрузка.CourseDesigning = CourseDesigning;
            Entry.Нагрузка.Vkr = Vkr;
            Entry.Нагрузка.Hack = Hack;
            Entry.Нагрузка.Hak = Hak;
            Entry.Нагрузка.Rma = Rma;
            Entry.Нагрузка.Rmp = Rmp;
        }

        private void OnLoadChanged()
        {
            RaisePropertyChanged("Amount");
            Subject.UpdateActualLoad();
            Teacher.UpdateActualLoad();
        }
    }
}
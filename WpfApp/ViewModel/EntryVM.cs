using Entities;

namespace WpfApp.EntitiesVM
{
    public class EntryVM : PropertyChangedBase
    {
        public EntryVM()
        {
            Entry = new Запись();
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

        public Запись Entry;
        private float _lectures;
        private float _test;
        private float _consultations;
        private float _exams;
        private float _nir;
        private float _courseDesigning;
        private float _vkr;
        private float _hack;
        private float _hak;
        private float _rma;
        private float _rmp;
        private float _practical;
        private float _laboratory;

        public SubjectVM Subject { get; set; }
        public TeacherVM Teacher { get; set; }

        public float Lectures
        {
            get { return _lectures; }
            set { _lectures = value; RaisePropertyChanged("Amount");}
        }

        public float Laboratory
        {
            get { return _laboratory; }
            set { _laboratory = value; RaisePropertyChanged("Amount");  }
        }

        public float Practical
        {
            get { return _practical; }
            set { _practical = value; RaisePropertyChanged("Amount");  }
        }

        public float Test
        {
            get { return _test; }
            set { _test = value; RaisePropertyChanged("Amount");  }
        }

        public float Consultations
        {
            get { return _consultations; }
            set { _consultations = value; RaisePropertyChanged("Amount");  }
        }

        public float Exams
        {
            get { return _exams; }
            set { _exams = value; RaisePropertyChanged("Amount");  }
        }

        public float Nir
        {
            get { return _nir; }
            set { _nir = value; RaisePropertyChanged("Amount");  }
        }

        public float CourseDesigning
        {
            get { return _courseDesigning; }
            set { _courseDesigning = value; RaisePropertyChanged("Amount");  }
        }

        public float Vkr
        {
            get { return _vkr; }
            set { _vkr = value; RaisePropertyChanged("Amount");  }
        }

        public float Hack
        {
            get { return _hack; }
            set { _hack = value; RaisePropertyChanged("Amount");  }
        }

        public float Hak
        {
            get { return _hak; }
            set { _hak = value; RaisePropertyChanged("Amount");  }
        }

        /// <summary>
        ///     Руководство магитрами аспирантам
        /// </summary>
        public float Rma
        {
            get { return _rma; }
            set { _rma = value; RaisePropertyChanged("Amount");  }
        }

        /// <summary>
        ///     руководство магистерскими программами
        /// </summary>
        public float Rmp
        {
            get { return _rmp; }
            set { _rmp = value; RaisePropertyChanged("Amount");  }
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

    }
}
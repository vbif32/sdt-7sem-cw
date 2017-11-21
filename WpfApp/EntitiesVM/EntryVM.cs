using Entities;

namespace WpfApp.EntitiesVM
{
    public class EntryVM : PropertyChangedBase
    {
        public EntryVM(Запись entry) => _entry = entry;

        public EntryVM(Запись entry, SubjectVM subject, TeacherVM teacher, LoadVM load)
        {
            _entry = entry;
            Subject = subject;
            Teacher = teacher;
            Load = load;
        }


        public EntryVM()
        {
        }

        private readonly Запись _entry;

        public int Id => _entry.Id;
        public SubjectVM Subject { get; set; }
        public TeacherVM Teacher { get; set; }
        public LoadVM Load { get; set; }

        public void SaveChanges() { }
    }
}
using Entities;

namespace WpfApp.EntitiesVM
{
    public class EntryVM : PropertyChangedBase
    {
        public EntryVM(Запись entry) => Entry = entry;

        public EntryVM(Запись entry, SubjectVM subject, TeacherVM teacher, LoadVM load)
        {
            Entry = entry;
            Subject = subject;
            Teacher = teacher;
            Load = load;
        }


        public EntryVM()
        {
            Entry = new Запись();
        }

        public Запись Entry;

        public int Id => Entry.Id;
        public SubjectVM Subject { get; set; }
        public TeacherVM Teacher { get; set; }
        public LoadVM Load { get; set; }

        public void SaveChanges() { }
    }
}
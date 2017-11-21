using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Dao;

namespace WpfApp.EntitiesVM
{
    public class EntitiesVMRegistry
    {
        public ObservableCollection<EntryVM> Entries;
        public ObservableCollection<LoadVM> Loads;
        public ObservableCollection<PostVM> Posts;
        public ObservableCollection<SubjectVM> Subjects;
        public ObservableCollection<TeacherVM> Teachers;

        public EntitiesVMRegistry(DaoRegistry daoRegistry)
        {
            foreach (var post in daoRegistry.PostDao.FindAll())
                Posts.Add(new PostVM(post));
            foreach (var load in daoRegistry.LoadDao.FindAll())
                Loads.Add(new LoadVM(load));
            foreach (var teacher in daoRegistry.TeacherDao.FindAll())
                Teachers.Add(new TeacherVM(teacher,Posts.First(post => post.Id == teacher.Должность.Id)));
            foreach (var subject in daoRegistry.SubjectDao.FindAll())
                Subjects.Add(new SubjectVM(subject, Loads.First(load => load.Id == subject.ПлановаяНагрузка.Id)));
            foreach (var entry in daoRegistry.EntryDao.FindAll())
                Entries.Add(new EntryVM(entry, 
                    Subjects.First(subject => subject.Id == entry.Предмет.Id),
                    Teachers.First(teacher => teacher.Id == entry.Преподаватель.Id),
                    Loads.First(load => load.Id == entry.Нагрузка.Id)));
            foreach (var teacher in Teachers)
                foreach (var entry in Entries.Where(entry => entry.Teacher.Id == teacher.Id))
                    teacher.Entries.Add(entry);
            foreach (var subject in Subjects)
                foreach (var entry in Entries.Where(entry => entry.Subject.Id == subject.Id))
                    subject.Entries.Add(entry);

            Teachers.CollectionChanged += OnTeachersChanged;
        }

        private void OnTeachersChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            
        }
    }
}
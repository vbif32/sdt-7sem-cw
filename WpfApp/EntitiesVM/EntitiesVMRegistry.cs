using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Dao;
using Entities;

namespace WpfApp.EntitiesVM
{
    public class EntitiesVMRegistry
    {
        private readonly DaoRegistry _daoRegistry;

        public ObservableCollection<EntryVM> Entries = new ObservableCollection<EntryVM>();
        public List<Запись> DeletedEntries = new List<Запись>();
        public ObservableCollection<LoadVM> Loads = new ObservableCollection<LoadVM>();
        public List<Нагрузка> DeletedLoads = new List<Нагрузка>();
        public ObservableCollection<PostVM> Posts = new ObservableCollection<PostVM>();
        public List<Должность> DeletedPosts = new List<Должность>();
        public ObservableCollection<SubjectVM> Subjects = new ObservableCollection<SubjectVM>();
        public List<Предмет> DeletedSubjects = new List<Предмет>();
        public ObservableCollection<TeacherVM> Teachers = new ObservableCollection<TeacherVM>();
        public List<Преподаватель> DeletedTeachers = new List<Преподаватель>();

        public EntitiesVMRegistry(DaoRegistry daoRegistry)
        {
            _daoRegistry = daoRegistry;
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

            Posts.CollectionChanged += Posts_CollectionChanged;
            Loads.CollectionChanged += Loads_CollectionChanged;
            Teachers.CollectionChanged += Teachers_CollectionChanged;
            Subjects.CollectionChanged += Subjects_CollectionChanged;
            Entries.CollectionChanged += Entries_CollectionChanged;
        }

        public void ResetCollections()
        {
            Posts.CollectionChanged -= Posts_CollectionChanged;
            Loads.CollectionChanged -= Loads_CollectionChanged;
            Teachers.CollectionChanged -= Teachers_CollectionChanged;
            Subjects.CollectionChanged -= Subjects_CollectionChanged;
            Entries.CollectionChanged -= Entries_CollectionChanged;

            Posts.Clear();
            Loads.Clear();
            Teachers.Clear();
            Subjects.Clear();
            Entries.Clear();

            foreach (var post in _daoRegistry.PostDao.FindAll())
                Posts.Add(new PostVM(post));
            foreach (var load in _daoRegistry.LoadDao.FindAll())
                Loads.Add(new LoadVM(load));
            foreach (var teacher in _daoRegistry.TeacherDao.FindAll())
                Teachers.Add(new TeacherVM(teacher, Posts.First(post => post.Id == teacher.Должность.Id)));
            foreach (var subject in _daoRegistry.SubjectDao.FindAll())
                Subjects.Add(new SubjectVM(subject, Loads.First(load => load.Id == subject.ПлановаяНагрузка.Id)));
            foreach (var entry in _daoRegistry.EntryDao.FindAll())
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

            Posts.CollectionChanged += Posts_CollectionChanged;
            Loads.CollectionChanged += Loads_CollectionChanged;
            Teachers.CollectionChanged += Teachers_CollectionChanged;
            Subjects.CollectionChanged += Subjects_CollectionChanged;
            Entries.CollectionChanged += Entries_CollectionChanged;
        }

        private void Posts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    DeletedPosts.Add(((PostVM)sender).Post);
                    break;
            }
        }
        private void Loads_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    DeletedLoads.Add(((LoadVM)sender).Load);
                    break;
            }
        }
        private void Teachers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    DeletedTeachers.Add(((TeacherVM)sender).Teacher);
                    break;
            }
        }
        private void Subjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    DeletedSubjects.Add(((SubjectVM)sender).Subject);
                    break;
            }
        }
        private void Entries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    DeletedEntries.Add(((EntryVM)sender).Entry);
                    break;
            }
        }

        public void SaveChanges()
        {
            foreach (var post in Posts)
                if (!_daoRegistry.PostDao.Update(post.Post))
                    _daoRegistry.PostDao.Insert(post.Post);
            _daoRegistry.PostDao.Delete(DeletedPosts);
            foreach (var load in Loads)
                if (!_daoRegistry.LoadDao.Update(load.Load))
                    _daoRegistry.LoadDao.Insert(load.Load);
            _daoRegistry.LoadDao.Delete(DeletedLoads);
            foreach (var teacher in Teachers)
                if (!_daoRegistry.TeacherDao.Update(teacher.Teacher))
                    _daoRegistry.TeacherDao.Insert(teacher.Teacher);
            _daoRegistry.TeacherDao.Delete(DeletedTeachers);
            foreach (var subject in Subjects)
                if (!_daoRegistry.SubjectDao.Update(subject.Subject))
                    _daoRegistry.SubjectDao.Insert(subject.Subject);
            _daoRegistry.SubjectDao.Delete(DeletedSubjects);
            foreach (var entry in Entries)
                if (!_daoRegistry.EntryDao.Update(entry.Entry))
                    _daoRegistry.EntryDao.Insert(entry.Entry);
            _daoRegistry.EntryDao.Delete(DeletedEntries);
        }
        
    }
}
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
        public List<Запись> DeletedEntries = new List<Запись>();
        public List<Нагрузка> DeletedLoads = new List<Нагрузка>();
        public List<Должность> DeletedPosts = new List<Должность>();
        public List<Предмет> DeletedSubjects = new List<Предмет>();
        public List<Преподаватель> DeletedTeachers = new List<Преподаватель>();

        public ObservableCollection<EntryVM> Entries = new ObservableCollection<EntryVM>();

        public ObservableCollection<LoadVM> Loads = new ObservableCollection<LoadVM>();
        public List<Запись> NewEntries = new List<Запись>();
        public List<Нагрузка> NewLoads = new List<Нагрузка>();
        public List<Должность> NewPosts = new List<Должность>();
        public List<Предмет> NewSubjects = new List<Предмет>();
        public List<Преподаватель> NewTeachers = new List<Преподаватель>();

        public ObservableCollection<PostVM> Posts = new ObservableCollection<PostVM>();

        public ObservableCollection<SubjectVM> Subjects = new ObservableCollection<SubjectVM>();

        public ObservableCollection<TeacherVM> Teachers = new ObservableCollection<TeacherVM>();


        public EntitiesVMRegistry(DaoRegistry daoRegistry)
        {
            _daoRegistry = daoRegistry;
            CollectionsInitialaize();

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

            CollectionsInitialaize();

            Posts.CollectionChanged += Posts_CollectionChanged;
            Loads.CollectionChanged += Loads_CollectionChanged;
            Teachers.CollectionChanged += Teachers_CollectionChanged;
            Subjects.CollectionChanged += Subjects_CollectionChanged;
            Entries.CollectionChanged += Entries_CollectionChanged;
        }

        private void CollectionsInitialaize()
        {
            foreach (var post in _daoRegistry.PostDao.FindAll())
                Posts.Add(new PostVM(post));
            foreach (var load in _daoRegistry.LoadDao.FindAll())
                Loads.Add(new LoadVM(load));
            foreach (var teacher in _daoRegistry.TeacherDao.FindAll())
                Teachers.Add(new TeacherVM(teacher));
            foreach (var subject in _daoRegistry.SubjectDao.FindAll())
                Subjects.Add(new SubjectVM(subject));
            foreach (var entry in _daoRegistry.EntryDao.FindAll())
                Entries.Add(new EntryVM(entry,
                    Subjects.First(subject => subject.Id == entry.Предмет.Id),
                    Teachers.First(teacher => teacher.Id == entry.Преподаватель.Id)));
            foreach (var teacher in Teachers)
            foreach (var entry in Entries.Where(entry => entry.Teacher.Id == teacher.Id))
                teacher.Entries.Add(entry);
            foreach (var subject in Subjects)
            foreach (var entry in Entries.Where(entry => entry.Subject.Id == subject.Id))
                subject.Entries.Add(entry);
        }

        private void Posts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    DeletedPosts.Add(((PostVM) e.OldItems[0]).Post);
                    NewPosts.Remove(((PostVM) e.OldItems[0]).Post);
                    break;
                case NotifyCollectionChangedAction.Add:

                    NewPosts.Add(((PostVM) e.NewItems[0]).Post);
                    break;
            }
        }

        private void Loads_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    DeletedLoads.Add(((LoadVM) e.OldItems[0]).Load);
                    NewLoads.Remove(((LoadVM) e.OldItems[0]).Load);
                    break;
                case NotifyCollectionChangedAction.Add:
                    NewLoads.Add(((LoadVM) e.NewItems[0]).Load);
                    break;
            }
        }

        private void Teachers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    DeletedTeachers.Add(((TeacherVM) e.OldItems[0]).Teacher);
                    NewTeachers.Remove(((TeacherVM) e.OldItems[0]).Teacher);
                    break;
                case NotifyCollectionChangedAction.Add:
                    NewTeachers.Add(((TeacherVM) e.NewItems[0]).Teacher);
                    break;
            }
        }

        private void Subjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    DeletedSubjects.Add(((SubjectVM) e.OldItems[0]).Subject);
                    NewSubjects.Remove(((SubjectVM) e.OldItems[0]).Subject);
                    break;
                case NotifyCollectionChangedAction.Add:
                    NewSubjects.Add(((SubjectVM) e.NewItems[0]).Subject);
                    break;
            }
        }

        private void Entries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    foreach (var teacherVm in Teachers)
                        teacherVm.Entries.Clear();
                    foreach (var subjectVm in Subjects)
                        subjectVm.Entries.Clear();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    DeletedEntries.Add(((EntryVM) e.OldItems[0]).Entry);
                    NewEntries.Remove(((EntryVM) e.OldItems[0]).Entry);
                    break;
                case NotifyCollectionChangedAction.Add:
                    NewEntries.Add(((EntryVM) e.NewItems[0]).Entry);
                    break;
            }
        }

        public void SaveChanges()
        {
            SavePosts();
            SaveTeachers();
            SaveEntries();
            SaveLoads();
            SaveSubjects();


            void SaveLoads()
            {
                _daoRegistry.LoadDao.Delete(DeletedLoads);
                DeletedLoads.Clear();
                _daoRegistry.LoadDao.Insert(NewLoads);
                NewLoads.Clear();
                foreach (var load in Loads)
                {
                    if (load.IsChanged)
                        _daoRegistry.LoadDao.Update(load.Load);
                    load.IsChanged = false;
                }
            }

            void SaveSubjects()
            {
                _daoRegistry.SubjectDao.Delete(DeletedSubjects);
                DeletedSubjects.Clear();
                _daoRegistry.SubjectDao.Insert(NewSubjects);
                NewSubjects.Clear();
                foreach (var subject in Subjects)
                {
                    if (subject.IsChanged)
                        _daoRegistry.SubjectDao.Update(subject.Subject);
                    subject.IsChanged = false;
                }
            }
        }


        public void SavePosts()
        {
            _daoRegistry.PostDao.Delete(DeletedPosts);
            DeletedPosts.Clear();
            _daoRegistry.PostDao.Insert(NewPosts);
            NewPosts.Clear();
            foreach (var post in Posts)
            {
                if (post.IsChanged)
                    _daoRegistry.PostDao.Update(post.Post);
                post.IsChanged = false;
            }
        }

        public void SaveTeachers()
        {
            _daoRegistry.TeacherDao.Delete(DeletedTeachers);
            DeletedTeachers.Clear();
            _daoRegistry.TeacherDao.Insert(NewTeachers);
            NewTeachers.Clear();
            foreach (var teacher in Teachers)
            {
                if (teacher.IsChanged)
                    _daoRegistry.TeacherDao.Update(teacher.Teacher);
                teacher.IsChanged = false;
            }
        }

        public void SaveEntries()
        {
            _daoRegistry.EntryDao.Delete(DeletedEntries);
            DeletedEntries.Clear();
            _daoRegistry.EntryDao.Insert(NewEntries);
            NewEntries.Clear();
            foreach (var entry in Entries)
            {
                if (entry.IsChanged)
                {
                    _daoRegistry.EntryDao.Update(entry.Entry);
                }
                entry.IsChanged = false;
            }
        }
    }
}
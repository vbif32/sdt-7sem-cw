using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Dao;
using Entities;

namespace Services.EntitiesViewModels
{
    public class EntitiesVMRegistry
    {
        private readonly DaoRegistry _daoRegistry;

        public List<Entry> DeletedEntries = new List<Entry>();
        public List<Load> DeletedLoads = new List<Load>();
        public List<Post> DeletedPosts = new List<Post>();
        public List<Setting> DeletedSettings = new List<Setting>();
        public List<Specialty> DeletedSpecialties = new List<Specialty>();
        public List<Subject> DeletedSubjects = new List<Subject>();
        public List<Teacher> DeletedTeachers = new List<Teacher>();

        public ObservableCollection<EntryVM> Entries = new ObservableCollection<EntryVM>();
        public ObservableCollection<LoadVM> Loads = new ObservableCollection<LoadVM>();

        public List<Entry> NewEntries = new List<Entry>();
        public List<Load> NewLoads = new List<Load>();
        public List<Post> NewPosts = new List<Post>();
        public List<Setting> NewSettings = new List<Setting>();
        public List<Specialty> NewSpecialties = new List<Specialty>();
        public List<Subject> NewSubjects = new List<Subject>();
        public List<Teacher> NewTeachers = new List<Teacher>();
        public ObservableCollection<PostVM> Posts = new ObservableCollection<PostVM>();
        public ObservableCollection<SettingVM> Settings = new ObservableCollection<SettingVM>();
        public ObservableCollection<SpecialtyVM> Specialties = new ObservableCollection<SpecialtyVM>();
        public ObservableCollection<SubjectVM> Subjects = new ObservableCollection<SubjectVM>();
        public ObservableCollection<TeacherVM> Teachers = new ObservableCollection<TeacherVM>();


        public EntitiesVMRegistry(DaoRegistry daoRegistry)
        {
            _daoRegistry = daoRegistry;
            CollectionsInitialaize();
            AddEvents();
        }

        public void ReloadCollections()
        {
            RemoveEvents();
            EraseCollections();
            CollectionsInitialaize();
            AddEvents();
        }

        private void AddEvents()
        {
            Posts.CollectionChanged += Posts_CollectionChanged;
            Loads.CollectionChanged += Loads_CollectionChanged;
            Teachers.CollectionChanged += Teachers_CollectionChanged;
            Subjects.CollectionChanged += Subjects_CollectionChanged;
            Settings.CollectionChanged += Settings_CollectionChanged;
            Specialties.CollectionChanged += Specialties_CollectionChanged;
            Entries.CollectionChanged += Entries_CollectionChanged;
        }

        private void RemoveEvents()
        {
            Posts.CollectionChanged -= Posts_CollectionChanged;
            Loads.CollectionChanged -= Loads_CollectionChanged;
            Teachers.CollectionChanged -= Teachers_CollectionChanged;
            Subjects.CollectionChanged -= Subjects_CollectionChanged;
            Settings.CollectionChanged -= Settings_CollectionChanged;
            Specialties.CollectionChanged -= Specialties_CollectionChanged;
            Entries.CollectionChanged -= Entries_CollectionChanged;
        }

        private void EraseCollections()
        {
            DeletedPosts.Clear();
            Posts.Clear();
            NewPosts.Clear();

            DeletedLoads.Clear();
            Loads.Clear();
            NewLoads.Clear();

            DeletedTeachers.Clear();
            Teachers.Clear();
            NewTeachers.Clear();

            DeletedSubjects.Clear();
            Subjects.Clear();
            NewSubjects.Clear();

            DeletedSettings.Clear();
            Settings.Clear();
            NewSettings.Clear();

            DeletedSpecialties.Clear();
            Specialties.Clear();
            NewSpecialties.Clear();

            DeletedEntries.Clear();
            Entries.Clear();
            NewEntries.Clear();
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
            foreach (var setting in _daoRegistry.SettingDao.FindAll())
                Settings.Add(new SettingVM(setting));
            foreach (var specialty in _daoRegistry.SpecialtyDao.FindAll())
                Specialties.Add(new SpecialtyVM(specialty));
            foreach (var entry in _daoRegistry.EntryDao.FindAll())
                Entries.Add(new EntryVM(entry,
                    Subjects.First(subject => subject.Id == entry.Subject.Id),
                    Teachers.First(teacher => teacher.Id == entry.Teacher.Id)));
        }

        private void CollectionChanged<T, TVM>(DaoBase<T> dao, List<T> deletedList, List<T> newList,
            NotifyCollectionChangedEventArgs e) where TVM : VMBase<T>
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    dao.DeleteAll();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    deletedList.Add(((TVM) e.OldItems[0]).ModelObject);
                    newList.Remove(((TVM) e.OldItems[0]).ModelObject);
                    break;
                case NotifyCollectionChangedAction.Add:
                    newList.Add(((TVM) e.NewItems[0]).ModelObject);
                    break;
            }
        }

        private void Posts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged<Post, PostVM>(_daoRegistry.PostDao, DeletedPosts, NewPosts, e);
        }

        private void Loads_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged<Load, LoadVM>(_daoRegistry.LoadDao, DeletedLoads, NewLoads, e);
        }

        private void Teachers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged<Teacher, TeacherVM>(_daoRegistry.TeacherDao, DeletedTeachers, NewTeachers, e);
        }

        private void Subjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged<Subject, SubjectVM>(_daoRegistry.SubjectDao, DeletedSubjects, NewSubjects, e);
        }

        private void Entries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged<Entry, EntryVM>(_daoRegistry.EntryDao, DeletedEntries, NewEntries, e);
        }

        private void Settings_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged<Setting, SettingVM>(_daoRegistry.SettingDao, DeletedSettings, NewSettings, e);
        }

        private void Specialties_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged<Specialty, SpecialtyVM>(_daoRegistry.SpecialtyDao, DeletedSpecialties, NewSpecialties, e);
        }

        public void SaveChanges()
        {
            SaveEntries();
            SaveLoads();
            SavePosts();
            SaveTeachers();
            SaveSubjects();
            SaveSettings();
            SaveSpecialties();
        }

        private void SaveChanges<T, TVM>(DaoBase<T> dao, List<T> dataToDelete, List<T> DataToInsert,
            ObservableCollection<TVM> currentData) where TVM : VMBase<T>
        {
            dao.Delete(dataToDelete);
            dataToDelete.Clear();
            dao.Insert(DataToInsert);
            DataToInsert.Clear();
            foreach (var vm in currentData)
            {
                if (vm.IsChanged)
                    dao.Update(vm.ModelObject);
                vm.IsChanged = false;
            }
        }

        private void SaveLoads()
        {
            SaveChanges(_daoRegistry.LoadDao, DeletedLoads, NewLoads, Loads);
        }

        public void SaveSubjects()
        {
            SaveChanges(_daoRegistry.SubjectDao, DeletedSubjects, NewSubjects, Subjects);
        }

        public void SavePosts()
        {
            SaveChanges(_daoRegistry.PostDao, DeletedPosts, NewPosts, Posts);
        }

        public void SaveSettings()
        {
            SaveChanges(_daoRegistry.SettingDao, DeletedSettings, NewSettings, Settings);
        }

        public void SaveSpecialties()
        {
            SaveChanges(_daoRegistry.SpecialtyDao, DeletedSpecialties, NewSpecialties, Specialties);
        }

        public void SaveTeachers()
        {
            SaveChanges(_daoRegistry.TeacherDao, DeletedTeachers, NewTeachers, Teachers);
        }

        public void SaveEntries()
        {
            SaveChanges(_daoRegistry.EntryDao, DeletedEntries, NewEntries, Entries);
        }
    }
}
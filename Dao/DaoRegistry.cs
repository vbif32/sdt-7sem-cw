using Entities;

namespace Dao
{
    public class DaoRegistry
    {
        private EntryDao _entryDao;
        private LoadDao _loadDao;
        private PostDao _postDao;
        private SettingsDao _settingsDao;
        private SpecialtyDao _specialtyDao;
        private SubjectDao _subjectDao;
        private TeacherDao _teacherDao;

        public DaoRegistry(LiteDbModel model)
        {
            Model = model;
        }

        public LiteDbModel Model { get; }
        public EntryDao EntryDao => _entryDao ?? (_entryDao = new EntryDao(Model));
        public LoadDao LoadDao => _loadDao ?? (_loadDao = new LoadDao(Model));
        public PostDao PostDao => _postDao ?? (_postDao = new PostDao(Model));
        public SubjectDao SubjectDao => _subjectDao ?? (_subjectDao = new SubjectDao(Model));
        public TeacherDao TeacherDao => _teacherDao ?? (_teacherDao = new TeacherDao(Model));
        public SettingsDao SettingsDao => _settingsDao ?? (_settingsDao = new SettingsDao(Model));
        public SpecialtyDao SpecialtyDao => _specialtyDao ?? (_specialtyDao = new SpecialtyDao(Model));
    }
}
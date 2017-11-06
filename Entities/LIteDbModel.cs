using System;
using System.IO;
using LiteDB;

namespace Entities
{
    public class LiteDbModel : LiteDatabase
    {
        public LiteDbModel(string connectionString, BsonMapper mapper = null, Logger log = null) : base(
            connectionString, mapper, log)
        {
            AddReferences();
        }

        public LiteDbModel(ConnectionString connectionString, BsonMapper mapper = null, Logger log = null) : base(
            connectionString, mapper, log)
        {
            AddReferences();
        }

        public LiteDbModel(Stream stream, BsonMapper mapper = null, string password = null, bool disposeStream = false)
            : base(stream, mapper, password, disposeStream)
        {
            AddReferences();
        }

        public LiteDbModel(IDiskService diskService, BsonMapper mapper = null, string password = null,
            TimeSpan? timeout = null, int cacheSize = 5000, Logger log = null) : base(diskService, mapper, password,
            timeout, cacheSize, log)
        {
            AddReferences();
        }

        public LiteDbModel CreateModel()
        {
            var model = new LiteDbModel(@"Data\MyData.db");
            model.AddReferences();
            return model;
        }


        private void AddReferences()
        {
            Mapper.Entity<Запись>()
                .DbRef(x => x.Предмет, Предмет.CollectionName)
                .DbRef(x => x.Преподаватель, Преподаватель.CollectionName)
                .DbRef(x => x.ФактическаяНагрузка, Нагрузка.CollectionName);
            Mapper.Entity<Предмет>()
                .DbRef(x => x.ПлановаяНагрузка, Нагрузка.CollectionName);
            Mapper.Entity<Преподаватель>()
                .DbRef(x => x.Должность, Должность.CollectionName)
                .DbRef(x => x.Предметы, Предмет.CollectionName);
        }
    }
}
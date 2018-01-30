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

        public static LiteDbModel CreateModel()
        {
            LiteDbModel model;
            var path = @"MyData.db";
            try
            {
                model = new LiteDbModel(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                File.Delete(path);
                model = new LiteDbModel(path);
            }
            return model;
        }


        private void AddReferences()
        {
            Mapper.Entity<Должность>()
                .Id(x => x.Id);
            Mapper.Entity<Запись>()
                .Id(x => x.Id)
                .DbRef(x => x.Предмет, Предмет.CollectionName)
                .DbRef(x => x.Преподаватель, Преподаватель.CollectionName);
            Mapper.Entity<Нагрузка>()
                .Id(x => x.Id);
            Mapper.Entity<Предмет>()
                .Id(x => x.Id);
            Mapper.Entity<Преподаватель>()
                .Id(x => x.Id)
                .DbRef(x => x.Должность, Должность.CollectionName);
        }
    }
}
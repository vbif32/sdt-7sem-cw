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
            Mapper.Entity<Post>()
                .Id(x => x.Id);
            Mapper.Entity<Entry>()
                .Id(x => x.Id)
                .DbRef(x => x.Subject, Subject.CollectionName)
                .DbRef(x => x.Teacher, Teacher.CollectionName);
            Mapper.Entity<Load>()
                .Id(x => x.Id);
            Mapper.Entity<Subject>()
                .Id(x => x.Id);
            Mapper.Entity<Teacher>()
                .Id(x => x.Id)
                .DbRef(x => x.Post, Post.CollectionName);
            Mapper.Entity<Setting>()
                .Id(x => x.Id);
            Mapper.Entity<Specialty>()
                .Id(x => x.Id);
        }
    }
}
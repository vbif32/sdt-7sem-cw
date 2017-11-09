using System.Linq;
using LiteDB;

namespace ConsoleTests
{
    public class LibeDbReferenceTest
    {



        public void Test()
        {
            using (var db = new LiteDatabase("RefTest.db"))
            {
                //var mapper = BsonMapper.Global;

                //mapper.Entity<Преподаватель>()
                //    .Id(x => x.Id).DbRef(x => x.Должность, Должность.CollectionName);

                //mapper.Entity<Должность>()
                //    .Id(x => x.Id);

                var teachers = db.GetCollection<Преподаватель>(Преподаватель.CollectionName);
                //var posts = db.GetCollection<Должность>(Должность.CollectionName);

                //var newPost = new Должность
                //{
                //    Название = "асс.",
                //    ПолноеНазвание = "Ассистент",
                //    Часы = 123,
                //};

                //posts.Insert(newPost);
                //var post = posts.FindAll().First();

                //var newTeacher = new Преподаватель
                //{
                //    Фамилия = "Богорадникова",
                //    Имя = "Алиса",
                //    Отчество = "Викторовна",
                //    Ставка = 12,
                //    Должность = post,
                //};

                //teachers.Insert(newTeacher);

                var teacher = teachers.Include(x => x.Должность).FindAll().First();
            }

        }


        public class Должность
        {
            public const string CollectionName = "posts";
            public int Id { get; set; }
            public string Название { get; set; }
            public string ПолноеНазвание { get; set; }
            public int Часы { get; set; }
        }

        public class Преподаватель
        {
            public const string CollectionName = "teachers";

            public int Id { get; set; }

            public string ПолноеИОФ => $"{Имя} {Отчество} {Фамилия}";
            public string ПолноеФИО => $"{Фамилия} {Имя} {Отчество}";
            public string ФамилияИИнициалы => $"{Фамилия} {Имя.First()}. {Отчество.First()}.";

            public string Имя { get; set; }
            public string Отчество { get; set; }
            public string Фамилия { get; set; }
            public Должность Должность { get; set; }
            public float Ставка { get; set; }

            public string УченаяСтепеньПолная { get; set; }
            public string УченаяСтепень { get; set; }

        }
    }
}
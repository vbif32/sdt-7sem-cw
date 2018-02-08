using Entities;
using LiteDB;

namespace Dao
{
    public sealed class PostDao : DaoBase<Post>
    {
        public PostDao(LiteDbModel model) : base(model)
        {
            if (GetCollection().Count() == 0)
                InitialValues();
        }

        protected override LiteCollection<Post> GetCollection()
        {
            return _model.GetCollection<Post>(Post.CollectionName);
        }

        private void InitialValues()
        {
            Insert(new[]
            {
                new Post {LongName = "Ассистент", ShortName = "асс.", Hours = 880},
                new Post {LongName = "Старший преподаватель", ShortName = "ст. п.", Hours = 880},
                new Post {LongName = "Доцент", ShortName = "доц.", Hours = 810},
                new Post {LongName = "Профессор", ShortName = "проф.", Hours = 720},
                new Post {LongName = "Заведующий кафедрой", ShortName = "зав. каф.", Hours = 640}
            });
        }
    }
}
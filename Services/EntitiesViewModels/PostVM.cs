using Entities;

namespace Services.EntitiesViewModels
{
    public class PostVM : PropertyChangedBase
    {
        public PostVM(Post post)
        {
            Post = post;
        }

        public PostVM()
        {
            Post = new Post();
        }

        public Post Post { get; }

        public int Id
        {
            get => Post.Id;
            set => Post.Id = value;
        }

        public string LongName
        {
            get => Post.LongName;
            set => Post.LongName = value;
        }

        public string ShortName
        {
            get => Post.ShortName;
            set => Post.ShortName = value;
        }

        public int Hours
        {
            get => Post.Hours;
            set => Post.Hours = value;
        }
    }
}
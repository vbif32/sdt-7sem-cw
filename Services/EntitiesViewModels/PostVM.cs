using Entities;

namespace EntitiesViewModels
{
    public class PostVM : PropertyChangedBase
    {
        public PostVM(Должность post)
        {
            Post = post;
        }

        public PostVM()
        {
            Post = new Должность();
        }

        public Должность Post { get; }

        public int Id
        {
            get => Post.Id;
            set => Post.Id = value;
        }

        public string FullName
        {
            get => Post.ПолноеНазвание;
            set => Post.ПолноеНазвание = value;
        }

        public string Name
        {
            get => Post.Название;
            set => Post.Название = value;
        }

        public int Hours
        {
            get => Post.Часы;
            set => Post.Часы = value;
        }
    }
}
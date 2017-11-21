using Entities;

namespace WpfApp.EntitiesVM
{
    public class PostVM : PropertyChangedBase
    {
        public PostVM(Должность post) => _post = post;

        private readonly Должность _post;

        public int Id { get => _post.Id; set => _post.Id = value; }
        public string Name { get => _post.ПолноеНазвание; set => _post.ПолноеНазвание = value; }
        public string FullName { get => _post.Название; set => _post.Название = value; }
        public int Hours { get => _post.Часы; set => _post.Часы = value; }
    }
}
using Entities;

namespace Services.EntitiesViewModels
{
    public class PostVM : VMBase<Post>
    {
        public PostVM(Post post)
        {
            ModelObject = post;
        }

        public PostVM()
        {
            ModelObject = new Post();
        }

        public int Id
        {
            get => ModelObject.Id;
            set => ModelObject.Id = value;
        }

        public string LongName
        {
            get => ModelObject.LongName;
            set => ModelObject.LongName = value;
        }

        public string ShortName
        {
            get => ModelObject.ShortName;
            set => ModelObject.ShortName = value;
        }

        public int Hours
        {
            get => ModelObject.Hours;
            set => ModelObject.Hours = value;
        }
    }
}
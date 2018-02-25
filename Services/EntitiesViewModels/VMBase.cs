namespace Services.EntitiesViewModels
{
    public class VMBase<T> : PropertyChangedBase
    {
        public T ModelObject { get; set; }
    }
}
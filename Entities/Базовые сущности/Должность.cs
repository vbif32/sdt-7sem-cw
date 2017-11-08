namespace Entities
{
    public class Должность
    {
        public const string CollectionName = "posts";

        public int Id { get; set; }
        public string Название { get; set; }
        public string ПолноеНазвание { get; set; }
        public int Часы { get; set; }

        public override bool Equals(object должность)
        {

            return ПолноеНазвание == (должность as Должность)?.ПолноеНазвание;
        }
    }
}
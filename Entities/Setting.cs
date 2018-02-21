namespace Entities
{
    public class Setting
    {
        public const string CollectionName = "settings";
        public int Id { get; set; }
        public Settings Name { get; set; }
        public string Value { get; set; }

        public int IntValue() => int.Parse(Value);

        public float FloatValue() => float.Parse(Value);
    }
}
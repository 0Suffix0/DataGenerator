namespace DataGenerator_Core.Entites
{
    public sealed class Template
    {
        public int ID { get; set; }
        public string? Data { get; set; }
        public int TypeID { get; set; }
        public Type? Type { get; set; }
    }
}

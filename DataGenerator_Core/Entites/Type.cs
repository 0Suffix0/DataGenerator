namespace DataGenerator_Core.Entites
{
    public sealed class Type
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public List<Template> Templates { get; set; } = new(); 
    }
}

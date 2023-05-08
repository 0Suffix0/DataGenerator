namespace DataGenerator_Core.Entites
{
    public sealed class Column
    {
        public Type Type { get; set; }
        public string ColumnName { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}

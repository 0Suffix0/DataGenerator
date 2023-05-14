namespace DataGenerator_Core.Entites
{
    public sealed class Column
    {
        public string? Type { get; set; }
        public string? ColumnName { get; set; }
        public int From { get; set; } // Start interval
        public int To { get; set; } // End interval
    }
}

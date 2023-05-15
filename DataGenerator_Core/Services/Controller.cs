using DataGenerator_Core.Entites;

namespace DataGenerator_Core.Services
{
    public sealed class Controller
    {
        private readonly string? _connectionString;

        public Controller(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Starts generating test data
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="counter"></param>
        /// <returns>String converted to the selected format</returns>
        public string GenerateData(List<Column> columns, string toConvert, int counter)
        {
            if (columns == null) 
                throw new ArgumentNullException("Column is null");

            foreach(Column column in columns) 
                if (column.Type == null || column.ColumnName == null) 
                    throw new ArgumentException("Required fields is null");

            Generator generator = new (_connectionString);
            List<List<string>> generatorResult = generator.Start(columns, counter);

            Converter converter = new ();
            string ConvertResult = converter.toSQL(columns, generatorResult);

            return ConvertResult;
        }
    }
}

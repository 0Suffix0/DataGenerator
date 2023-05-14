using DataGenerator_Core.Entites;
using System.Collections;

namespace DataGenerator_Core.Services
{
    public sealed class Controller
    {
        public string Generator(List<Column> columns, string toConvert, int counter)
        {
            if (columns == null) 
                throw new ArgumentNullException("Column is null");

            foreach(Column column in columns) 
                if (column.Type == null || column.ColumnName == null) 
                    throw new ArgumentException("Required fields is null");

            Generator generator = new ();
            List<List<string>> generatorResult = generator.Start(columns, counter);

            Converter converter = new ();
            string ConvertResult = converter.toSQL(columns, generatorResult);

            return ConvertResult;
        }
    }
}

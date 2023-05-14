using DataGenerator_Core.Entites;
using DataGenerator_Core.Services;

namespace DataGenerator_CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Column firstColumn = new Column();
            firstColumn.ColumnName = "FirstAge";
            firstColumn.Type = "NumberRange";
            firstColumn.From = 20;
            firstColumn.To = 40;

            Column secondColumn = new Column();
            secondColumn.ColumnName = "DoubleRange";
            secondColumn.Type = "DoubleRange";
            secondColumn.From = 5;
            secondColumn.To = 10;

            List<Column> columns = new List<Column>();
            columns.Add(firstColumn);
            columns.Add(secondColumn);

            Controller controller = new ();

            string result = controller.Generator(columns, "SQL", 5);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
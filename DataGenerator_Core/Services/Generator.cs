using DataGenerator_Core.Entites;
using System.Diagnostics.Metrics;

namespace DataGenerator_Core.Services
{
    public sealed class Generator
    {
        Random random = new ();

        public List<List<string>> Start(List<Column> columns, int counter)
        {
            List<List<string>> results = new();

            for (int i = 0; i < counter; i++)
            {
                List<string> rows = new();

                foreach (Column column in columns)
                {
                    switch (column.Type)
                    {
                        case "NumberRange":
                            rows.Add(NumberRange(column.From, column.To).ToString());
                            break;
                        case "DoubleRange":
                            rows.Add(DoubleRange(column.To).ToString());
                            break;
                        case "StringRange":
                            rows.Add(StringRange(column.From, column.To));
                            break;
                        case "DateTime":
                            rows.Add(DateTime(column.From, column.To).ToString());
                            break;
                    }
                }

                results.Add(rows);
            }

            return results;
        }

        private string City()
        {
            throw new NotImplementedException();
        }

        private string Country()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate random datetime
        /// </summary>
        /// <returns>Random datetime from START to END years</returns>
        private string DateTime(int start, int end)
        {
            return new DateTime(random.Next(start, end), random.Next(1, 12), random.Next(1,30)).ToShortDateString();
        }

        private string Email()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate random double
        /// </summary>
        /// <param name="end"></param>
        /// <returns>Random double from 0 to END</returns>
        private double DoubleRange(double end)
        {
            return (random.NextDouble() * end);
        }

        private string Name()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate random integer
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Random integer from START to END</returns>
        private int NumberRange(int start, int end)
        {
            return random.Next(start, end);
        }

        private string Phone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate random string
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Random string FROM start TO end</returns>
        private string StringRange(int start, int end)
        {
            string _string = string.Empty;

            for (int i = 0; i < random.Next(start, end); i++)
                _string += Convert.ToString(Convert.ToChar(random.Next(65, 90)));

            return _string;
        }
    }
}

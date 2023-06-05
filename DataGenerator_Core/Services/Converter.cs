using DataGenerator_Core.Entites;

namespace DataGenerator_Core.Services
{
    public sealed class Converter
    {
        /// <summary>
        /// Convert data to SQL
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="generatorResults"></param>
        /// <returns>SQL "INSERT INTO VALUES" string</returns>
        public string toSQL(IEnumerable<Column> columns, IEnumerable<IEnumerable<string>> generatorResults)
        {
            string sqlHead = toSQLHeader(columns);
            string sqlBody = toSQLBody(generatorResults);

            return sqlHead + sqlBody;
        }

        /// <summary>
        /// Generate "INSERT INTO" string
        /// </summary>
        /// <param name="columns"></param>
        /// <returns>string "INSERT INTO" with columns</returns>
        private string toSQLHeader(IEnumerable<Column> columns)
        {
            string sqlHead = "INSERT INTO `TABLENAME` (";

            foreach (Column column in columns)
            {
                sqlHead += column.ColumnName + ", ";
            }

            return sqlHead.Remove(sqlHead.Length - 2) + ")\n";
        }

        /// <summary>
        /// Generate "VALUES" string
        /// </summary>
        /// <param name="generatorResults"></param>
        /// <returns>string "VALUES" with data</returns>
        private string toSQLBody(IEnumerable<IEnumerable<string>> generatorResults)
        {
            string sqlBody = "VALUES ";

            foreach(IEnumerable<string> results in generatorResults)
            {
                string sqlres = "(";

                foreach (string result in results)
                {
                    try
                    {
                        Convert.ToInt32(result);
                        sqlres += result + ", ";
                    }
                    catch
                    {
                        sqlres += "\'" + result + "\'" + ", ";
                    }
                }

                sqlBody += sqlres.Remove(sqlres.Length - 2) + "),\n";
            }

            return sqlBody.Remove(sqlBody.Length - 2) + ";";
        }
    }
}
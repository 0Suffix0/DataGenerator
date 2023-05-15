using DataGenerator_Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace DataGenerator_Core.Services
{
    public sealed class Generator
    {
        Random random = new ();
        private DatabaseContext _databaseContext;

        public Generator(string connectionString)
        {
            _databaseContext = new DatabaseContext(connectionString);
        }

        /// <summary>
        /// Starts generating test data
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="counter"></param>
        /// <returns>List within a List where the second one stores the strings like (int, "string")</returns>
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
                        case "Name":
                            rows.Add(Name());
                            break;
                        case "City":
                            rows.Add(City());
                            break;
                        case "Country":
                            rows.Add(Country());
                            break;
                        case "Email":
                            rows.Add(Email());
                            break;
                        case "Phone":
                            rows.Add(Phone());
                            break;
                    }
                }

                results.Add(rows);
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>One random template where type is a city</returns>
        private string City()
        {
            List<Template> city = (from Template in _databaseContext.Templates.Include(t => t.Type)
                                    where Template.Type.Name == "City"
                                    select Template).ToList();

            return city[new Random().Next(city.Count)].Data.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>One random template where type is a country</returns>
        private string Country()
        {
            List<Template> country = (from Template in _databaseContext.Templates.Include(t => t.Type)
                                    where Template.Type.Name == "Country"
                                    select Template).ToList();

            return country[new Random().Next(country.Count)].Data.ToString();
        }

        /// <summary>
        /// Generate random datetime
        /// </summary>
        /// <returns>Random datetime from START to END years</returns>
        private string DateTime(int start, int end)
        {
            return new DateTime(random.Next(start, end), random.Next(1, 12), random.Next(1,30)).ToShortDateString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>One random template where type is a email</returns>
        private string Email()
        {
            List<Template> email = (from Template in _databaseContext.Templates.Include(t => t.Type)
                                   where Template.Type.Name == "Email"
                                   select Template).ToList();

            return email[new Random().Next(email.Count)].Data.ToString();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>One random template where type is a name</returns>
        private string Name()
        {
            List<Template> name = (from Template in _databaseContext.Templates.Include(t => t.Type) 
                                   where Template.Type.Name == "Name"
                                   select Template).ToList();

            return name[new Random().Next(name.Count)].Data.ToString();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>One random template where type is a phone</returns>
        private string Phone()
        {
            List<Template> phone = (from Template in _databaseContext.Templates.Include(t => t.Type)
                                   where Template.Type.Name == "Phone"
                                   select Template).ToList();

            return phone[new Random().Next(phone.Count)].Data.ToString();
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

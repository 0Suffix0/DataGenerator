using DataGenerator_Core.Entites;
using DataGenerator_Core.Services;

namespace DataGenerator_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
            Controller controller = new Controller(_connectionString);

            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.Run(async (context) =>
            {
                var response = context.Response;
                var request = context.Request;
                if (request.Path == "/api/DataGenerator/")
                {
                    string ConvertTo = context.Request.Query["ConvertTo"];
                    int count = Convert.ToInt32(context.Request.Query["count"]);
                    List<Column> columns = new List<Column>();

                    try
                    {
                        columns = await request.ReadFromJsonAsync<List<Column>>();
                    }
                    catch
                    {
                        columns.Add(await request.ReadFromJsonAsync<Column>());
                    }

                    if (columns != null)
                    {
                        var result = controller.GenerateData(columns, ConvertTo, count);
                        await context.Response.WriteAsync(result);
                    }
                }
            });

            app.Run("http://localhost:3000");
        }
    }
}
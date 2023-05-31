
using DataGenerator_Core;
using Microsoft.EntityFrameworkCore;

namespace DataGenerator_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            string connection = System.Configuration.ConfigurationManager.AppSettings.Get("connectionString");
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
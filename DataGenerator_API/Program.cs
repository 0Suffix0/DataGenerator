using DataGenerator_Core;
using DataGenerator_Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DataGenerator_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // TODO: Перенести в appsettings.Development.json
            string connection = System.Configuration.ConfigurationManager.AppSettings.Get("connectionString");
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddScoped<Generator>();
            builder.Services.AddSingleton<Converter>();
            builder.Services.AddScoped<TemplateService>();
            builder.Services.AddScoped<TypeService>();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
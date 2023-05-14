using DataGenerator_Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace DataGenerator_Core
{
    public sealed class DatabaseContext : DbContext
    { 
        private readonly string? connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("connectionString");

        public DbSet<Entites.Type> Types { get; set; } = null!;
        public DbSet<Template> Templates { get; set; } = null!;

        public DatabaseContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}

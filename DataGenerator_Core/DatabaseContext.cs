using DataGenerator_Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace DataGenerator_Core
{
    public sealed class DatabaseContext : DbContext
    {
        private readonly string? _connectionString;

        public DbSet<Entites.Type> Types { get; set; } = null!;
        public DbSet<Template> Templates { get; set; } = null!;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }
    }
}

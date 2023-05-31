using DataGenerator_Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace DataGenerator_Core
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<Entites.Type> Types { get; set; } = null!;
        public DbSet<Template> Templates { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework
{
    // Add-Migration InitialCreate -Project Infrastructure -StartupProject Web -Context MyEventSourcingDbContext -OutputDir Persistence/EntityFramework/MyEventSourcingDbContextMigrations
    // Update-Database -Project Infrastructure -StartupProject Web -Context MyEventSourcingDbContext
    public class MyEventSourcingDbContext : DbContext
    {
        public MyEventSourcingDbContext(DbContextOptions<MyDataDbContext> options) : base(options)
        {
        }

        public DbSet<EventData> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}

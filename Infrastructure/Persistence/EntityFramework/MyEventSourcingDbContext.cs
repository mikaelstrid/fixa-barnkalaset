using Microsoft.EntityFrameworkCore;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework
{
    // Add-Migration InitialCreate -Project Infrastructure -StartupProject Web -Context MyEventSourcingDbContext -OutputDir Persistence/EntityFramework/MyEventSourcingDbContextMigrations
    // Update-Database -Project Infrastructure -StartupProject Web -Context MyEventSourcingDbContext
    public class MyEventSourcingDbContext : DbContext
    {
        public MyEventSourcingDbContext(DbContextOptions<MyEventSourcingDbContext> options) : base(options) { }

        public virtual DbSet<EventData> Events { get; set; }
    }
}

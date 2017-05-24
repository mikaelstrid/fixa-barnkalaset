using Microsoft.EntityFrameworkCore;
using Pixel.FixaBarnkalaset.Core;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework
{
    // Add-Migration InitialCreate -Project Infrastructure -StartupProject Web -Context MyDataDbContext -OutputDir Persistence/EntityFramework/MyDataDbContextMigrations
    // Update-Database -Project Infrastructure -StartupProject Web
    // Remove-Migration
    public class MyDataDbContext : DbContext
    {
        public MyDataDbContext(DbContextOptions<MyDataDbContext> options) : base(options)
        {
        }

        public DbSet<Arrangement> Arrangements { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasKey(c => c.Slug);

            modelBuilder.Entity<Arrangement>()
                .HasOne(a => a.City)
                .WithMany(c => c.Arrangements)
                .HasForeignKey(a => a.CitySlug)
                .HasPrincipalKey(c => c.Slug);
        }
    }
}

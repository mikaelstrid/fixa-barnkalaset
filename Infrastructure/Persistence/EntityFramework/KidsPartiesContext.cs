using Microsoft.EntityFrameworkCore;
using Pixel.Kidsparties.Core;

namespace Pixel.Kidsparties.Infrastructure.Persistence.EntityFramework
{
    // Add-Migration InitialCreate -Project Infrastructure -StartupProject Web -Context KidsPartiesContext
    // Update-Database -Project Infrastructure -StartupProject Web
    // Remove-Migration
    public class KidsPartiesContext : DbContext
    {
        public KidsPartiesContext(DbContextOptions<KidsPartiesContext> options) : base(options)
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

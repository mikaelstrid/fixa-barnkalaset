using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pixel.Kidsparties.Infrastructure.Identity;

namespace Pixel.Kidsparties.Infrastructure.Persistence.EntityFramework
{
    // Add-Migration InitialCreate -Project Infrastructure -StartupProject Web -Context ApplicationDbContext -OutputDir Persistence/EntityFramework/ApplicationDbContextMigrations
    // Update-Database -Project Infrastructure -StartupProject Web
    // Remove-Migration
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pixel.FixaBarnkalaset.Infrastructure.Identity;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework
{
    // Add-Migration InitialCreate -Project Infrastructure -StartupProject Web -Context MyIdentityDbContext -OutputDir Persistence/EntityFramework/ApplicationDbContextMigrations
    // Update-Database -Project Infrastructure -StartupProject Web
    // Remove-Migration
    public class MyIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options)
            : base(options)
        {
        }
    }
}

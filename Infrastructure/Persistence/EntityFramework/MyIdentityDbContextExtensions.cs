using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pixel.FixaBarnkalaset.Infrastructure.Identity;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework
{
    // https://github.com/rowanmiller/UnicornStore/blob/master/UnicornStore/src/UnicornStore/Models/Identity/IdentityExtensions.cs
    public static class MyIdentityDbContextExtensions
    {
        public static void EnsureRolesCreated(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<MyIdentityDbContext>();

            if (!context.AllMigrationsApplied()) return;

            var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
            foreach (var role in Roles.All)
            {
                if (!roleManager.RoleExistsAsync(role.ToUpper()).Result)
                {
                    roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }
        }
    }
}

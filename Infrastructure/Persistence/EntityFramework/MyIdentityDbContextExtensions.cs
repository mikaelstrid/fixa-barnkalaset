using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Pixel.FixaBarnkalaset.Infrastructure.Identity;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework
{
    // https://github.com/rowanmiller/UnicornStore/blob/master/UnicornStore/src/UnicornStore/Models/Identity/IdentityExtensions.cs
    public static class MyIdentityDbContextExtensions
    {
        public static void EnsureRolesCreated(this IApplicationBuilder app, string envEnvironmentName)
        {
            // Had to disable this due to a problem in .NET Core 2.0
            if (envEnvironmentName != "Testing")
            {
                var context = app.ApplicationServices.GetService<MyIdentityDbContext>();
                if (!context.AllMigrationsApplied()) return;
            }

            var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
            foreach (var role in Roles.All)
            {
                if (!roleManager.RoleExistsAsync(role.ToUpper()).Result)
                {
                    roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }
        }

        public static void AddTestingUsers(this IApplicationBuilder app)
        {
            var email = "test@test.com";
            var nameIdentifier = "123";
            var givenName = "Test";
            var surname = "Testsson";
            var password = "B1pdsosp!";

            var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                NameIdentifier = nameIdentifier,
                Name = $"{givenName} {surname}",
                GivenName = givenName,
                Surname = surname
            };
            var result = userManager.CreateAsync(user, password).Result;
            userManager.AddToRoleAsync(user, Roles.Admin).Wait();
        }
    }
}

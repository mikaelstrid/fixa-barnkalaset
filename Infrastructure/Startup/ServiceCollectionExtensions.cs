using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Identity;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories;

namespace Pixel.FixaBarnkalaset.Infrastructure.Startup
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<KidsPartiesContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ICityRepository, SqlCityRepository>();
            services.AddTransient<IArrangementRepository, SqlArrangementRepository>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
                {
                    o.Cookies.ApplicationCookie.LoginPath = new PathString("/konto/logga-in");
                    o.Cookies.ApplicationCookie.LogoutPath = new PathString("/konto/logga-ut");
                    o.Cookies.ApplicationCookie.AccessDeniedPath = new PathString("/konto/atkomst-nekad");
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}

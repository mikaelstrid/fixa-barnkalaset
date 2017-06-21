using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Core.Services;
using Pixel.FixaBarnkalaset.Infrastructure.Identity;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories;

namespace Pixel.FixaBarnkalaset.Infrastructure.Startup
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MyDataDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<MyIdentityDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<MyEventSourcingDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ISettings, Settings>();
            services.AddTransient<IAggregateFactory, AggregateFactory>();
            services.AddTransient<ICityRepository, SqlCityRepository>();
            services.AddTransient<IArrangementRepository, SqlArrangementRepository>();
            services.AddTransient<IRepository, SqlServerRepository>();
            services.AddTransient<ICityService, CityService>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
                {
                    o.Cookies.ApplicationCookie.LoginPath = new PathString("/konto/logga-in");
                    o.Cookies.ApplicationCookie.LogoutPath = new PathString("/konto/logga-ut");
                    o.Cookies.ApplicationCookie.AccessDeniedPath = new PathString("/konto/atkomst-nekad");
                })
                .AddEntityFrameworkStores<MyIdentityDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}

using System.Globalization;
using AutoMapper;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Core.Services;
using Pixel.FixaBarnkalaset.Infrastructure.Identity;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories;

namespace Pixel.FixaBarnkalaset.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();

                TelemetryConfiguration.Active.DisableTelemetry = true;
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            ConfigureServicesDatabase(services, _env, Configuration);
            services.AddTransient<ICityRepository, SqlCityRepository>();

            if (!_env.IsEnvironment("Testing"))
            {
                services.AddIdentity<ApplicationUser, IdentityRole>(o =>
                {
                    o.Cookies.ApplicationCookie.LoginPath = new PathString("/konto/logga-in");
                    o.Cookies.ApplicationCookie.LogoutPath = new PathString("/konto/logga-ut");
                    o.Cookies.ApplicationCookie.AccessDeniedPath = new PathString("/konto/atkomst-nekad");
                })
                    .AddEntityFrameworkStores<MyIdentityDbContext>()
                    .AddDefaultTokenProviders();
#if DEBUG
                services.AddMvc(options =>
                {
                    options.SslPort = 44369;
                    options.Filters.Add(new RequireHttpsAttribute());
                });
#else
            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
#endif
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                services.AddTransient<ISettings, Settings>();
                services.AddTransient<IAggregateFactory, AggregateFactory>();
                services.AddTransient<IArrangementRepository, SqlArrangementRepository>();
                services.AddTransient<IAggregateRepository, SqlServerAggregateRepository>();
                services.AddTransient<ICityService, CityService>();
            }
            else
            {
                services.AddMvc();
            }
        }

        private static void ConfigureServicesDatabase(IServiceCollection services, IHostingEnvironment env, IConfigurationRoot configuration)
        {
            if (env.IsEnvironment("Testing"))
            {
                // The database DI is configured in the TestFixture
            }
            else
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<MyDataDbContext>(options => options.UseSqlServer(connectionString));
                services.AddDbContext<MyIdentityDbContext>(options => options.UseSqlServer(connectionString));
                services.AddDbContext<MyEventSourcingDbContext>(options => options.UseSqlServer(connectionString));
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var defaultCulture = new CultureInfo("sv-SE");
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            if (!env.IsEnvironment("Testing"))
            {
                using (
                    var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<MyIdentityDbContext>().Database.Migrate();

                    serviceScope.ServiceProvider.GetService<MyDataDbContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<MyDataDbContext>().EnsureSeedData();

                    serviceScope.ServiceProvider.GetService<MyEventSourcingDbContext>().Database.Migrate();
                }

                app.UseIdentity();
                app.UseFacebookAuthentication(new FacebookOptions()
                {
                    AppId = Configuration["Authentication:Facebook:AppId"],
                    AppSecret = Configuration["Authentication:Facebook:AppSecret"]
                });
                app.EnsureRolesCreated();
            }

            app.UseStaticFiles();
            
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "city",
            //        template: "stad/{controller=City}/{action=Index}/{id?}");
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseMvc(routes =>
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}")
            );
        }
    }
}

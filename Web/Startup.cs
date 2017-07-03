﻿using System.Globalization;
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
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Core.Services;
using Pixel.FixaBarnkalaset.Infrastructure;
using Pixel.FixaBarnkalaset.Infrastructure.Identity;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Messaging;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories;
using Pixel.FixaBarnkalaset.Infrastructure.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

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
            }

            if (env.IsDevelopment() || env.IsEnvironment("Testing"))
            {
                TelemetryConfiguration.Active.DisableTelemetry = true;
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesDatabase(services, _env, Configuration);
            ConfigureServicesMvc(services, _env);
            ConfigureServicesIdentity(services, _env);
            ConfigureServicesApplication(services, _env);
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

        private static void ConfigureServicesMvc(IServiceCollection services, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                services.AddMvc(options =>
                {
                    options.SslPort = 44369;
                    options.Filters.Add(new RequireHttpsAttribute());
                });
            if (env.IsEnvironment("Testing"))
                services.AddMvc();
            else
                services.AddMvc(options => { options.Filters.Add(new RequireHttpsAttribute()); });
        }

        private static void ConfigureServicesIdentity(IServiceCollection services, IHostingEnvironment env)
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

        private static void ConfigureServicesApplication(IServiceCollection services, IHostingEnvironment env)
        {
            services.AddAutoMapper();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IAggregateFactory, AggregateFactory>();
            services.AddTransient<IAggregateRepository, SqlServerAggregateRepository>();
            services.AddTransient<IArrangementRepository, SqlArrangementRepository>();
            services.AddTransient<ICityRepository, SqlCityRepository>();
            services.AddTransient<IProjectionRegistry, ProjectionRegistry>();
            services.AddSingleton<IEventPublisher, EventPublisher>();

            if (!env.IsEnvironment("Testing"))
            {
                var inMemoryViewRepository = new InMemoryViewRepository();
                services.AddSingleton<IViewRepository>(inMemoryViewRepository);
                services.AddSingleton<ISlugDictionary>(inMemoryViewRepository);
            }
        }



        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IEventPublisher eventPublisher)
        {
            ConfigureLogging(loggerFactory, Configuration);

            ConfigureCulture();

            if (env.IsDevelopment() || env.IsEnvironment("Testing"))
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
                ConfigureDatabaseMigration(app);
            }

            ConfigureIdentity(app, _env, Configuration);
            app.UseStaticFiles();
            app.UseMvc(routes => routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"));

            //app.ApplicationServices.GetRequiredService<CustomCookieAuthenticationEvents>()
            eventPublisher.CatchUp(0);
        }

        private static void ConfigureLogging(ILoggerFactory loggerFactory, IConfigurationRoot configuration)
        {
            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
        }

        private static void ConfigureCulture()
        {
            var defaultCulture = new CultureInfo("sv-SE");
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
        }

        private static void ConfigureDatabaseMigration(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<MyIdentityDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetService<MyDataDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetService<MyDataDbContext>().EnsureSeedData();
                serviceScope.ServiceProvider.GetService<MyEventSourcingDbContext>().Database.Migrate();
            }
        }

        private static void ConfigureIdentity(IApplicationBuilder app, IHostingEnvironment env, IConfigurationRoot configuration)
        {
            app.UseIdentity();

            app.EnsureRolesCreated();
            if (env.IsEnvironment("Testing"))
            {
                app.AddTestingUsers();
            }

            if (!env.IsEnvironment("Testing"))
            {
                app.UseFacebookAuthentication(new FacebookOptions()
                {
                    AppId = configuration["Authentication:Facebook:AppId"],
                    AppSecret = configuration["Authentication:Facebook:AppSecret"]
                });
            }
        }
    }
}

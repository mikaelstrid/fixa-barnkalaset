using System.Globalization;
using AutoMapper;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.Infrastructure.Startup;

namespace Pixel.FixaBarnkalaset.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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
            services.ConfigureAuthentication();

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

            services.AddAutoMapper();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddEntityFramework(connectionString);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddApplicationServices();
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

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<MyIdentityDbContext>().Database.Migrate();

                serviceScope.ServiceProvider.GetService<MyDataDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetService<MyDataDbContext>().EnsureSeedData();

                serviceScope.ServiceProvider.GetService<MyEventSourcingDbContext>().Database.Migrate();
            }

            app.UseStaticFiles();

            app.UseAuthentication(Configuration);
            
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

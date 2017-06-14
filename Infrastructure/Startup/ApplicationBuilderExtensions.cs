using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Startup
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseAuthentication(this IApplicationBuilder app, IConfigurationRoot configuration)
        {
            app.UseIdentity();
            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = configuration["Authentication:Facebook:AppId"],
                AppSecret = configuration["Authentication:Facebook:AppSecret"]
            });
            app.EnsureRolesCreated();
        }
    }
}

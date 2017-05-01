using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Pixel.Kidsparties.Infrastructure
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
        }
    }
}

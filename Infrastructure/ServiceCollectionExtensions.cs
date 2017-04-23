using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<KidsPartiesContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            //services.AddSingleton<ICityRepository, DummyCityRepository>();
            //services.AddSingleton<IArrangementRepository, DummyArrangementRepository>();
            services.AddScoped<ICityRepository, SqlCityRepository>();
            services.AddScoped<IArrangementRepository, SqlArrangementRepository>();
        }
    }
}

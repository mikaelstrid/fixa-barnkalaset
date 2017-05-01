using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pixel.Kidsparties.Core.Interfaces;
using Pixel.Kidsparties.Infrastructure.Persistence;
using Pixel.Kidsparties.Infrastructure.Persistence.EntityFramework;
using Pixel.Kidsparties.Infrastructure.Persistence.Repositories;

namespace Pixel.Kidsparties.Infrastructure.Startup
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
    }
}

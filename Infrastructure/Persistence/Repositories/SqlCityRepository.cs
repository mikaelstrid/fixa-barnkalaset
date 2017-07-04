using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public class SqlCityRepository : ICityRepository
    {
        private readonly MyDataDbContext _dbContext;
        private readonly ILogger _logger;

        public SqlCityRepository(MyDataDbContext dbContext, ILogger<SqlCityRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            _logger.LogDebug("GetAll: Get all cities");
            return await _dbContext.Cities.Include(c => c.Arrangements).ToListAsync();
        }

        public async Task<City> GetBySlug(string slug)
        {
            _logger.LogDebug("GetBySlug: Get city with slug {Slug}", slug);
            return await _dbContext.Cities
                .Include(c => c.Arrangements)
                .SingleOrDefaultAsync(c => c.Slug == slug);
        }

        public async Task AddOrUpdate(City city)
        {
            _logger.LogDebug("AddOrUpdate: Adding or updating city with slug {Slug} with data {City}", city.Slug, JsonConvert.SerializeObject(city));
            var existingCity = await GetBySlug(city.Slug);
            if (existingCity != null)
            {
                existingCity.Name = city.Name;
                existingCity.Latitude = city.Latitude;
                existingCity.Longitude = city.Longitude;
                _dbContext.Update(existingCity);
                _logger.LogInformation("AddOrUpdate: Updated city with slug {Slug} with data {City}", city.Slug, JsonConvert.SerializeObject(city));
            }
            else
            {
                await _dbContext.Cities.AddAsync(city);
                _logger.LogInformation("AddOrUpdate: Added city with slug {Slug} with data {City}", city.Slug, JsonConvert.SerializeObject(city));
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(string slug)
        {
            _logger.LogDebug("Remove: Removing city with slug {Slug}", slug);
            var city = await GetBySlug(slug);
            if (city == null)
            {
                _logger.LogInformation("Remove: City with slug {Slug} not found, continue", slug);
                return;
            }
            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Remove: City with slug {Slug} removed", slug);
        }
    }
}

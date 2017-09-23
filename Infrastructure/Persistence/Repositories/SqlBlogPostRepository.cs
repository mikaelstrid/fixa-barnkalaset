using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public class SqlBlogPostRepository : IBlogPostRepository
    {
        private readonly MyDataDbContext _dbContext;
        private readonly ILogger _logger;

        public SqlBlogPostRepository(MyDataDbContext dbContext, ILogger<SqlCityRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<BlogPost>> GetAll()
        {
            _logger.LogDebug("GetAll: Get all blog posts");
            return await _dbContext.BlogPosts.ToListAsync();
        }

        //public async Task<City> GetById(int id)
        //{
        //    _logger.LogDebug("GetById: Get city with id {Id}", id);
        //    return await _dbContext.Cities
        //        .Include(c => c.Arrangements)
        //        .SingleOrDefaultAsync(c => c.Id == id);
        //}

        //public async Task<City> GetBySlug(string slug)
        //{
        //    _logger.LogDebug("GetBySlug: Get city with slug {Slug}", slug);
        //    return await _dbContext.Cities
        //        .Include(c => c.Arrangements)
        //        .SingleOrDefaultAsync(c => c.Slug == slug);
        //}

        //public async Task AddOrUpdate(City city)
        //{
        //    var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        //    _logger.LogDebug("AddOrUpdate: Adding or updating city with id {Id} with data {City}", city.Id, JsonConvert.SerializeObject(city, settings));
        //    var existingCity = await GetById(city.Id);
        //    if (existingCity != null)
        //    {
        //        existingCity.Name = city.Name;
        //        existingCity.Latitude = city.Latitude;
        //        existingCity.Longitude = city.Longitude;
        //        _dbContext.Update(existingCity);
        //        _logger.LogInformation("AddOrUpdate: Updated city with id {Id} with data {City}", city.Id, JsonConvert.SerializeObject(city, settings));
        //    }
        //    else
        //    {
        //        await _dbContext.Cities.AddAsync(city);
        //        _logger.LogInformation("AddOrUpdate: Added city with id {Id} with data {City}", city.Id, JsonConvert.SerializeObject(city, settings));
        //    }
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task Remove(int id)
        //{
        //    _logger.LogDebug("Remove: Removing city with id {Id}", id);
        //    var city = await GetById(id);
        //    if (city == null)
        //    {
        //        _logger.LogInformation("Remove: City with id {Id} not found, continue", id);
        //        return;
        //    }
        //    _dbContext.Cities.Remove(city);
        //    await _dbContext.SaveChangesAsync();
        //    _logger.LogInformation("Remove: City with id {Id} removed", id);
        //}
    }
}

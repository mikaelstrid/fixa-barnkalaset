using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public class SqlCityRepository : ICityRepository
    {
        private readonly MyDataDbContext _dbContext;

        public SqlCityRepository(MyDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<City> GetAll()
        {
            return _dbContext.Cities.Include(c => c.Arrangements).AsEnumerable();
        }

        public City GetBySlug(string citySlug)
        {
            return _dbContext.Cities
                .Include(c => c.Arrangements)
                .SingleOrDefault(c => c.Slug == citySlug);
        }

        public void AddOrUpdate(City city)
        {
            var existingCity = GetBySlug(city.Slug);
            if (existingCity != null)
            {
                existingCity.Name = city.Name;
                existingCity.Latitude = city.Latitude;
                existingCity.Longitude = city.Longitude;
                _dbContext.Update(existingCity);
            }
            else
            {
                _dbContext.Cities.Add(city);
            }
            _dbContext.SaveChanges();
        }
    }
}

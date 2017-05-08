using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;
using Pixel.Kidsparties.Infrastructure.Persistence.EntityFramework;

namespace Pixel.Kidsparties.Infrastructure.Persistence.Repositories
{
    public class SqlCityRepository : ICityRepository
    {
        private readonly KidsPartiesContext _dbContext;

        public SqlCityRepository(KidsPartiesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<City> GetAll()
        {
            return _dbContext.Cities.AsEnumerable();
        }

        public City GetBySlug(string citySlug)
        {
            return _dbContext.Cities
                .Include(c => c.Arrangements)
                .SingleOrDefault(c => c.Slug == citySlug);
        }
    }
}

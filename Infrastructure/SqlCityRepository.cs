using System.Collections.Generic;
using System.Linq;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Infrastructure
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
            return _dbContext.Cities.SingleOrDefault(c => c.Slug == citySlug);
        }
    }
}

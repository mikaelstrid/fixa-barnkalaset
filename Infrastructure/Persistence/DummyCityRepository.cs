using System.Collections.Generic;
using System.Linq;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Infrastructure.Persistence
{
    public class DummyCityRepository : ICityRepository
    {
        private readonly List<City> _cities;

        public DummyCityRepository()
        {
            _cities = new List<City>
            {
                new City
                {
                    Slug = "halmstad",
                    Name = "Halmstad",
                    Latitude = 56.6706614,
                    Longitude = 12.7579992
                },
                new City
                {
                    Slug = "goteborg",
                    Name = "Göteborg",
                    Latitude = 57.7018796,
                    Longitude = 11.7536028
                },
                new City
                {
                    Slug = "malmo",
                    Name = "Malmö",
                    Latitude = 55.5708212,
                    Longitude = 12.9500712
                },
                new City
                {
                    Slug = "vaxjo",
                    Name = "Växjö",
                    Latitude = 56.8894262,
                    Longitude = 14.7241027
                }
            };
        }

        public IEnumerable<City> GetAll()
        {
            return _cities;
        }

        public City GetBySlug(string citySlug)
        {
            return _cities.SingleOrDefault(c => c.Slug == citySlug);
        }
    }
}

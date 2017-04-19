using System.Collections.Generic;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Infrastructure
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
                    Id = 1,
                    Name = "Halmstad",
                    Slug = "halmstad",
                    Latitude = 56.6706614m,
                    Longitude = 12.7579992m
                },
                new City
                {
                    Id = 2,
                    Name = "Göteborg",
                    Slug = "goteborg",
                    Latitude = 57.7018796m,
                    Longitude = 11.7536028m
                },
                new City
                {
                    Id = 3,
                    Name = "Malmö",
                    Slug = "malmo",
                    Latitude = 55.5708212m,
                    Longitude = 12.9500712m
                },
                new City
                {
                    Id = 4,
                    Name = "Växjö",
                    Slug = "vaxjo",
                    Latitude = 56.8894262m,
                    Longitude = 14.7241027m
                }
            };

        }

        public IEnumerable<City> GetAll()
        {
            return _cities;
        }
    }
}

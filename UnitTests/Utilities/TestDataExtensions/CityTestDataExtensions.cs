using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Core;

namespace UnitTests.Utilities.TestDataExtensions
{
    public static class CityTestDataExtensions
    {
        public static City Halmstad(this City city)
        {
            city.Name = "Halmstad";
            city.Slug = "halmstad";
            city.Latitude = 56.6706614;
            city.Longitude = 12.7579992;
            return city;
        }

        public static City Goteborg(this City city)
        {
            city.Name = "Göteborg";
            city.Slug = "goteborg";
            city.Latitude = 57.7018796;
            city.Longitude = 11.7536028;
            return city;
        }

        public static City Malmo(this City city)
        {
            city.Name = "Malmö";
            city.Slug = "malmo";
            city.Latitude = 55.5708212;
            city.Longitude = 12.9500712;
            return city;
        }

        public static City Vaxjo(this City city)
        {
            city.Name = "Växjö";
            city.Slug = "vaxjo";
            city.Latitude = 56.8894262;
            city.Longitude = 14.7241027;
            return city;
        }

        public static Arrangement Busfabriken(this City city)
        {
            var arrangement = new Arrangement().Busfabriken(city);
            city.Arrangements = new List<Arrangement> { arrangement };
            return arrangement;
        }

        public static Arrangement Laserdome(this City city)
        {
            var arrangement = new Arrangement().Laserdome(city);
            city.Arrangements = new List<Arrangement> { arrangement };
            return arrangement;
        }
    }
}

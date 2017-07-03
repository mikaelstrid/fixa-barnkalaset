using System;
using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class CityListView : ViewBase
    {
        public static Guid ListViewId = Guid.Parse("7DD8538F-3BBE-4A8C-8E65-D6ECA114938F");
        
        public List<City> Cities { get; set; } = new List<City>();

        public CityListView(Guid id, List<City> cities)
        {
            Id = id;
            Cities = cities;
        }

        public class City
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Slug { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public int ArrangementsCount { get; set; }

            public City(Guid id, string name, string slug, double latitude, double longitude)
            {
                Id = id;
                Name = name;
                Slug = slug;
                Latitude = latitude;
                Longitude = longitude;
            }
        }
    }
}
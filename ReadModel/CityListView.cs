using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class CityListView : ViewBase
    {
        public List<City> Cities { get; set; } = new List<City>();

        public class City
        {
            public string Name { get; set; }
            public string Slug { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class CityListView : ViewBase
    {
        public static Guid ListViewId = Guid.Parse("7DD8538F-3BBE-4A8C-8E65-D6ECA114938F");

        public List<City> Cities { get; set; } = new List<City>();

        public class City
        {
            public string Name { get; set; }
            public string Slug { get; set; }
        }
    }
}
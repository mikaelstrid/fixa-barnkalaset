using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Core
{
    public class City
    {
        public City() { }

        public City(string name, string slug, double latitude, double longitude)
        {
            Name = name;
            Slug = slug;
            Latitude = latitude;
            Longitude = longitude;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public virtual List<Arrangement> Arrangements { get; set; }
    }
}

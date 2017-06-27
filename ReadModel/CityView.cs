using System;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class CityView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
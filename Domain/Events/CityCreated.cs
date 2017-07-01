using System;

namespace Pixel.FixaBarnkalaset.Domain.Events
{
    public class CityCreated : EventBase
    {
        public string Name { get; }
        public string Slug { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public CityCreated(Guid id, string name, string slug, double latitude, double longitude)
        {
            Id = id;
            Name = name;
            Slug = slug;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
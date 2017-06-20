using System;

namespace Pixel.FixaBarnkalaset.Core.Events
{
    public class CityCreated : IEvent
    {
        public readonly Guid Id;
        public readonly string Name;
        public readonly string Slug;
        public readonly double Latitude;
        public readonly double Longitude;

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
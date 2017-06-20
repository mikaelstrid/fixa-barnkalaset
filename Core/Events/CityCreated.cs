using System;

namespace Pixel.FixaBarnkalaset.Core.Events
{
    public class CityCreated : IEvent
    {
        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public Guid Id { get; }
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
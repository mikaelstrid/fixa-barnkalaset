using System;

namespace Pixel.FixaBarnkalaset.Domain.Commands
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ChangeCityPosition : CommandBase
    {
        public Guid CityId { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public ChangeCityPosition(Guid cityId, double latitude, double longitude)
        {
            CityId = cityId;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
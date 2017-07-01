using System;

namespace Pixel.FixaBarnkalaset.Domain.Events
{
    public class CityPositionChanged : EventBase
    {
        public double NewLatitude { get; }
        public double NewLongitude { get; }
        public double OldLatitude { get; }
        public double OldLongitude { get; }

        public CityPositionChanged(Guid id, double newLatitude, double newLongitude, double oldLatitude, double oldLongitude)
        {
            Id = id;
            NewLatitude = newLatitude;
            NewLongitude = newLongitude;
            OldLatitude = oldLatitude;
            OldLongitude = oldLongitude;
        }
    }
}

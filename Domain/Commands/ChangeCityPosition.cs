namespace Pixel.FixaBarnkalaset.Domain.Commands
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ChangeCityPosition : CommandBase
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public ChangeCityPosition(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
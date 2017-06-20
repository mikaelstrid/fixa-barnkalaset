namespace Pixel.FixaBarnkalaset.Core.Commands
{
    public class CreateCity
    {
        public string Name { get; }
        public string Slug { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public CreateCity(string name, string slug, double latitude, double longitude)
        {
            Name = name;
            Slug = slug;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
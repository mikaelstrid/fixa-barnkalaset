namespace Pixel.FixaBarnkalaset.Core.Models
{
    public class Arrangement : Entity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string Pitch { get; set; }
        public string Description { get; set; }
        public string BookingConditions { get; set; }
        public string PriceInformation { get; set; }

        public string GooglePlacesId { get; set; }
        public string CoverImage { get; set; }
        public string CoverImageAttributions { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string PostalCity { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
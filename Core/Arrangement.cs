namespace Pixel.Kidsparties.Core
{
    public class Arrangement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Pitch { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string PostalCity { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string CategoryCity { get; set; }
    }
}
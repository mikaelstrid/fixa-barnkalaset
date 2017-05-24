using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Core
{
    public class Arrangement
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        public string Pitch { get; set; }
        public string Description { get; set; }
        public string GooglePlacesId { get; set; }
        public string CoverImage { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string PostalCity { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public string CitySlug { get; set; }
        public City City { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Pixel.Kidsparties.Core
{
    public class Arrangement
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Pitch { get; set; }

        public string Description { get; set; }

        public string CoverImage { get; set; }

        public string StreetAddress { get; set; }

        public string PostalCode { get; set; }

        public string PostalCity { get; set; }

        public string Country { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public string CategoryCity { get; set; }

        [Required]
        public string CategoryCounty { get; set; }
    }
}
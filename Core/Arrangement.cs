using System.ComponentModel.DataAnnotations;

namespace Pixel.Kidsparties.Core
{
    public class Arrangement
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Display(Name = "Pitch")]
        public string Pitch { get; set; }

        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Display(Name = "Omslagsbild")]
        public string CoverImage { get; set; }

        [Display(Name = "Gatuadress")]
        public string StreetAddress { get; set; }

        [Display(Name = "Postnummer")]
        public string PostalCode { get; set; }

        [Display(Name = "Postort")]
        public string PostalCity { get; set; }

        [Display(Name = "Land")]
        public string Country { get; set; }

        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-postadress")]
        public string EmailAddress { get; set; }

        [Display(Name = "Hemsida")]
        public string Website { get; set; }

        [Required]
        [Display(Name = "Latitud")]
        public double Latitude { get; set; }

        [Required]
        [Display(Name = "Longitud")]
        public double Longitude { get; set; }

        [Required]
        [Display(Name = "Stad")]
        public string CategoryCity { get; set; }
    }
}
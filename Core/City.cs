using System.ComponentModel.DataAnnotations;

namespace Pixel.Kidsparties.Core
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Required]
        [Display(Name = "Latitud")]
        public decimal Latitude { get; set; }

        [Required]
        [Display(Name = "Longitud")]
        public decimal Longitude { get; set; }
    }
}

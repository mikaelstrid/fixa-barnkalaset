using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels
{
    public class CreateOrEditCityViewModel
    {
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

using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels
{
    public class CreateOrEditCityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[abcdefghijklmnopqrstuvwxyz0123456789-]*$")]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Required]
        [Range(-90.0, 90.0)]
        [Display(Name = "Latitud")]
        public double Latitude { get; set; }

        [Required]
        [Range(-180.0, 180.0)]
        [Display(Name = "Longitud")]
        public double Longitude { get; set; }
    }
}

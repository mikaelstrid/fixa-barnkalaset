using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels
{
    public class CreateOrEditCityViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [RegularExpression("^[abcdefghijklmnopqrstuvwxyz0123456789-]*$")]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [Range(-90.0, 90.0, ErrorMessage = "Värdet ska vara mellan -90,0 och 90,0")]
        [Display(Name = "Latitud")]
        public double Latitude { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [Range(-180.0, 180.0, ErrorMessage = "Värdet ska vara mellan -180,0 och 180,0")]
        [Display(Name = "Longitud")]
        public double Longitude { get; set; }
    }
}

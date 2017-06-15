using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels
{
    public class IndexCityViewModel
    {
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Display(Name = "Latitud")]
        [DisplayFormat(DataFormatString = "{0:N6}")]
        public decimal Latitude { get; set; }

        [Display(Name = "Longitud")]
        [DisplayFormat(DataFormatString = "{0:N6}")]
        public decimal Longitude { get; set; }

        [Display(Name = "Arrangemang")]
        public int ArrangementsCount { get; set; }
    }
}

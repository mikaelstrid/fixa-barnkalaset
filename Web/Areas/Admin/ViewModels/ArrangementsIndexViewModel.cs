using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels
{
    public class ArrangementsIndexViewModel
    {
        [Display(Name = "Arrangemang")]
        public IEnumerable<ArrangementViewModel> Arrangements { get; set; }

        public class ArrangementViewModel
        {
            public int Id { get; set; }

            [Display(Name = "Namn")]
            public string Name { get; set; }

            [Display(Name = "Slug")]
            public string Slug { get; set; }

            [Display(Name = "Stad")]
            public string CityName { get; set; }

            [Display(Name = "Stadsslug")]
            public string CitySlug { get; set; }

            [Display(Name = "Pitch")]
            public string Pitch { get; set; }
        }
    }
}
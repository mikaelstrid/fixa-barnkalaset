using System.Collections.Generic;

namespace Pixel.Kidsparties.Web.Models
{
    public class ArrangementIndexViewModel
    {
        public string CityName { get; set; }
        public string CitySlug { get; set; }
        public IEnumerable<ArrangementViewModel> Arrangements { get; set; }

        public class ArrangementViewModel
        {
            public string Name { get; set; }
            public string Pitch { get; set; }
            public string Slug { get; set; }
            public string CoverImage { get; set; }
        }
    }
}
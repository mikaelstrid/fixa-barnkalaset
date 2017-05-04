using System.Collections.Generic;

namespace Pixel.Kidsparties.Web.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<CityViewModel> Cities { get; set; }

        public class CityViewModel
        {
            public string Name { get; set; }
            public string Slug { get; set; }
        }
    }
}
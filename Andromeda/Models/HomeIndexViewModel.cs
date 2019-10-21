using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Core;

namespace Andromeda.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<CityViewModel> Cities { get; set; }

        public class CityViewModel
        {
            public string Name { get; set; }
            public string Slug { get; set; }

            public static CityViewModel MapFromBusinessModel(City businessModel)
            {
                return new CityViewModel
                {
                    Name = businessModel.Name,
                    Slug = businessModel.Slug
                };
            }
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pixel.Kidsparties.Core
{
    public class City
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public List<Arrangement> Arrangements { get; set; }
    }
}

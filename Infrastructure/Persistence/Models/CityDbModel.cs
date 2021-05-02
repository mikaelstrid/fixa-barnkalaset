using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Models
{
    public class CityDbModel
    {
        public int Id { get; set; }
        public bool IsRemoved { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual List<ArrangementDbModel> Arrangements { get; set; }
    }
}

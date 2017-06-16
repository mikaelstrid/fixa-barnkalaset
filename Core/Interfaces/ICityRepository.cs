using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface ICityRepository
    {
        IEnumerable<City> GetAll();
        City GetBySlug(string citySlug);
        void AddOrUpdate(City model);
    }
}

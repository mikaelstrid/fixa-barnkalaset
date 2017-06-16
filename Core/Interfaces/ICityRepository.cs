using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface ICityRepository
    {
        IEnumerable<City> GetAll();
        City GetBySlug(string slug);
        void AddOrUpdate(City model);
        void Remove(string slug);
    }
}

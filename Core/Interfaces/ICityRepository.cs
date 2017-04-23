using System.Collections.Generic;

namespace Pixel.Kidsparties.Core.Interfaces
{
    public interface ICityRepository
    {
        IEnumerable<City> GetAll();
        City GetBySlug(string citySlug);
    }
}

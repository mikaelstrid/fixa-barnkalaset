using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAll();
        Task<City> GetBySlug(string slug);
        Task AddOrUpdate(City model);
        Task Remove(string slug);
    }
}

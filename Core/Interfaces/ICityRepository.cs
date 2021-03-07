using System.Collections.Generic;
using System.Threading.Tasks;
using Pixel.FixaBarnkalaset.Core.Models;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAll();
        Task<City> GetById(int id);
        Task<City> GetBySlug(string slug);
        Task AddOrUpdate(City model);
        Task Remove(int id);
    }
}

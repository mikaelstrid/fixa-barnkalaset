using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface ICityRepository : IRepository<City, int>
    {
        Task<City> GetBySlug(string slug);
    }
}

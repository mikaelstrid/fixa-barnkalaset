using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAll();
        //Task<City> GetById(int id);
        //Task<City> GetBySlug(string slug);
        //Task AddOrUpdate(City model);
        //Task Remove(int id);
    }
}

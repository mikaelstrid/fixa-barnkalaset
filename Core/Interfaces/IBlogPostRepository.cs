using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAll();
        Task<BlogPost> GetById(int id);
        Task<BlogPost> GetBySlug(string slug);
        Task AddOrUpdate(BlogPost model);
        Task Remove(int id);
    }
}

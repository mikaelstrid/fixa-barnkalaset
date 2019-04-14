using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IBlogPostRepository : IRepository<BlogPost, int>
    {
        Task<BlogPost> GetBySlug(string slug);
    }
}

using System.Threading.Tasks;
using Pixel.FixaBarnkalaset.Core.Interfaces;

namespace Pixel.FixaBarnkalaset.Web.Utilities
{
    public interface ISitemapGenerator
    {
        Task<string> GetAsString(ICityRepository cityRepository, IBlogPostRepository blogPostRepository);
    }
}
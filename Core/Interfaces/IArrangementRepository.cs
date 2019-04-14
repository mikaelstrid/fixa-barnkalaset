using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IArrangementRepository : IRepository<Arrangement, int>
    {
        IEnumerable<Arrangement> GetByCitySlug(string citySlug);
        Task<Arrangement> GetBySlug(string citySlug, string arrangementSlug);
    }
}

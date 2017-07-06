using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IArrangementRepository
    {
        Task<IEnumerable<Arrangement>> GetAll();
        IEnumerable<Arrangement> GetByCitySlug(string citySlug);
        Task<Arrangement> GetBySlug(string citySlug, string arrangementSlug);
        Task<Arrangement> GetById(int id);
        Task AddOrUpdate(Arrangement arrangement);
        Task Remove(int id);
    }
}

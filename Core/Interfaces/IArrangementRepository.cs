using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IArrangementRepository
    {
        Task<IEnumerable<Arrangement>> GetAll();
        IEnumerable<Arrangement> GetByCitySlug(string citySlug);
        Arrangement GetBySlug(string citySlug, string arrangementSlug);
        Arrangement GetById(int id);
        void AddOrUpdate(Arrangement arrangement);
        void Remove(int id);
    }
}

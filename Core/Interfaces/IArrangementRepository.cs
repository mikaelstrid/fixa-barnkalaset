using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IArrangementRepository
    {
        IEnumerable<Arrangement> GetAll();
        IEnumerable<Arrangement> GetByCitySlug(string citySlug);
        Arrangement GetBySlug(string citySlug, string arrangementSlug);
        Arrangement GetById(int id);
        void AddOrUpdate(Arrangement arrangement);
        void Remove(int id);
    }
}

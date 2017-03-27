using System.Collections.Generic;

namespace Pixel.Kidsparties.Core.Interfaces
{
    public interface IArrangementRepository
    {
        IEnumerable<Arrangement> GetAll();
        Arrangement GetById(int id);
        void AddOrUpdate(Arrangement arrangement);
        void Remove(int id);
    }
}

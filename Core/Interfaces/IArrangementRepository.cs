using System.Collections.Generic;

namespace Pixel.Kidsparties.Core.Interfaces
{
    public interface IArrangementRepository
    {
        IEnumerable<Arrangement> GetAll();
        IEnumerable<Arrangement> GetByCity(string city);
        Arrangement GetBySlug(string citySlug, string arrangementSlug);
        Arrangement GetById(int id);
        void AddOrUpdate(Arrangement arrangement);
        void Remove(int id);
        string GetCityName(string citySlug);
    }
}

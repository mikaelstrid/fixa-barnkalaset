using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IPartyRepository
    {
        Task<IEnumerable<Party>> GetAll();
        Task<Party> GetById(string id);
        Task AddOrUpdate(Party party);
        Task Remove(string id);
    }
}

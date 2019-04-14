using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IRepository<TModel, in TId> 
    {
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> GetById(TId id);
        Task AddOrUpdate(TModel model);
        Task Remove(TId id);
    }
}
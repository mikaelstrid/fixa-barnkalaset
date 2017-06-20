using System;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IRepository
    {
        Task<T> GetById<T>(Guid id) where T : class, IAggregate;
        Task Save(IAggregate aggregate);
    }
}
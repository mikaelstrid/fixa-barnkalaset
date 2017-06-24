using System;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IAggregateRepository
    {
        Task<T> GetById<T>(Guid id) where T : class, IAggregate;
        Task Save(IAggregate aggregate);
    }
}
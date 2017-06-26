using System;
using System.Threading.Tasks;
using Pixel.FixaBarnkalaset.Domain.Model;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IAggregateRepository
    {
        Task<T> GetById<T>(Guid id) where T : class, IAggregate;
        Task Save(IAggregate aggregate);
    }
}
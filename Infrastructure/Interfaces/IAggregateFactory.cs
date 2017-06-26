using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Domain.Events;

namespace Pixel.FixaBarnkalaset.Infrastructure.Interfaces
{
    public interface IAggregateFactory
    {
        T Create<T>(IEnumerable<IEvent> events);
    }
}
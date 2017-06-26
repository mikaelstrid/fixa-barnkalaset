using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Domain.Events;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IAggregateFactory
    {
        T Create<T>(IEnumerable<IEvent> events);
    }
}
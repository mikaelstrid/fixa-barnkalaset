using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Core.Events;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IAggregateFactory
    {
        T Create<T>(IEnumerable<IEvent> events);
    }
}
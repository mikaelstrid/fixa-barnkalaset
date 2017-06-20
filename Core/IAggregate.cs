using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Core.Events;

namespace Pixel.FixaBarnkalaset.Core
{
    public interface IAggregate
    {
        IEnumerable<IEvent> GetUncommittedEvents();
        int Version { get; }
        Guid Id { get; }
        void ClearUncommittedEvents();
    }
}
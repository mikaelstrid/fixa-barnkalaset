using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Domain.Events;

namespace Pixel.FixaBarnkalaset.Domain.Model
{
    public interface IAggregate
    {
        IEnumerable<IEvent> GetUncommittedEvents();
        int Version { get; }
        Guid Id { get; }
        void ClearUncommittedEvents();
    }
}
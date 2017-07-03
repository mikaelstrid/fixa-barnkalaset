using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Domain.Events;

namespace Pixel.FixaBarnkalaset.Domain.Model
{
    public interface IAggregate
    {
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}
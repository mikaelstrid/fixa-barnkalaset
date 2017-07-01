using System;

namespace Pixel.FixaBarnkalaset.Domain.Events
{
    public abstract class EventBase : IEvent
    {
        public Guid Id { get; protected set; }
    }
}
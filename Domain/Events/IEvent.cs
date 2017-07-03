using System;

namespace Pixel.FixaBarnkalaset.Domain.Events
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}
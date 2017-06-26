using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AggregateFactory : IAggregateFactory
    {
        public T Create<T>(IEnumerable<IEvent> events)
        {
            return (T)Activator.CreateInstance(typeof(T), events);
        }
    }
}
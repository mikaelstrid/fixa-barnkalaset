using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Domain.Events;

namespace Pixel.FixaBarnkalaset.Core
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
using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Core.Events;
using Pixel.FixaBarnkalaset.Core.Interfaces;

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
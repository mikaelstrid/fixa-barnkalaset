using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Core.Events;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public interface IAggregateFactory
    {
        T Create<T>(IEnumerable<IEvent> events);
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class AggregateFactory : IAggregateFactory
    {
        public T Create<T>(IEnumerable<IEvent> events)
        {
            return (T)Activator.CreateInstance(typeof(T), events);
        }
    }
}
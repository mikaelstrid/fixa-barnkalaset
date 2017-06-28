using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IEnumerable<IProjection> _observers;

        public EventPublisher(IProjectionRegistry projectionRegistry)
        {
            _observers = projectionRegistry.GetObservers() ?? new List<IProjection>();
        }

        public void Publish(IEvent @event)
        {
            foreach (var observer in _observers) 
                observer.Handle(@event);
        }
    }
}
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IEnumerable<IObserver> _observers;

        public EventPublisher(IObserverRegistry observerRegistry)
        {
            _observers = observerRegistry.GetObservers() ?? new List<IObserver>();
        }

        public void Publish(IEvent @event)
        {
            foreach (var observer in _observers) 
                observer.Handle(@event);
        }
    }
}
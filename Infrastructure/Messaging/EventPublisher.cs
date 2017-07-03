using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly MyEventSourcingDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IEnumerable<IProjection> _projections;

        public EventPublisher(MyEventSourcingDbContext dbContext, IProjectionRegistry projectionRegistry, ILogger<EventPublisher> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _projections = projectionRegistry.GetObservers() ?? new List<IProjection>();
        }

        public void Publish(IEvent @event)
        {
            _logger.LogDebug("Publish: Publishing event with id {EventId} and type {EventType}...", @event.Id, @event.GetType().Name);
            var sw = Stopwatch.StartNew();
            var count = PublishInternal(@event);
            _logger.LogDebug("Publish: Publish of event with id {EventId} and type {EventType} to {NumberOfProjections} took {ElapsedTime}", @event.Id, @event.GetType().Name, count, sw.ElapsedMilliseconds);
        }

        public void CatchUp(int lastPosition)
        {
            _logger.LogInformation("CatchUp: Starting catchup...");
            var count = 0;
            var sw = Stopwatch.StartNew();
            foreach (var e in _dbContext.Events.Where(e => e.Id > lastPosition))
            {
                PublishInternal(e.ToEvent());
                count++;
            }
            _logger.LogInformation("CatchUp: Catchup published {NumberOfEvents} events which took {ElapsedTime} ms ({AverageTime} e/s)...", count, sw.ElapsedMilliseconds, count/sw.Elapsed.TotalSeconds);
        }


        private int PublishInternal(IEvent @event)
        {
            var count = 0;
            foreach (var observer in _projections)
            {
                observer.Handle(@event);
                count++;
            }
            return count;
        }
    }
}
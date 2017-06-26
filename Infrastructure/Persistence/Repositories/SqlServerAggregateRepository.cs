using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Domain.Model;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SqlServerAggregateRepository : IAggregateRepository
    {
        private readonly MyEventSourcingDbContext _dbContext;
        private readonly IAggregateFactory _factory;
        private readonly ISettings _settings;

        public SqlServerAggregateRepository(MyEventSourcingDbContext dbContext, IAggregateFactory factory, ISettings settings)
        {
            _dbContext = dbContext;
            _factory = factory;
            _settings = settings;
        }

        public async Task<T> GetById<T>(Guid id) where T : class, IAggregate
        {
            var events = await _dbContext.Events
                .Where(e => e.AggregateId == id)
                .Select(e => e.ToEvent())
                .ToListAsync();

            var aggregate = _factory.Create<T>(events);

            return aggregate;
        }

        public async Task Save(IAggregate aggregate)
        {
            var events = aggregate.GetUncommittedEvents().ToArray();
            if (!events.Any())
                return;

            var aggregateType = aggregate.GetType().Name;
            var originalVersion = aggregate.Version - events.Length + 1;

            var eventsToSave = events
                .Select(e => e.ToEventData(aggregateType, aggregate.Id, originalVersion++))
                .ToArray();
            
            if (await _dbContext.Events.AnyAsync() && await _dbContext.Events.MaxAsync(e => e.Version) >= originalVersion)
                throw new Exception("Concurrency exception");

            await _dbContext.Events.AddRangeAsync(eventsToSave);

            await _dbContext.SaveChangesAsync();

            aggregate.ClearUncommittedEvents();
        }
    }
}

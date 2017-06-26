using System;
using System.Collections.Generic;
using System.Linq;
using Pixel.FixaBarnkalaset.Domain.Events;

namespace Pixel.FixaBarnkalaset.Domain.Model
{
    public abstract class AggregateBase : IAggregate
    {
        protected Guid _id;
        protected int _version;
        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

        public Guid Id => _id;
        public int Version => _version;

        protected abstract void Apply(IEvent e);

        protected void Publish(IEvent e)
        {
            _uncommittedEvents.Add(e);
            Apply(e);
        }

        public IEnumerable<IEvent> GetUncommittedEvents() => _uncommittedEvents.AsEnumerable();

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }
    }
}
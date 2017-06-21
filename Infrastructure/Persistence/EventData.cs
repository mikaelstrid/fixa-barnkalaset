using System;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence
{
    public class EventData
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string AggregateType { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public string Event { get; set; }
        public string Metadata { get; set; }
    }
}

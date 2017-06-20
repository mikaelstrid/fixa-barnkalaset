﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core.Events;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence
{
    public static class EventDataExtensions
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };

        public static EventData ToEventData(this IEvent @event, string aggregateType, Guid aggregateId, int version)
        {
            var data = JsonConvert.SerializeObject(@event, SerializerSettings);
            var eventHeaders = new Dictionary<string, object>
            {
                {
                    "EventClrType", @event.GetType().AssemblyQualifiedName
                }
            };

            var metadata = JsonConvert.SerializeObject(eventHeaders, SerializerSettings);
            var eventId = Guid.NewGuid();

            return new EventData
            {
                EventId = eventId,
                Created = DateTime.UtcNow,
                AggregateType = aggregateType,
                AggregateId = aggregateId,
                Version = version,
                Event = data,
                Metadata = metadata,
            };
        }

        public static IEvent ToEvent(this EventData eventData)
        {
            var type = Type.GetType(eventData.AggregateType);
            var @event = JsonConvert.DeserializeObject(eventData.Event, type) as IEvent;
            return @event;
        }
    }
}
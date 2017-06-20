﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Pixel.FixaBarnkalaset.Core.Events;
using Pixel.FixaBarnkalaset.Core.Utilities;

namespace Pixel.FixaBarnkalaset.Core
{
    public class CityAggregate : AggregateBase
    {
        public CityAggregate() { }

        public CityAggregate(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
                Apply(e);
        }
        
        protected sealed override void Apply(IEvent e)
        {
            _version++;
            RedirectToWhen.InvokeEventOptional(this, e);
        }
        
        public void Create(Guid id, string name, string slug, double latitude, double longitude)
        {
            if (_version > 0)
                throw new InvalidOperationException("Cannot create a city more than once");

            if (id == Guid.Empty)
                throw new ArgumentException("A city must have a non-empty id", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("A city must have a valid name", nameof(name));

            if (string.IsNullOrWhiteSpace(slug) || !Regex.IsMatch(slug, "^[abcdefghijklmnopqrstuvwxyz0123456789-]*$"))
                throw new ArgumentException("A city must have a valid slug", nameof(slug));

            if (!(-90.0 <= latitude && latitude <= 90.0))
                throw new ArgumentException("A city must have a valid latitude", nameof(latitude));

            if (!(-180.0 <= longitude && longitude <= 180.0))
                throw new ArgumentException("A city must have a valid latitude", nameof(latitude));

            Publish(new CityCreated(id, name, slug, latitude, longitude));
        }
        
        private void When(CityCreated e)
        {
            _id = e.Id;
        }
    }
}
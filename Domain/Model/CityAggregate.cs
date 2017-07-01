using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Domain.Utilities;

namespace Pixel.FixaBarnkalaset.Domain.Model
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class CityAggregate : AggregateBase
    {
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
        
        public virtual void Create(Guid id, string name, string slug, double latitude, double longitude)
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


        public virtual void ChangeName(string newName)
        {
            throw new NotImplementedException();
        }

        public virtual void ChangeSlug(string newSlug)
        {
            throw new NotImplementedException();
        }

        public virtual void ChangePosition(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }
    }
}
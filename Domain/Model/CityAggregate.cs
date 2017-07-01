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
        private string _name;
        private string _slug;
        private double _latitude;
        private double _longitude;

        public CityAggregate(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
                Apply(e);
        }

        public virtual void Create(Guid id, string name, string slug, double latitude, double longitude)
        {
            if (_version > 0)
            {
                throw new InvalidOperationException("Cannot create a city more than once");
            }

            ValidateId(id);
            ValidateName(name);
            ValidateSlug(slug);
            ValidateLatitude(latitude);
            ValidateLongitude(longitude);

            Publish(new CityCreated(id, name, slug, latitude, longitude));
        }

        private void When(CityCreated e)
        {
            _id = e.Id;
            _name = e.Name;
            _slug = e.Slug;
            _latitude = e.Latitude;
            _longitude = e.Longitude;
        }


        public virtual void ChangeName(string newName)
        {
            ValidateName(newName);
            Publish(new CityNameChanged(_id, newName, _name));
        }

        private void When(CityNameChanged e)
        {
            _name = e.NewName;
        }


        public virtual void ChangeSlug(string newSlug)
        {
            ValidateSlug(newSlug);
            Publish(new CitySlugChanged(_id, newSlug, _slug));
        }

        private void When(CitySlugChanged e)
        {
            _slug = e.NewSlug;
        }


        public virtual void ChangePosition(double latitude, double longitude)
        {
            ValidateLatitude(latitude);
            ValidateLongitude(longitude);
            Publish(new CityPositionChanged(_id, latitude, longitude, _latitude, _longitude));
        }



        protected sealed override void Apply(IEvent e)
        {
            _version++;
            RedirectToWhen.InvokeEventOptional(this, e);
        }


        private static void ValidateId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("A city must have a non-empty id", nameof(id));
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("A city must have a valid name", nameof(name));
        }

        private static void ValidateSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug) || !Regex.IsMatch(slug, "^[abcdefghijklmnopqrstuvwxyz0123456789-]*$"))
                throw new ArgumentException("A city must have a valid slug", nameof(slug));
        }

        private static void ValidateLatitude(double latitude)
        {
            if (!(-90.0 <= latitude && latitude <= 90.0))
                throw new ArgumentException("A city must have a valid latitude", nameof(latitude));
        }

        private static void ValidateLongitude(double longitude)
        {
            if (!(-180.0 <= longitude && longitude <= 180.0))
                throw new ArgumentException("A city must have a valid latitude", nameof(longitude));
        }
    }
}
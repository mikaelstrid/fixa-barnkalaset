using System;

namespace Pixel.FixaBarnkalaset.Domain.Events
{
    public class CitySlugChanged : EventBase
    {
        public string NewSlug { get; }
        public string OldSlug { get; }

        public CitySlugChanged(Guid id, string newSlug, string oldSlug)
        {
            Id = id;
            NewSlug = newSlug;
            OldSlug = oldSlug;
        }
    }
}

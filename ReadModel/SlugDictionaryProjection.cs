using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class SlugLookupProjection : IProjection
    {
        private readonly ISlugLookup _slugLookup;

        public SlugLookupProjection(ISlugLookup slugLookup)
        {
            _slugLookup = slugLookup;
        }

        public void When(CityCreated e)
        {
            _slugLookup.AddSlug(e.Slug, e.Id);
        }

        public void When(CitySlugChanged e)
        {
            _slugLookup.RemoveSlug(e.OldSlug);
            _slugLookup.AddSlug(e.NewSlug, e.Id);
        }

        public void Handle(IEvent e)
        {
            switch (e)
            {
                case CityCreated c:
                {
                    When(c); break;
                }
                case CitySlugChanged c:
                {
                    When(c); break;
                }
            }
        }
    }
}

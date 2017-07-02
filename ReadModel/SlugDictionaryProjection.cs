using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class SlugDictionaryProjection : IProjection
    {
        private readonly ISlugDictionary _slugDictionary;

        public SlugDictionaryProjection(ISlugDictionary slugDictionary)
        {
            _slugDictionary = slugDictionary;
        }

        public void When(CityCreated e)
        {
            _slugDictionary.AddSlug(e.Slug, e.Id);
        }

        public void When(CitySlugChanged e)
        {
            _slugDictionary.RemoveSlug(e.OldSlug);
            _slugDictionary.AddSlug(e.NewSlug, e.Id);
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

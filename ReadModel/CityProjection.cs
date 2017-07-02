using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class CityProjection : IProjection
    {
        private readonly IViewRepository _viewRepository;

        public CityProjection(IViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }

        public void When(CityCreated e)
        {
            var view = new CityView
            {
                Id = e.Id,
                Name = e.Name,
                Slug = e.Slug,
                Latitude = e.Latitude,
                Longitude = e.Longitude
            };
            _viewRepository.Add(view);
        }


        public void Handle(IEvent e)
        {
            switch (e)
            {
                case CityCreated c:
                {
                    When(c); break;
                }
                case CityNameChanged c:
                {
                    When(c); break;
                }
                case CitySlugChanged c:
                {
                    When(c); break;
                }
                case CityPositionChanged c:
                {
                    When(c); break;
                }
            }
        }

        public void When(CityNameChanged e)
        {
            _viewRepository.Update<CityView>(e.Id, c => c.Name = e.NewName);
        }

        public void When(CitySlugChanged e)
        {
            _viewRepository.Update<CityView>(e.Id, c => c.Slug = e.NewSlug);
        }

        public void When(CityPositionChanged e)
        {
            _viewRepository.Update<CityView>(e.Id, c =>
            {
                c.Latitude = e.NewLatitude;
                c.Longitude = e.NewLongitude;
            });
        }
    }
}

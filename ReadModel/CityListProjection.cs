using System;
using System.Collections.Generic;
using System.Linq;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class CityListProjection : IProjection
    {
        private readonly IViewRepository _viewRepository;

        public CityListProjection(IViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }

        public void When(CityCreated e)
        {
            var city = new CityListView.City(e.Id, e.Name, e.Slug, e.Latitude, e.Longitude);

            if (!_viewRepository.Contains<CityListView>(CityListView.ListViewId))
            {
                _viewRepository.Add(new CityListView(CityListView.ListViewId, new List<CityListView.City> {city}));
            }
            else
            {
                _viewRepository.Update<CityListView>(CityListView.ListViewId, v => v.Cities.Add(city));
            }
        }

        public void When(CityNameChanged e)
        {
            _viewRepository.Update<CityListView>(CityListView.ListViewId, v => v.Cities.Single(c => c.Id == e.Id).Name = e.NewName);
        }

        public void When(CitySlugChanged e)
        {
            _viewRepository.Update<CityListView>(CityListView.ListViewId, v => v.Cities.Single(c => c.Id == e.Id).Slug = e.NewSlug);
        }

        public void When(CityPositionChanged e)
        {
            _viewRepository.Update<CityListView>(CityListView.ListViewId, v =>
            {
                var city = v.Cities.Single(c => c.Id == e.Id);
                city.Latitude = e.NewLatitude;
                city.Longitude = e.NewLongitude;
            });
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
    }
}

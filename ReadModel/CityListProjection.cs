using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class CityListProjection : IProjection
    {
        private readonly IViewRepository _writer;

        public CityListProjection(IViewRepository writer)
        {
            _writer = writer;
        }

        public void When(CityCreated e)
        {
            var city = new CityListView.City
            {
                Name = e.Name,
                Slug = e.Slug
            };

            if (!_writer.Contains<CityListView>(CityListView.ListViewId))
            {
                _writer.Add(new CityListView {Cities = new List<CityListView.City> {city}});
            }
            else
            {
                _writer.Update<CityListView>(CityListView.ListViewId, v => v.Cities.Add(city));
            }
        }
        
        public void Handle(IEvent e)
        {
            switch (e)
            {
                case CityCreated c:
                {
                    When(c);
                    break;
                }
            }
        }
    }
}

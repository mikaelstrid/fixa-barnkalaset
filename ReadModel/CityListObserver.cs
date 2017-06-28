using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class CityListObserver : IObserver
    {
        private readonly Guid _id = Guid.Parse("7DD8538F-3BBE-4A8C-8E65-D6ECA114938F");

        private readonly IProjectionWriter _writer;

        public CityListObserver(IProjectionWriter writer)
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

            if (!_writer.Contains<CityListView>(_id))
            {
                var view = new CityListView {Cities = new List<CityListView.City> {city}};
                _writer.Add(view);
            }
            else
            {
                _writer.Update<CityListView>(_id, v => v.Cities.Add(city));
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

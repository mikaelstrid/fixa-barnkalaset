using System;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class CityObserver
    {
        private readonly IProjectionWriter<CityView> _writer;

        public CityObserver(IProjectionWriter<CityView> writer)
        {
            _writer = writer;
        }

        public void When(CityCreated e)
        {
            var view = new CityView
            {
                Id = e.Id,
                Name = e.Name,
                Latitude = e.Latitude,
                Longitude = e.Longitude
            };
            _writer.Add(view);
        }

        //public void When(CityNameChanged e)
        //{
        //    _writer.Update(e.Id, v => v.Name = e.Name);
        //}
    }

    public class CityView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

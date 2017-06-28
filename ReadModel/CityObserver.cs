using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.ReadModel
{
    public class CityObserver : IObserver
    {
        private readonly IProjectionWriter _writer;

        public CityObserver(IProjectionWriter writer)
        {
            _writer = writer;
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
            _writer.Add(view);
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

        //public void When(CityNameChanged e)
        //{
        //    _writer.Update(e.Id, v => v.Name = e.Name);
        //}
    }
}

using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.Messaging
{
    public class ObserverRegistry : IObserverRegistry
    {
        private readonly IProjectionWriter _projectionWriter;

        public ObserverRegistry(IProjectionWriter projectionWriter)
        {
            _projectionWriter = projectionWriter;
        }

        public IEnumerable<IObserver> GetObservers()
        {
            yield return new CityObserver(_projectionWriter);
        }
    }
}

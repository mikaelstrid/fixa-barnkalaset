using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.ReadModel
{
    public class ProjectionRegistry : IProjectionRegistry
    {
        private readonly IViewRepository _viewRepository;

        public ProjectionRegistry(IViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }

        public IEnumerable<IProjection> GetObservers()
        {
            yield return new CityProjection(_viewRepository);
        }
    }
}

using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.ReadModel
{
    public class ProjectionRegistry : IProjectionRegistry
    {
        private readonly IViewRepository _viewRepository;
        private readonly ISlugLookup _slugLookup;

        public ProjectionRegistry(IViewRepository viewRepository, ISlugLookup slugLookup)
        {
            _viewRepository = viewRepository;
            _slugLookup = slugLookup;
        }

        public IEnumerable<IProjection> GetObservers()
        {
            yield return new CityProjection(_viewRepository);
            yield return new CityListProjection(_viewRepository);
            yield return new SlugLookupProjection(_slugLookup);
        }
    }
}

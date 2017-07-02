using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.ReadModel
{
    public class ProjectionRegistry : IProjectionRegistry
    {
        private readonly IViewRepository _viewRepository;
        private readonly ISlugDictionary _slugDictionary;

        public ProjectionRegistry(IViewRepository viewRepository, ISlugDictionary slugDictionary)
        {
            _viewRepository = viewRepository;
            _slugDictionary = slugDictionary;
        }

        public IEnumerable<IProjection> GetObservers()
        {
            yield return new CityProjection(_viewRepository);
            yield return new CityListProjection(_viewRepository);
            yield return new SlugDictionaryProjection(_slugDictionary);
        }
    }
}

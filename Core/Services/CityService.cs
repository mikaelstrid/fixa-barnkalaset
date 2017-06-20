using System;
using System.Threading.Tasks;
using Pixel.FixaBarnkalaset.Core.Commands;
using Pixel.FixaBarnkalaset.Core.Interfaces;

namespace Pixel.FixaBarnkalaset.Core.Services
{
    public class CityService
    {
        private readonly IRepository _repository;

        public CityService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> When(CreateCity cmd)
        {
            var id = Guid.NewGuid();
            await Act<CityAggregate>(id, aggregate => aggregate.Create(id, cmd.Name, cmd.Slug, cmd.Latitude, cmd.Longitude));
            return id;
        }

        private async Task Act<T>(Guid id, Action<T> action) where T : class, IAggregate
        {
            var aggregate = await _repository.GetById<T>(id);
            action(aggregate);
            await _repository.Save(aggregate);
        }
    }
}

using System;
using System.Threading.Tasks;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Domain.Commands;
using Pixel.FixaBarnkalaset.Domain.Model;

namespace Pixel.FixaBarnkalaset.Core.Services
{
    public class CityService : ICityService
    {
        private readonly IAggregateRepository _aggregateRepository;

        public CityService(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public async Task<Guid> When(CreateCity cmd)
        {
            var id = Guid.NewGuid();
            await Act<CityAggregate>(id, aggregate => aggregate.Create(id, cmd.Name, cmd.Slug, cmd.Latitude, cmd.Longitude));
            return id;
        }

        public async Task When(ChangeCityName cmd)
        {
            await Act<CityAggregate>(cmd.CityId, aggregate => aggregate.ChangeName(cmd.NewName));
        }

        public async Task When(ChangeCitySlug cmd)
        {
            await Act<CityAggregate>(cmd.CityId, aggregate => aggregate.ChangeSlug(cmd.NewSlug));
        }

        public async Task When(ChangeCityPosition cmd)
        {
            await Act<CityAggregate>(cmd.CityId, aggregate => aggregate.ChangePosition(cmd.Latitude, cmd.Longitude));
        }

        private async Task Act<T>(Guid id, Action<T> action) where T : class, IAggregate
        {
            var aggregate = await _aggregateRepository.GetById<T>(id);
            action(aggregate);
            await _aggregateRepository.Save(aggregate);
        }
    }
}

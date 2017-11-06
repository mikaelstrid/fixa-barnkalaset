using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public class SqlPartyRepository : SqlIdGeneratorRepositoryBase<Party>, IPartyRepository
    {
        public SqlPartyRepository(MyDataDbContext dbContext, ILogger<SqlPartyRepository> logger, IPartyIdGenerator partyIdGenerator)
            : base(dbContext, logger, partyIdGenerator)
        {
        }

        public async Task AddOrUpdate(Party party)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(party, settings);
            
            Logger.LogDebug("AddOrUpdate: Adding or updating party with id {Id} with data {Entity}", party.Id, json);

            if (party.Id == null)
            {
                party.Id = await GetId();
                await DbSet.AddAsync(party);
                Logger.LogInformation("AddOrUpdate: Added party with id {Id} with data {Entity}", party.Id, json);
            }
            else
            {
                var existingEntity = await GetById(party.Id);
                existingEntity.NameOfBirthdayChild = party.NameOfBirthdayChild;
                DbContext.Update(existingEntity);
                Logger.LogInformation("AddOrUpdate: Updated party with id {Id} with data {Entity}", party.Id, json);
            }
            await DbContext.SaveChangesAsync();
        }
    }
}

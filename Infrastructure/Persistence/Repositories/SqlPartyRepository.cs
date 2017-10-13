using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public class SqlPartyRepository : IPartyRepository
    {
        private readonly MyDataDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IPartyIdGenerator _partyIdGenerator;

        public SqlPartyRepository(MyDataDbContext dbContext, ILogger<SqlPartyRepository> logger, IPartyIdGenerator partyIdGenerator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _partyIdGenerator = partyIdGenerator;
        }

        public async Task<IEnumerable<Party>> GetAll()
        {
            _logger.LogDebug("GetAll: Get all parties");
            return await _dbContext.Parties.ToListAsync();
        }

        public async Task<Party> GetById(string id)
        {
            _logger.LogDebug("GetById: Get party with id {Id}", id);
            return await _dbContext.Parties.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddOrUpdate(Party entity)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(entity, settings);
            
            _logger.LogDebug("AddOrUpdate: Adding or updating party with id {Id} with data {Entity}", entity.Id, json);

            if (entity.Id == null)
            {
                entity.Id = _partyIdGenerator.Next();
                await _dbContext.Parties.AddAsync(entity);
                _logger.LogInformation("AddOrUpdate: Added party with id {Id} with data {Entity}", entity.Id, json);
            }
            else
            {
                var existingEntity = await GetById(entity.Id);
                existingEntity.NameOfBirthdayChild = entity.NameOfBirthdayChild;
                _dbContext.Update(existingEntity);
                _logger.LogInformation("AddOrUpdate: Updated party with id {Id} with data {Entity}", entity.Id, json);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(string id)
        {
            _logger.LogDebug("Remove: Removing party with id {Id}", id);
            var invitationCard = await GetById(id);
            if (invitationCard == null)
            {
                _logger.LogInformation("Remove: party with id {Id} not found, continue", id);
                return;
            }
            _dbContext.Parties.Remove(invitationCard);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Remove: party with id {Id} removed", id);
        }
    }
}

using System;
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
    public class SqlGuestRepository : IGuestRepository
    {
        private readonly MyDataDbContext _dbContext;
        private readonly ILogger _logger;

        public SqlGuestRepository(MyDataDbContext dbContext, ILogger<SqlGuestRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Guest>> GetAll()
        {
            _logger.LogDebug("GetAll: Get all guests");
            return await _dbContext.Guests.ToListAsync();
        }

        public async Task<Guest> GetById(int id)
        {
            _logger.LogDebug("GetById: Get guest with id {Id}", id);
            return await _dbContext.Guests.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddOrUpdate(Guest model)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(model, settings);
            
            _logger.LogDebug("AddOrUpdate: Adding or updating guest with id {Id} with data {Entity}", model.Id, json);

            if (model.Id == default(int))
            {
                await _dbContext.Guests.AddAsync(model);
                _logger.LogInformation("AddOrUpdate: Added guest with id {Id} with data {Entity}", model.Id, json);
            }
            else
            {
                var existingGuest = await GetById(model.Id);
                existingGuest.FirstName = model.FirstName;
                existingGuest.LastName = model.LastName;
                existingGuest.StreetAddress = model.StreetAddress;
                existingGuest.PostalCode = model.PostalCode;
                existingGuest.PostalCity = model.PostalCity;
                _dbContext.Update(existingGuest);
                _logger.LogInformation("AddOrUpdate: Updated guest with id {Id} with data {Entity}", model.Id, json);
            }
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Remove(int id)
        {
            _logger.LogDebug("Remove: Removing guest with id {Id}", id);
            var existingGuest = await GetById(id);
            if (existingGuest == null)
            {
                _logger.LogInformation("Remove: guest with id {Id} not found, continue", id);
                return;
            }
            _dbContext.Guests.Remove(existingGuest);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Remove: Guest with id {Id} removed", id);
        }
    }
}

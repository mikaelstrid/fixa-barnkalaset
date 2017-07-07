using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public class SqlArrangementRepository : IArrangementRepository
    {
        private readonly MyDataDbContext _dbContext;
        private readonly ILogger<SqlArrangementRepository> _logger;

        public SqlArrangementRepository(MyDataDbContext dbContext, ILogger<SqlArrangementRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Arrangement>> GetAll()
        {
            _logger.LogDebug("GetAll: Get all arrangements");
            return await _dbContext
                .Arrangements
                .Include(a => a.City)
                .ToListAsync();
        }

        public IEnumerable<Arrangement> GetByCitySlug(string citySlug)
        {
            _logger.LogDebug("GetByCitySlug: Get arrangements in city with slug {Slug}", citySlug);
            return _dbContext
                .Cities
                .SingleOrDefault(c => c.Slug.Equals(citySlug, StringComparison.CurrentCultureIgnoreCase))
                ?.Arrangements
                ?? new List<Arrangement>();
        }

        public Task<Arrangement> GetBySlug(string citySlug, string arrangementSlug)
        {
            _logger.LogDebug("GetBySlug: Get arrangement with slug {CitySlug}/{ArrangementSlug}", citySlug, arrangementSlug);
            return _dbContext
                .Arrangements
                .Include(a => a.City)
                .SingleOrDefaultAsync(a =>
                    a.City.Slug.Equals(citySlug, StringComparison.CurrentCultureIgnoreCase)
                    && a.Slug.Equals(arrangementSlug, StringComparison.CurrentCultureIgnoreCase)
                );
        }

        public Task<Arrangement> GetById(int id)
        {
            _logger.LogDebug("GetById: Get arrangement with id {Id}", id);
            return _dbContext
                .Arrangements
                .FindAsync(id);
        }

        public async Task AddOrUpdate(Arrangement arrangement)
        {
            var serializerSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            _logger.LogDebug("AddOrUpdate: Adding or updating arrangement with id {Id} with data {Arrangement}", arrangement.Id, JsonConvert.SerializeObject(arrangement, serializerSettings));
            if (arrangement.Id == default(int))
            {
                await _dbContext.Arrangements.AddAsync(arrangement);
                _logger.LogInformation("AddOrUpdate: Updated arrangement with id {Id} with data {Arrangement}", arrangement.Id, JsonConvert.SerializeObject(arrangement, serializerSettings));
            }
            else
            {
                _dbContext.Update(arrangement);
                _logger.LogInformation("AddOrUpdate: Added arrangement with id {Id} with data {Arrangement}", arrangement.Id, JsonConvert.SerializeObject(arrangement, serializerSettings));
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            _logger.LogDebug("Remove: Removing arrangement with id {Id}", id);
            var arrangement = await GetById(id);
            if (arrangement == null)
            {
                _logger.LogInformation("Remove: Arrangement with id {Id} not found, continue", id);
                return;
            }
            _dbContext.Arrangements.Remove(arrangement);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Remove: Arrangement with id {Id} removed", id);
        }
    }
}

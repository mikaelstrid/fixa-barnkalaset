using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Arrangement> GetAll()
        {
            _logger.LogDebug("GetAll: Get all arrangements");
            return _dbContext.Arrangements.AsEnumerable();
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

        public Arrangement GetBySlug(string citySlug, string arrangementSlug)
        {
            _logger.LogDebug("GetBySlug: Get arrangement with slug {CitySlug}/{ArrangementSlug}", citySlug, arrangementSlug);
            return _dbContext
                .Arrangements
                .SingleOrDefault(a =>
                    a.CitySlug.Equals(citySlug, StringComparison.CurrentCultureIgnoreCase)
                    && a.Slug.Equals(arrangementSlug, StringComparison.CurrentCultureIgnoreCase)
                );
        }

        public Arrangement GetById(int id)
        {
            _logger.LogDebug("GetById: Get arrangement with id {Id}", id);
            return _dbContext
                .Arrangements
                .Find(id);
        }

        public void AddOrUpdate(Arrangement arrangement)
        {
            _logger.LogDebug("AddOrUpdate: Adding or updating arrangement with id {Id} with data {Arrangement}", arrangement.Id, JsonConvert.SerializeObject(arrangement));
            if (arrangement.Id == default(int))
            {
                _dbContext.Arrangements.Add(arrangement);
                _logger.LogInformation("AddOrUpdate: Updated arrangement with id {Id} with data {Arrangement}", arrangement.Id, JsonConvert.SerializeObject(arrangement));
            }
            else
            {
                _dbContext.Update(arrangement);
                _logger.LogInformation("AddOrUpdate: Added arrangement with id {Id} with data {Arrangement}", arrangement.Id, JsonConvert.SerializeObject(arrangement));
            }
            _dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            _logger.LogDebug("Remove: Removing arrangement with id {Id}", id);
            var arrangement = GetById(id);
            if (arrangement == null)
            {
                _logger.LogInformation("Remove: Arrangement with id {Id} not found, continue", id);
                return;
            }
            _dbContext.Arrangements.Remove(arrangement);
            _dbContext.SaveChanges();
            _logger.LogInformation("Remove: Arrangement with id {Id} removed", id);
        }
    }
}

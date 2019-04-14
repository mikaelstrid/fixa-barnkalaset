using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public abstract class SqlRepositoryBase<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> DbSet;

        protected MyDataDbContext DbContext;
        protected ILogger Logger;

        protected SqlRepositoryBase(MyDataDbContext dbContext, ILogger logger)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
            Logger = logger;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            Logger.LogDebug("GetAll: Get all {Entity}", typeof(TEntity).Name);
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(string id)
        {
            Logger.LogDebug("GetById: Get {Entity} with id {Id}", typeof(TEntity).Name, id);
            return await DbSet.FindAsync(id);
        }

        public virtual async Task Remove(string id)
        {
            Logger.LogDebug("Remove: Removing {Entity} with id {Id}", typeof(TEntity).Name, id);
            var existingEntity = await GetById(id);
            if (existingEntity == null)
            {
                Logger.LogWarning("Remove: {Entity} with id {Id} not found, continue", typeof(TEntity).Name, id);
                return;
            }
            DbSet.Remove(existingEntity);
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Remove: {Entity} with id {Id} removed", typeof(TEntity).Name, id);
        }

    }
}
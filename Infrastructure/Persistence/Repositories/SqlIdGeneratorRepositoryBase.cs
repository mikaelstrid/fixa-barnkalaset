using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public abstract class SqlIdGeneratorRepositoryBase<TEntity> : SqlRepositoryBase<TEntity> where TEntity : class
    {
        private const int NumberOfAttemptsToGenerateUniqueId = 10000;

        private readonly IIdGenerator _idGenerator;

        protected SqlIdGeneratorRepositoryBase(MyDataDbContext dbContext, ILogger logger, IIdGenerator idGenerator) : base(dbContext, logger)
        {
            _idGenerator = idGenerator;
        }

        protected async Task<string> GetId(string idPrefix = null)
        {
            var attempt = 0;

            while (attempt < NumberOfAttemptsToGenerateUniqueId)
            {
                attempt++;
                var id = _idGenerator.Next();

                var concatenatedId = id;
                if (idPrefix != null)
                    concatenatedId = _idGenerator.Concatenate(idPrefix, id);

                if (await GetById(concatenatedId) == null)
                {
                    Logger.LogDebug("GetId: Id '{Id}' generated in {NumberOfAttempts}", id, attempt);
                    return id;
                }
            }

            throw new Exception($"Could not generate new id in {NumberOfAttemptsToGenerateUniqueId} attempts");
        }
    }
}
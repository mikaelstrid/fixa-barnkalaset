using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public class SqlInvitationCardTemplateRepository : IInvitationCardTemplateRepository
    {
        private readonly MyDataDbContext _dbContext;
        private readonly ILogger<SqlInvitationCardTemplateRepository> _logger;

        public SqlInvitationCardTemplateRepository(MyDataDbContext dbContext, ILogger<SqlInvitationCardTemplateRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<InvitationCardTemplate>> GetAll()
        {
            _logger.LogDebug("GetAll: Get all invitation card templates");
            return await _dbContext
                .InvitationCardTemplates
                .ToListAsync();
        }

        public async Task<InvitationCardTemplate> GetById(int id)
        {
            _logger.LogDebug("GetById: Get invitation card templates with id {Id}", id);
            return await _dbContext
                .InvitationCardTemplates
                .FindAsync(id);
        }
    }
}

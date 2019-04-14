using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public class SqlInvitationRepository : SqlIdGeneratorRepositoryBase<Invitation>, IInvitationRepository
    {
        private readonly IInvitationIdGenerator _invitationIdGenerator;

        public SqlInvitationRepository(MyDataDbContext dbContext, ILogger<SqlInvitationRepository> logger, IInvitationIdGenerator invitationIdGenerator)
            : base(dbContext, logger, invitationIdGenerator)
        {
            _invitationIdGenerator = invitationIdGenerator;
        }

        public override async Task<Invitation> GetById(string id)
        {
            Logger.LogDebug("GetById: Get invitation with id {Id}", id);
            var splitId = _invitationIdGenerator.Split(id);
            return await DbSet.FindAsync(splitId.first, splitId.second);
        }

        public async Task AddOrUpdate(Invitation invitation)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(invitation, settings);

            Logger.LogDebug("AddOrUpdate: Adding or updating invitation with id {Id} with data {Entity}", invitation.Id, json);

            if (invitation.Id == null)
            {
                invitation.Id = await GetId(invitation.PartyId);
                await DbSet.AddAsync(invitation);
                Logger.LogInformation("AddOrUpdate: Added invitation with id {Id} with data {Entity}", _invitationIdGenerator.Concatenate(invitation.PartyId, invitation.Id), json);
            }
            else
            {
                var existingInvitation = await GetById(_invitationIdGenerator.Concatenate(invitation.PartyId, invitation.Id));
                existingInvitation.GuestId = invitation.GuestId;
                DbContext.Update(existingInvitation);
                Logger.LogInformation("AddOrUpdate: Updated invitation with id {Id} with data {Entity}", _invitationIdGenerator.Concatenate(invitation.PartyId, invitation.Id), json);
            }
            await DbContext.SaveChangesAsync();
        }
    }
}

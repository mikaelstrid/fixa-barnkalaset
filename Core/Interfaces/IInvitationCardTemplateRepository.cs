using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IInvitationCardTemplateRepository
    {
        Task<IEnumerable<InvitationCardTemplate>> GetAll();
        Task<InvitationCardTemplate> GetById(int id);
    }
}

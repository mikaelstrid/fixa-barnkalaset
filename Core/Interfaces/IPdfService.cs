using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IPdfService
    {
        //[Obsolete]
        //Stream GetInvitationCardsReviewStream(string reviewTemplateUrl, IEnumerable<Invitation> partyInvitations);

        byte[] GenerateInvitations(byte[] template, int instancesInTemplate, IEnumerable<Invitation> invitations);
    }
}
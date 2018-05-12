using System;
using System.Collections.Generic;
using System.IO;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IPdfService
    {
        [Obsolete]
        Stream GetInvitationCardsReviewStream(string reviewTemplateUrl, IEnumerable<Invitation> partyInvitations);

        byte[] GenerateInvitations(byte[] template, int instancesInTemplate, IEnumerable<Invitation> invitations);
    }
}
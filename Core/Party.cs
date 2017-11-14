using System;
using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Core
{
    public class Party : Entity<string>
    {
        public string User { get; set; }

        public string NameOfBirthdayChild { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string PartyType { get; set; }

        public string LocationName { get; set; }

        public string StreetAddress { get; set; }

        public string PostalCode { get; set; }

        public string PostalCity { get; set; }

        public DateTime? RsvpDate { get; set; }

        public string RsvpDescription { get; set; }

        //public int? ArrangementId { get; set; }
        //public Arrangement Arrangement { get; set; }

        public List<Invitation> Invitations { get; set; }

        public int? InvitationCardTemplateId { get; set; }
        public InvitationCardTemplate InvitationCardTemplate { get; set; }
    }
}

using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels
{
    public class WhichViewModel : InvitationViewModelBase
    {
        public IEnumerable<InvitationViewModel> Invitations { get; set; }
        
        public class InvitationViewModel
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string StreetAddress { get; set; }
            public string PostalCode { get; set; }
            public string PostalCity { get; set; }

            public string FullName => $"{FirstName} {LastName}";
            public string FullAddress => $"{StreetAddress}, {PostalCode} {PostalCity}";
        }
    }
}

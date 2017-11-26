using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels
{
    public abstract class InvitationViewModelBase
    {
        [Required]
        public string PartyId { get; set; }
    }
}
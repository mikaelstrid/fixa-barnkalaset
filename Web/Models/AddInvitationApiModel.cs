using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Models
{
    public class AddInvitationApiModel
    {
        [Required]
        public string PartyId { get; set; }
        [Required]
        public int GuestId { get; set; }
    }
}
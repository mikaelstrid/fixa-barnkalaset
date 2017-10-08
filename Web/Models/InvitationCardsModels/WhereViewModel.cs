using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels
{
    public class WhereViewModel
    {
        [Display(Name = "Vad är det för sorts kalas?")]
        public string PartyType { get; set; }
        
        [Display(Name = "Var ska ni vara?")]
        public string PartyLocationName { get; set; }

        [Required]
        [Display(Name = "Gatuadress (obligatorisk)")]
        public string StreetAddress { get; set; }

        [Display(Name = "Postnummer")]
        public string PostalCode { get; set; }

        [Display(Name = "Ort")]
        public string PostalCity { get; set; }
    }
}

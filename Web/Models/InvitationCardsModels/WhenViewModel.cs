using System;
using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels
{
    public class WhenViewModel
    {
        [Required]
        [Display(Name = "Datum (obligatoriskt)")]
        public DateTime PartyDate { get; set; }

        [Required]
        [Display(Name = "Starttid (obligatorisk)")]
        public DateTime PartyStartTime { get; set; }

        [Required]
        [Display(Name = "Sluttid (obligatorisk)")]
        public DateTime PartyEndTime { get; set; }
    }
}

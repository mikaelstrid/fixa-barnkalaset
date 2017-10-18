using System;
using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels
{
    public class WhenViewModel : InvitationViewModelBase
    {
        public string NameOfBirthdayChild { get; set; }

        [Required]
        [Display(Name = "Datum (obligatoriskt)")]
        public DateTime? PartyDate { get; set; }

        [Required]
        [Display(Name = "Starttid (obligatorisk)")]
        public DateTime? PartyStartTime { get; set; }

        [Required]
        [Display(Name = "Sluttid (obligatorisk)")]
        public DateTime? PartyEndTime { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels
{
    public class RsvpViewModel
    {
        [Display(Name = "O.S.A.-datum")]
        public DateTime RsvpDate { get; set; }

        [Display(Name = "Beskriv hur ni vill att gästerna ska svara")]
        public string Description { get; set; }
    }
}

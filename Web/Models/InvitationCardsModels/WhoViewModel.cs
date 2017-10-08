using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels
{
    public class WhoViewModel
    {
        [Required(ErrorMessage = "Fältet är obligatoriskt")]
        [Display(Name = "Vad heter födelsedagsbarnet? (obligatoriskt)")]
        public string NameOfBirthdayChild { get; set; }
    }
}

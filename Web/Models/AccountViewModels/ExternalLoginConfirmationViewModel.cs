using System.ComponentModel.DataAnnotations;

namespace Pixel.Kidsparties.Web.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

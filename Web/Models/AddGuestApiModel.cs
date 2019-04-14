using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Models
{
    public class AddGuestApiModel
    {
        [Required]
        public string PartyId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string PostalCity { get; set; }
    }
}
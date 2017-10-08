using Microsoft.AspNetCore.Mvc;

namespace Pixel.FixaBarnkalaset.Web.ApiControllers
{
    [Route("api/invitationcards")]
    public class InvitationCardsController : Controller
    {
        [Route("add-guest")]
        [HttpPost]
        public IActionResult AddGuest(AddGuestApiModel model)
        {
            return Ok();
        }
    }

    public class AddGuestApiModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string PostalCity { get; set; }
    }
}

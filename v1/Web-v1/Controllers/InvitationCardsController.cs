using Microsoft.AspNetCore.Mvc;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    [Route("inbjudningskort")]
    public class InvitationCardsController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Inbjudningskort | Fixa barnkalaset";
            ViewData["Description"] = "Vi hjälper dig att designa, trycka och skicka inbjudningskorten.";
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    [Route("osa")]
    public class RsvpController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            ViewData["Title"] = "O.S.A. | Fixa barnkalaset";
            ViewData["Description"] = "Vi hjälper dig att hålla reda på vilka som kommer och inte kommer.";
            return View();
        }
    }
}

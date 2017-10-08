using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    [Route("inbjudningskort")]
    public class InvitationCardsController : Controller
    {
        public InvitationCardsController()
        {
            ViewData["Title"] = "Inbjudningskort | Fixa barnkalaset";
            ViewData["Description"] = "Vi hjälper dig att designa, trycka och skicka inbjudningskorten.";
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("vem")]
        [Authorize]
        public IActionResult Who()
        {
            return View();
        }

        [Route("vem")]
        [HttpPost]
        [Authorize]
        public IActionResult Who(WhoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            return RedirectToAction("Where");
        }


        [Route("var")]
        [Authorize]
        public IActionResult Where()
        {
            return View();
        }

        [Route("var")]
        [HttpPost]
        [Authorize]
        public IActionResult Where(WhereViewModel model)
        {
            return RedirectToAction("When");
        }


        [Route("nar")]
        [Authorize]
        public IActionResult When()
        {
            return View();
        }

        [Route("nar")]
        [HttpPost]
        [Authorize]
        public IActionResult When(WhenViewModel model)
        {
            return RedirectToAction("Which");
        }


        [Route("vilka")]
        [Authorize]
        public IActionResult Which()
        {
            return View();
        }

        [Route("vilka")]
        [HttpPost]
        [Authorize]
        public IActionResult Which(WhichViewModel model)
        {
            return RedirectToAction("Rsvp");
        }


        [Route("osa")]
        [Authorize]
        public IActionResult Rsvp()
        {
            return View();
        }

        [Route("osa")]
        [HttpPost]
        [Authorize]
        public IActionResult Rsp(RsvpViewModel model)
        {
            return RedirectToAction("Index");
        }


        [Route("{id:regex([[\\w\\d]]{{4}})}")]
        [Authorize]
        public IActionResult Index(string id)
        {
            return View();
        }
    }
}

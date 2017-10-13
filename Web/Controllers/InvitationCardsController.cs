using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    [Route("inbjudningskort")]
    public class InvitationCardsController : Controller
    {
        private readonly ILogger<InvitationCardsController> _logger;
        private readonly IPartyRepository _partyRepository;

        public InvitationCardsController(ILogger<InvitationCardsController> logger, IPartyRepository partyRepository)
        {
            _logger = logger;
            _partyRepository = partyRepository;
            ViewData["Title"] = "Inbjudningskort | Fixa barnkalaset";
            ViewData["Description"] = "Vi hjälper dig att designa, trycka och skicka inbjudningskorten.";
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("vem-fyller-ar")]
        [Authorize]
        public IActionResult Who()
        {
            return View();
        }

        [Route("vem-fyller-ar")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Who(WhoViewModel model)
        {
            _logger.LogDebug("Who POST: called with model {Model}", JsonConvert.SerializeObject(model));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                return View(model);
            }

            var invitationCard = new Party {NameOfBirthdayChild = model.NameOfBirthdayChild};
            await _partyRepository.AddOrUpdate(invitationCard);
            _logger.LogInformation("Who POST: Created invitation card {Party}", JsonConvert.SerializeObject(invitationCard));

            return RedirectToAction("Where");
        }


        [Route("var-ar-kalaset")]
        [Authorize]
        public IActionResult Where()
        {
            return View();
        }

        [Route("var-ar-kalaset")]
        [HttpPost]
        [Authorize]
        public IActionResult Where(WhereViewModel model)
        {
            return RedirectToAction("When");
        }


        [Route("nar-ar-kalaset")]
        [Authorize]
        public IActionResult When()
        {
            return View();
        }

        [Route("nar-ar-kalaset")]
        [HttpPost]
        [Authorize]
        public IActionResult When(WhenViewModel model)
        {
            return RedirectToAction("Which");
        }


        [Route("vilka-ska-ni-bjuda")]
        [Authorize]
        public IActionResult Which()
        {
            return View();
        }

        [Route("vilka-ska-ni-bjuda")]
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

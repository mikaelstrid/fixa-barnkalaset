using System;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<InvitationCardsController> _logger;
        private readonly IPartyRepository _partyRepository;

        public InvitationCardsController(IMapper mapper, ILogger<InvitationCardsController> logger, IPartyRepository partyRepository)
        {
            _mapper = mapper;
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
                _logger.LogWarning("Who POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                return View(model);
            }

            var party = new Party {NameOfBirthdayChild = model.NameOfBirthdayChild};
            await _partyRepository.AddOrUpdate(party);
            _logger.LogInformation("Who POST: Created invitation card {Party}", JsonConvert.SerializeObject(party));

            return RedirectToAction("Where", new { partyId = party.Id });
        }


        [Route("{partyId}/var-ar-kalaset")]
        [Authorize]
        public async Task<IActionResult> Where(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);

            if (party == null)
                return NotFound();

            var viewModel = _mapper.Map<Party, WhereViewModel>(party);
            return View(viewModel);
        }

        [Route("{partyId}/var-ar-kalaset")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Where(WhereViewModel model)
        {
            _logger.LogDebug("Where POST: called with model {Model}", JsonConvert.SerializeObject(model));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Where POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                return View(model);
            }

            var existingParty = await _partyRepository.GetById(model.Id);
            if (existingParty == null)
                return NotFound();

            if (existingParty.Type != model.PartyLocationName
                || existingParty.LocationName != model.PartyLocationName
                || existingParty.StreetAddress != model.StreetAddress
                || existingParty.PostalCode != model.PostalCode
                || existingParty.PostalCity != model.PostalCity)
            {
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                _logger.LogInformation("Where POST: Edited party from {OldParty} to {NewParty}", JsonConvert.SerializeObject(existingParty, settings), JsonConvert.SerializeObject(model, settings));
                existingParty.Type = model.PartyType;
                existingParty.LocationName = model.PartyLocationName;
                existingParty.StreetAddress = model.StreetAddress;
                existingParty.PostalCode = model.PostalCode;
                existingParty.PostalCity = model.PostalCity;
                await _partyRepository.AddOrUpdate(existingParty);
            }
            else
            {
                _logger.LogInformation("Where POST: No changes detected");
            }

            return RedirectToAction("When", new { partyId = existingParty.Id });
        }


        [Route("{partyId}/nar-ar-kalaset")]
        [Authorize]
        public async Task<IActionResult> When(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);

            if (party == null)
                return NotFound();

            var viewModel = _mapper.Map<Party, WhenViewModel>(party);
            return View(viewModel);
        }

        [Route("{partyId}/nar-ar-kalaset")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> When(WhenViewModel model)
        {
            return await UpdatePartyInformation(nameof(When), nameof(Which), model,
                (p, m) => p.StartTime != ConcatenateDateAndTime(m.PartyDate, m.PartyStartTime)
                       || p.EndTime != ConcatenateDateAndTime(m.PartyDate, m.PartyEndTime),
                (m, p) =>
                {
                    p.StartTime = ConcatenateDateAndTime(m.PartyDate, m.PartyStartTime);
                    p.EndTime = ConcatenateDateAndTime(m.PartyDate, m.PartyEndTime);
                }
            );
        }

        private static DateTime ConcatenateDateAndTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
        }


        private async Task<IActionResult> UpdatePartyInformation<TViewModel>(string methodName, string redirectToAction, TViewModel model, Func<Party, TViewModel, bool> checkIfUpdatedFunc, Action<TViewModel, Party> updateAction) where TViewModel : InvitationViewModelBase
        {
            _logger.LogDebug("{MethodName} POST: called with model {model}", methodName, JsonConvert.SerializeObject(model));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("{MethodName} POST: Invalid model state {ModelState}", methodName, JsonConvert.SerializeObject(ModelState));
                return View(model);
            }

            var existingParty = await _partyRepository.GetById(model.Id);
            if (existingParty == null)
                return NotFound();

            if (checkIfUpdatedFunc(existingParty, model))
            {
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                _logger.LogInformation("{MethodName} POST: Edited party from {OldParty} to {NewParty}", methodName, JsonConvert.SerializeObject(existingParty, settings), JsonConvert.SerializeObject(model, settings));
                updateAction(model, existingParty);
                await _partyRepository.AddOrUpdate(existingParty);
            }
            else
            {
                _logger.LogInformation("{MethodName} POST: No changes detected", methodName);
            }

            return RedirectToAction(redirectToAction, new { partyId = existingParty.Id });
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

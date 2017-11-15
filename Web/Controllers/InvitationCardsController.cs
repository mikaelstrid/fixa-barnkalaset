using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly IInvitationCardTemplateRepository _invitationCardTemplateRepository;

        public InvitationCardsController(IMapper mapper, ILogger<InvitationCardsController> logger, IPartyRepository partyRepository, IInvitationCardTemplateRepository invitationCardTemplateRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _partyRepository = partyRepository;
            _invitationCardTemplateRepository = invitationCardTemplateRepository;
            ViewData["Title"] = "Inbjudningskort | Fixa barnkalaset";
            ViewData["Description"] = "Vi hjälper dig att designa, trycka och skicka inbjudningskorten.";
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }



        [Route("vem-fyller-ar")]
        public IActionResult Who()
        {
            return View();
        }

        [Route("vem-fyller-ar")]
        [HttpPost]
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
        public async Task<IActionResult> Where(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);
            if (party == null) return NotFound();
            var viewModel = _mapper.Map<Party, WhereViewModel>(party);
            return View(viewModel);
        }

        [Route("{partyId}/var-ar-kalaset")]
        [HttpPost]
        public async Task<IActionResult> Where(WhereViewModel model)
        {
            return await UpdatePartyInformation(nameof(Where), nameof(When), model,
                (p, m) => p.PartyType != m.LocationName
                          || p.LocationName != m.LocationName
                          || p.StreetAddress != m.StreetAddress
                          || p.PostalCode != m.PostalCode
                          || p.PostalCity != m.PostalCity,
                (m, p) =>
                {
                    p.PartyType = m.PartyType;
                    p.LocationName = m.LocationName;
                    p.StreetAddress = m.StreetAddress;
                    p.PostalCode = m.PostalCode;
                    p.PostalCity = m.PostalCity;
                }
            );
        }



        [Route("{partyId}/nar-ar-kalaset")]
        public async Task<IActionResult> When(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);
            if (party == null) return NotFound();
            var viewModel = _mapper.Map<Party, WhenViewModel>(party);
            return View(viewModel);
        }

        [Route("{partyId}/nar-ar-kalaset")]
        [HttpPost]
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

        private static DateTime? ConcatenateDateAndTime(DateTime? date, DateTime? time)
        {
            return date.HasValue && time.HasValue
                ? new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, 0)
                : (DateTime?) null;
        }
        

        
        [Route("{partyId}/vilka-ska-ni-bjuda")]
        public async Task<IActionResult> Which(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);
            if (party == null) return NotFound();
            var viewModel = _mapper.Map<Party, WhichViewModel>(party);
            return View(viewModel);
        }



        [Route("{partyId}/osa")]
        public async Task<IActionResult> Rsvp(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);
            if (party == null) return NotFound();
            var viewModel = _mapper.Map<Party, RsvpViewModel>(party);
            return View(viewModel);
        }

        [Route("{partyId}/osa")]
        [HttpPost]
        public async Task<IActionResult> Rsvp(RsvpViewModel model)
        {
            return await UpdatePartyInformation(nameof(Rsvp), nameof(ChooseTemplate), model,
                (p, m) => p.RsvpDate != m.RsvpDate
                       || p.RsvpDescription != m.RsvpDescription,
                (m, p) =>
                {
                    p.RsvpDate = m.RsvpDate;
                    p.RsvpDescription = m.RsvpDescription;
                }
            );
        }
        


        [Route("{partyId}/valj-mall")]
        public async Task<IActionResult> ChooseTemplate(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);
            if (party == null) return NotFound();
            var viewModel = _mapper.Map<Party, ChooseTemplateViewModel>(party);

            var availableTemplates = await _invitationCardTemplateRepository.GetAll();
            viewModel.AvailableTemplates = _mapper.Map<IEnumerable<InvitationCardTemplate>, IEnumerable<ChooseTemplateViewModel.TemplateViewModel>>(availableTemplates);
            
            return View(viewModel);
        }

        [Route("{partyId}/valj-mall")]
        [HttpPost]
        public async Task<IActionResult> ChooseTemplate(ChooseTemplateViewModel model)
        {
            return await UpdatePartyInformation(nameof(ChooseTemplate), nameof(Review), model,
                (p, m) => p.InvitationCardTemplateId != m.SelectedTemplateId,
                (m, p) => { p.InvitationCardTemplateId = m.SelectedTemplateId; }
            );
        }


        [Route("{partyId}/granska")]
        public async Task<IActionResult> Review(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);
            if (party == null) return NotFound();
            var viewModel = _mapper.Map<Party, ReviewViewModel>(party);
            return View(viewModel);
        }



        //[Route("{id:regex([[\\w\\d]]{{4}})}")]
        //public IActionResult Index(string id)
        //{
        //    return View();
        //}

        
        
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
    }
}

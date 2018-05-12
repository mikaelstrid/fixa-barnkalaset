using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IPdfService _pdfService;

        public InvitationCardsController(IMapper mapper, ILogger<InvitationCardsController> logger, IPartyRepository partyRepository, IInvitationCardTemplateRepository invitationCardTemplateRepository, IPdfService pdfService)
        {
            _mapper = mapper;
            _logger = logger;
            _partyRepository = partyRepository;
            _invitationCardTemplateRepository = invitationCardTemplateRepository;
            _pdfService = pdfService;
            ViewData["Title"] = "Inbjudningskort | Fixa barnkalaset";
            ViewData["Description"] = "Vi hjälper dig att designa, trycka och skicka inbjudningskorten.";
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("valj-mall")]
        public async Task<IActionResult> ChooseTemplate()
        {
            var viewModel = new ChooseTemplateViewModel
            {
                AvailableTemplates = await GetAvailableTemplates(_invitationCardTemplateRepository, _mapper)
            };
            return View(viewModel);
        }

        [HttpPost("valj-mall")]
        public async Task<IActionResult> ChooseTemplate(ChooseTemplateViewModel model)
        {
            _logger.LogDebug("ChooseTemplate POST: called with model {Model}", JsonConvert.SerializeObject(model));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ChooseTemplate POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                model.AvailableTemplates = await GetAvailableTemplates(_invitationCardTemplateRepository, _mapper);
                return View(model);
            }

            var party = new Party { InvitationCardTemplateId = model.SelectedTemplateId };
            await _partyRepository.AddOrUpdate(party);
            _logger.LogInformation("ChooseTemplate POST: Created entity {Party}", JsonConvert.SerializeObject(party));

            return RedirectToAction("PartyInformation", new { partyId = party.Id });
        }

        private static async Task<IEnumerable<ChooseTemplateViewModel.TemplateViewModel>> GetAvailableTemplates(IInvitationCardTemplateRepository invitationCardTemplateRepository, IMapper mapper)
        {
            var availableTemplates = await invitationCardTemplateRepository.GetAll();
            return mapper.Map<IEnumerable<InvitationCardTemplate>, IEnumerable<ChooseTemplateViewModel.TemplateViewModel>>(availableTemplates);
        }



        [HttpGet("{partyId}/kalas-info")]
        public async Task<IActionResult> PartyInformation(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);
            if (party == null) return NotFound();
            var viewModel = _mapper.Map<Party, PartyInformationViewModel>(party);
            return View(viewModel);
        }

        [HttpPost("{partyId}/kalas-info")]
        public async Task<IActionResult> PartyInformation(PartyInformationViewModel model)
        {
            return await UpdatePartyInformation(nameof(PartyInformation), nameof(Guests), model,
                (p, m) =>
                    p.NameOfBirthdayChild != m.NameOfBirthdayChild
                    || p.PartyType != m.LocationName
                    || p.LocationName != m.LocationName
                    || p.StreetAddress != m.StreetAddress
                    || p.PostalCode != m.PostalCode
                    || p.PostalCity != m.PostalCity
                    || p.StartTime != ConcatenateDateAndTime(m.PartyDate, m.PartyStartTime)
                    || p.EndTime != ConcatenateDateAndTime(m.PartyDate, m.PartyEndTime)
                    || p.RsvpDate != m.RsvpDate
                    || p.RsvpPhoneNumber != m.RsvpPhoneNumber
                    || p.RsvpEmail != m.RsvpEmail,
                (m, p) =>
                {
                    p.NameOfBirthdayChild = m.NameOfBirthdayChild;
                    p.LocationName = m.LocationName;
                    p.StreetAddress = m.StreetAddress;
                    p.PostalCode = m.PostalCode;
                    p.PostalCity = m.PostalCity;
                    p.StartTime = ConcatenateDateAndTime(m.PartyDate, m.PartyStartTime);
                    p.EndTime = ConcatenateDateAndTime(m.PartyDate, m.PartyEndTime);
                    p.RsvpDate = m.RsvpDate;
                    p.RsvpPhoneNumber = m.RsvpPhoneNumber;
                    p.RsvpEmail = m.RsvpEmail;
                }
            );
        }

        private static DateTime? ConcatenateDateAndTime(DateTime? date, DateTime? time)
        {
            return date.HasValue && time.HasValue
                ? new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, 0)
                : (DateTime?)null;
        }



        [HttpGet("{partyId}/gaster")]
        public async Task<IActionResult> Guests(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);
            if (party == null) return NotFound();
            var viewModel = _mapper.Map<Party, GuestsViewModel>(party);
            return View(viewModel);
        }



        [HttpGet("{partyId}/granska")]
        public async Task<IActionResult> Review(string partyId)
        {
            var party = await _partyRepository.GetById(partyId);
            if (party == null) return NotFound();
            var viewModel = new ReviewViewModel
            {
                PartyId = partyId,
                TemplateThumbnailUrl = party.InvitationCardTemplate.ThumbnailUrl,
                HtmlText = RenderInvitationCardTextPreview(party),
                Invitations = _mapper.Map<IEnumerable<GuestsViewModel.InvitationViewModel>>(party.Invitations)
            };
            return View(viewModel);
        }

        //[HttpGet("{partyId}/granska/ladda-ner")]
        //public async Task<IActionResult> Download(string partyId)
        //{
        //    var party = await _partyRepository.GetById(partyId);
        //    if (party == null) return NotFound();

        //    var templateBytes = System.IO.File.ReadAllBytes(party.InvitationCardTemplate.ReviewTemplateUrl, party.InvitationCardTemplate., );
        //    var reviewPdfBytes = _pdfService.GenerateInvitations()

        //    //using (var stream = _pdfService.GetInvitationCardsReviewStream(party.InvitationCardTemplate.ReviewTemplateUrl, party.Invitations))
        //    //{
        //    //    return File(stream, "application/pdf", $"inbjudningskort-{party.NameOfBirthdayChild}-{party.StartTime:yyMMdd}.pdf");
        //    //}
        //}




        private async Task<IActionResult> UpdatePartyInformation<TViewModel>(string methodName, string redirectToAction, TViewModel model, Func<Party, TViewModel, bool> checkIfUpdatedFunc, Action<TViewModel, Party> updateAction) where TViewModel : InvitationViewModelBase
        {
            _logger.LogDebug("{MethodName} POST: called with model {model}", methodName, JsonConvert.SerializeObject(model));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("{MethodName} POST: Invalid model state {ModelState}", methodName, JsonConvert.SerializeObject(ModelState));
                return View(model);
            }

            var existingParty = await _partyRepository.GetById(model.PartyId);
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

        private static string RenderInvitationCardTextPreview(Party party)
        {
            return party.InvitationCardTemplate.HtmlTemplateText
                .Replace("{NameOfBirthdayChild}", party.NameOfBirthdayChild)
                .Replace("{StartTime}", party.StartTime?.ToString("HH:mm") ?? "")
                .Replace("{EndTime}", party.EndTime?.ToString("HH:mm") ?? "")
                .Replace("{LocationName}", party.LocationName)
                .Replace("{StreetAddress}", party.StreetAddress)
                .Replace("{PostalCode}", party.PostalCode)
                .Replace("{PostalCity}", party.PostalCity)
                .Replace("{RsvpDate}", party.RsvpDate?.ToString("d MMMM"))
                .Replace("{RsvpContactInformation}", RenderRsvpContactInformation(party.RsvpPhoneNumber, party.RsvpEmail));
        }

        private static string RenderRsvpContactInformation(string phoneNumber, string email)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber) && !string.IsNullOrWhiteSpace(email))
                return $"{phoneNumber} eller {email}";
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                return phoneNumber;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!string.IsNullOrWhiteSpace(email))
                return email;
            return "";
        }
    }
}

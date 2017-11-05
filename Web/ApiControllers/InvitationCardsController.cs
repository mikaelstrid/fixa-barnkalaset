using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Models;

namespace Pixel.FixaBarnkalaset.Web.ApiControllers
{
    [Route("api/invitationcards")]
    public class InvitationCardsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IPartyRepository _partyRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IInvitationRepository _invitationRepository;

        public InvitationCardsController(
            IMapper mapper,
            ILogger<InvitationCardsController> logger, 
            IPartyRepository partyRepository, 
            IGuestRepository guestRepository,
            IInvitationRepository invitationRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _partyRepository = partyRepository;
            _guestRepository = guestRepository;
            _invitationRepository = invitationRepository;
        }

        [Route("add-guest")]
        [HttpPost]
        public async Task<IActionResult> AddGuest(AddGuestApiModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AddGuest: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                return BadRequest();
            }

            var existingParty = await _partyRepository.GetById(model.PartyId);
            if (existingParty == null)
            {
                _logger.LogWarning("AddGuest: No party with id {PartyId} found when adding guest", model.PartyId);
                return NotFound();
            }

            var guest = new Guest
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PostalCode = model.PostalCode,
                PostalCity = model.PostalCity
            };
            await _guestRepository.AddOrUpdate(guest);
            _logger.LogInformation("AddGuest: Added guest {Guest} with id {GuestId}", JsonConvert.SerializeObject(guest), guest.Id);





            return Ok();
        }

        [Route("add-invitation")]
        [HttpPost]
        public async Task<IActionResult> AddInvitation(AddInvitationApiModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AddInvitation: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                return BadRequest();
            }

            var existingParty = await _partyRepository.GetById(model.PartyId);
            if (existingParty == null)
            {
                _logger.LogWarning("AddInvitation: No party with id {PartyId} found when adding invitation", model.PartyId);
                return NotFound();
            }

            var existingGuest = await _guestRepository.GetById(model.GuestId);
            if (existingGuest == null)
            {
                _logger.LogWarning("AddInvitation: No guest with id {GuestId} found when adding invitation", model.GuestId);
                return NotFound();
            }

            var invitation = new Invitation
            {
                PartyId = model.PartyId,
                GuestId = model.GuestId
            };
            await _invitationRepository.AddOrUpdate(invitation);
            _logger.LogInformation("AddInvitation: Added invitation {Invitation} with id {InvitationId}", JsonConvert.SerializeObject(invitation), invitation.Id);

            return Ok();
        }
    }
}

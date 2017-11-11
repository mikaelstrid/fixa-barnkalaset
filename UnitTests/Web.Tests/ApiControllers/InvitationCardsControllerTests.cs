using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web;
using Pixel.FixaBarnkalaset.Web.ApiControllers;
using Pixel.FixaBarnkalaset.Web.Models;
using Xunit;

namespace UnitTests.Web.Tests.ApiControllers
{
    public class InvitationCardsControllerTests
    {
        private readonly Mock<ILogger<InvitationCardsController>> _mockLogger;
        private readonly Mock<IPartyRepository> _mockPartyRepository;
        private readonly Mock<IGuestRepository> _mockGuestRepository;
        private readonly Mock<IInvitationRepository> _mockInvitationRepository;
        private readonly InvitationCardsController _sut;

        public InvitationCardsControllerTests()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            _mockLogger = new Mock<ILogger<InvitationCardsController>>();
            _mockPartyRepository = new Mock<IPartyRepository>();
            _mockGuestRepository = new Mock<IGuestRepository>();
            _mockInvitationRepository = new Mock<IInvitationRepository>();
            _sut = new InvitationCardsController(mapper, _mockLogger.Object, _mockPartyRepository.Object, _mockGuestRepository.Object, _mockInvitationRepository.Object);
        }

        [Fact]
        public async Task AddGuest_GivenInvalidModel_ShouldReturnBadRequest()
        {
            // ARRANGE
            var model = new AddGuestApiModel();
            _sut.ModelState.AddModelError("Name", "Is invalid");

            // ACT
            var result = await _sut.AddGuest(model);

            // ASSERT
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task AddGuest_GivenUnknownPartyId_ShouldReturnNotFound()
        {
            // ARRANGE
            var model = new AddGuestApiModel { PartyId = "1234" };
            
            // ACT
            var result = await _sut.AddGuest(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(model.PartyId), Times.Once);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task AddGuest_GivenKnownPartyId_ShouldReturnOk()
        {
            // ARRANGE
            var model = new AddGuestApiModel { PartyId = "1234" };
            _mockPartyRepository.Setup(m => m.GetById(model.PartyId)).Returns(Task.FromResult(new Party()));

            // ACT
            var result = await _sut.AddGuest(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(model.PartyId), Times.Once);
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task AddGuest_GivenValidModel_ShouldCreateNewGuest()
        {
            // ARRANGE
            var model = CreateValidGuestApiModel();
            _mockPartyRepository.Setup(m => m.GetById(model.PartyId)).Returns(Task.FromResult(new Party()));

            // ACT
            await _sut.AddGuest(model);

            // ASSERT
            _mockGuestRepository.Verify(m => m.AddOrUpdate(It.IsAny<Guest>()), Times.Once);
        }




        [Fact]
        public async Task AddInvitation_GivenInvalidModel_ShouldReturnBadRequest()
        {
            // ARRANGE
            var model = new AddInvitationApiModel();
            _sut.ModelState.AddModelError("Name", "Is invalid");

            // ACT
            var result = await _sut.AddInvitation(model);

            // ASSERT
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task AddInvitation_GivenUnknownPartyId_ShouldReturnNotFound()
        {
            // ARRANGE
            var model = new AddInvitationApiModel { PartyId = "1234", GuestId = 1 };
            _mockGuestRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(Task.FromResult(new Guest()));

            // ACT
            var result = await _sut.AddInvitation(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(model.PartyId), Times.Once);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task AddInvitation_GivenUnknownGuestId_ShouldReturnNotFound()
        {
            // ARRANGE
            var model = new AddInvitationApiModel { PartyId = "1234", GuestId = 1 };
            _mockPartyRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(Task.FromResult(new Party()));

            // ACT
            var result = await _sut.AddInvitation(model);

            // ASSERT
            _mockGuestRepository.Verify(m => m.GetById(model.GuestId), Times.Once);
            result.Should().BeOfType<NotFoundResult>();
        }
        
        [Fact]
        public async Task AddInvitation_GivenValidModel_ShouldCreateInvitation()
        {
            // ARRANGE
            var model = new AddInvitationApiModel { PartyId = "1234", GuestId = 1 };
            _mockGuestRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(Task.FromResult(new Guest()));
            _mockPartyRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(Task.FromResult(new Party()));

            // ACT
            await _sut.AddInvitation(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(model.PartyId), Times.Once);
            _mockGuestRepository.Verify(m => m.GetById(model.GuestId), Times.Once);
            _mockInvitationRepository.Verify(m => m.AddOrUpdate(It.IsAny<Invitation>()), Times.Once);
        }

        

        private static AddGuestApiModel CreateValidGuestApiModel()
        {
            return new AddGuestApiModel { PartyId = "1234",  FirstName = "Leo", LastName = "Bovin", StreetAddress = "Korsvägen 1", PostalCode = "412 10", PostalCity = "GÖteborg"};
        }
    }
}

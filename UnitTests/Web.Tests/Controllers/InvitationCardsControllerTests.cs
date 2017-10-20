using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Controllers;
using Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace UnitTests.Web.Tests.Controllers
{
    public class InvitationCardsControllerTests : ControllerTestBase<InvitationCardsController>
    {
        private readonly Mock<IPartyRepository> _mockPartyRepository;

        private readonly InvitationCardsController _sut;

        public InvitationCardsControllerTests()
        {
            var mockLogger = new Mock<ILogger<InvitationCardsController>>();
            _mockPartyRepository = new Mock<IPartyRepository>();
            _sut = new InvitationCardsController(_mapper, mockLogger.Object, _mockPartyRepository.Object);
        }

        [Fact]
        public void Who_Get_ShouldOnlyReturnView()
        {
            // ARRANGE

            // ACT
            var result = _sut.Who();

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(It.IsAny<string>()), Times.Never);
            GetViewModel<WhoViewModel>(result).Should().BeNull();
        }


        [Fact]
        public async Task Who_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        {
            // ARRANGE
            var model = new WhoViewModel { NameOfBirthdayChild = "Julia" };
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.Who(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.IsAny<Party>()), Times.Never);
            GetViewModel<WhoViewModel>(result).Should().Be(model);
        }

        [Fact]
        public async Task Who_Post_GivenValidModel_ShouldCallRepository()
        {
            // ARRANGE
            var model = new WhoViewModel { NameOfBirthdayChild = "Julia" };

            // ACT
            var result = await _sut.Who(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(c => c.NameOfBirthdayChild == model.NameOfBirthdayChild)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }



        [Fact]
        public async Task Where_Get_GivenNullFromRepository_ShouldReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = await _sut.Where("PKFN");

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Where_Get_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var id = "PKFN";

            // ACT
            await _sut.Where(id);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(id), Times.Once);
        }

        [Fact]
        public async Task Where_Get_ShouldGetPartyFromRepository_AndReturnModel()
        {
            // ARRANGE
            var id = "PKFN";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle" };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.Where(id);

            // ASSERT
            GetViewModel<WhereViewModel>(result).ShouldBeEquivalentTo(party, opts => opts.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Where_Get_GivenParty_ShouldMapPartyTypeCorrectly()
        {
            // ARRANGE
            var id = "PKFN";
            var partyType = "actionkalas";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle", PartyType = partyType };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.Where(id);

            // ASSERT
            GetViewModel<WhereViewModel>(result).PartyType.Should().Be(partyType);
        }

        [Fact]
        public async Task Where_Get_GivenParty_ShouldMapPartyLocationCorrectly()
        {
            // ARRANGE
            var id = "PKFN";
            var locationName = "Hemma hos oss";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle", LocationName = locationName};
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.Where(id);

            // ASSERT
            GetViewModel<WhereViewModel>(result).LocationName.Should().Be(locationName);
        }

        [Fact]
        public async Task Where_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        {
            // ARRANGE
            var model = new WhereViewModel();
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.Where(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.IsAny<Party>()), Times.Never);
            GetViewModel<WhereViewModel>(result).Should().Be(model);
        }

        [Fact]
        public async Task Where_Post_GivenValidModel_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var model = new WhereViewModel { Id = "PKFN", StreetAddress = "Korsvägen 11" };

            // ACT
            await _sut.Where(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(model.Id), Times.Once);
        }

        [Fact]
        public async Task Where_Post_GivenValidModel_ButNoPartyInRepo_ShouldReturnNotFound()
        {
            // ARRANGE
            var model = new WhereViewModel { Id = "PKFN", StreetAddress = "Korsvägen 11" };

            // ACT
            var result = await _sut.Where(model);

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Where_Post_GivenValidModel_ButNoChanges_ShouldNotCallRepository_ButReturnRedirect()
        {
            // ARRANGE
            var model = new WhereViewModel { Id = "PKFN", StreetAddress = "Korsvägen 11" };
            _mockPartyRepository.Setup(m => m.GetById(model.Id)).Returns(Task.FromResult(new Party { Id = model.Id, StreetAddress = model.StreetAddress}));

            // ACT
            var result = await _sut.Where(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(p => p.Id == model.Id)), Times.Never);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Where_Post_GivenValidModel_ShouldCallRepository_AndReturnRedirect()
        {
            // ARRANGE
            var model = new WhereViewModel { Id = "PKFN", StreetAddress = "Korsvägen 11" };
            _mockPartyRepository.Setup(m => m.GetById(model.Id)).Returns(Task.FromResult(new Party { Id = model.Id }));

            // ACT
            var result = await _sut.Where(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(p => p.Id == model.Id)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }



        [Fact]
        public async Task When_Get_GivenNullFromRepository_ShouldReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = await _sut.When("PKFN");

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task When_Get_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var id = "PKFN";

            // ACT
            await _sut.When(id);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(id), Times.Once);
        }

        [Fact]
        public async Task When_Get_ShouldGetPartyFromRepository_AndReturnModel()
        {
            // ARRANGE
            var id = "PKFN";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle" };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.When(id);

            // ASSERT
            GetViewModel<WhenViewModel>(result).ShouldBeEquivalentTo(party, opts => opts.ExcludingMissingMembers());
        }

        [Fact]
        public async Task When_Get_GivenIfPartyHasNoPartyDate_PartyTimeShouldBeMappedCorrectly()
        {
            // ARRANGE
            var id = "PKFN";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle", StartTime = null, EndTime = null };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.When(id);

            // ASSERT
            var viewModel = GetViewModel<WhenViewModel>(result);
            viewModel.PartyDate.Should().BeNull();
            viewModel.PartyStartTime.Should().BeNull();
            viewModel.PartyEndTime.Should().BeNull();
        }

        [Fact]
        public async Task When_Get_GivenIfPartyHasPartyDate_PartyTimeShouldBeMappedCorrectly()
        {
            // ARRANGE
            var id = "PKFN";
            var startTime = DateTime.Parse("2017-10-30 13:00");
            var endTime = DateTime.Parse("2017-10-30 15:00");
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle", StartTime = startTime, EndTime = endTime};
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.When(id);

            // ASSERT
            var viewModel = GetViewModel<WhenViewModel>(result);
            viewModel.PartyDate.Should().Be(startTime.Date);
            viewModel.PartyStartTime.Should().Be(startTime);
            viewModel.PartyEndTime.Should().Be(endTime);
        }

        [Fact]
        public async Task When_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        {
            // ARRANGE
            var model = new WhenViewModel();
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.When(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.IsAny<Party>()), Times.Never);
            GetViewModel<WhenViewModel>(result).Should().Be(model);
        }

        [Fact]
        public async Task When_Post_GivenValidModel_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var model = new WhenViewModel { Id = "PKFN", PartyDate = DateTime.Parse("2017-10-17"), PartyStartTime = DateTime.Parse("2017-10-17 13:00"), PartyEndTime = DateTime.Parse("2017-10-17 15:00") };

            // ACT
            await _sut.When(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(model.Id), Times.Once);
        }

        [Fact]
        public async Task When_Post_GivenValidModel_ButNoPartyInRepo_ShouldReturnNotFound()
        {
            // ARRANGE
            var model = new WhenViewModel { Id = "PKFN", PartyDate = DateTime.Parse("2017-10-17"), PartyStartTime = DateTime.Parse("2017-10-17 13:00"), PartyEndTime = DateTime.Parse("2017-10-17 15:00") };

            // ACT
            var result = await _sut.When(model);

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task When_Post_GivenValidModel_ButNoChanges_ShouldNotCallRepository_ButReturnRedirect()
        {
            // ARRANGE
            var model = new WhenViewModel { Id = "PKFN", PartyDate = DateTime.Parse("2017-10-17"), PartyStartTime = DateTime.Parse("2017-10-17 13:00"), PartyEndTime = DateTime.Parse("2017-10-17 15:00") };
            _mockPartyRepository.Setup(m => m.GetById(model.Id)).Returns(Task.FromResult(new Party
            {
                Id = model.Id,
                StartTime = model.PartyStartTime,
                EndTime = model.PartyEndTime
            }));

            // ACT
            var result = await _sut.When(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(p => p.Id == model.Id)), Times.Never);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task When_Post_GivenValidModel_ShouldCallRepository_AndReturnRedirect()
        {
            // ARRANGE
            var model = new WhenViewModel { Id = "PKFN", PartyDate = DateTime.Parse("2017-10-17"), PartyStartTime = DateTime.Parse("2017-10-17 13:00"), PartyEndTime = DateTime.Parse("2017-10-17 15:00") };
            _mockPartyRepository.Setup(m => m.GetById(model.Id)).Returns(Task.FromResult(new Party
            {
                Id = model.Id,
                StartTime = model.PartyStartTime.Value.AddDays(2),
                EndTime = model.PartyEndTime.Value.AddDays(2)
            }));

            // ACT
            var result = await _sut.When(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(p => p.Id == model.Id)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }



        [Fact]
        public async Task Which_Get_GivenNullFromRepository_ShouldReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = await _sut.Which("PKFN");

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Which_Get_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var id = "PKFN";

            // ACT
            await _sut.Which(id);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(id), Times.Once);
        }

        [Fact]
        public async Task Which_Get_ShouldGetPartyFromRepository_AndReturnModel()
        {
            // ARRANGE
            var id = "PKFN";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle" };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.Which(id);

            // ASSERT
            GetViewModel<WhichViewModel>(result).ShouldBeEquivalentTo(party, opts => opts.ExcludingMissingMembers());
        }




        [Fact]
        public async Task Rsvp_Get_GivenNullFromRepository_ShouldReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = await _sut.Rsvp("PKFN");

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Rsvp_Get_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var id = "PKFN";

            // ACT
            await _sut.Rsvp(id);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(id), Times.Once);
        }

        [Fact]
        public async Task Rsvp_Get_ShouldGetPartyFromRepository_AndReturnModel()
        {
            // ARRANGE
            var id = "PKFN";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle" };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.Rsvp(id);

            // ASSERT
            GetViewModel<RsvpViewModel>(result).ShouldBeEquivalentTo(party, opts => opts.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Rsvp_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        {
            // ARRANGE
            var model = new RsvpViewModel();
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.Rsvp(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.IsAny<Party>()), Times.Never);
            GetViewModel<RsvpViewModel>(result).Should().Be(model);
        }

        [Fact]
        public async Task Rsvp_Post_GivenValidModel_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var model = new RsvpViewModel { Id = "PKFN" };

            // ACT
            await _sut.Rsvp(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(model.Id), Times.Once);
        }

        [Fact]
        public async Task Rsvp_Post_GivenValidModel_ButNoPartyInRepo_ShouldReturnNotFound()
        {
            // ARRANGE
            var model = new RsvpViewModel { Id = "PKFN" };

            // ACT
            var result = await _sut.Rsvp(model);

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Rsvp_Post_GivenValidModel_ButNoChanges_ShouldNotCallRepository_ButReturnRedirect()
        {
            // ARRANGE
            var model = new RsvpViewModel { Id = "PKFN", RsvpDate = DateTime.Parse("2017-10-17"), RsvpDescription = "" };
            _mockPartyRepository.Setup(m => m.GetById(model.Id)).Returns(Task.FromResult(new Party
            {
                Id = model.Id,
                RsvpDate = model.RsvpDate,
                RsvpDescription = model.RsvpDescription
            }));

            // ACT
            var result = await _sut.Rsvp(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(p => p.Id == model.Id)), Times.Never);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Rsvp_Post_GivenValidModel_ShouldCallRepository_AndReturnRedirect()
        {
            // ARRANGE
            var model = new RsvpViewModel { Id = "PKFN", RsvpDate = DateTime.Parse("2017-10-17"), RsvpDescription = "" };
            _mockPartyRepository.Setup(m => m.GetById(model.Id)).Returns(Task.FromResult(new Party
            {
                Id = model.Id,
                RsvpDate = model.RsvpDate.Value.AddDays(1),
                RsvpDescription = model.RsvpDescription
            }));

            // ACT
            var result = await _sut.Rsvp(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(p => p.Id == model.Id)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }



        [Fact]
        public async Task Review_Get_GivenNullFromRepository_ShouldReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = await _sut.Review("PKFN");

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Review_Get_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var id = "PKFN";

            // ACT
            await _sut.Review(id);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(id), Times.Once);
        }

        [Fact]
        public async Task Review_Get_ShouldGetPartyFromRepository_AndReturnModel()
        {
            // ARRANGE
            var id = "PKFN";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle" };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.Review(id);

            // ASSERT
            GetViewModel<ReviewViewModel>(result).ShouldBeEquivalentTo(party, opts => opts.ExcludingMissingMembers());
        }
    }
}

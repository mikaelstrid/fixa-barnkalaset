using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Mock<IInvitationCardTemplateRepository> _mockInvitationCardTemplateRepository;
        private readonly Mock<IPdfService> _mockPdfService;
        private readonly InvitationCardsController _sut;

        public InvitationCardsControllerTests()
        {
            var mockLogger = new Mock<ILogger<InvitationCardsController>>();
            _mockPartyRepository = new Mock<IPartyRepository>();
            _mockInvitationCardTemplateRepository = new Mock<IInvitationCardTemplateRepository>();
            _mockPdfService = new Mock<IPdfService>();
            _sut = new InvitationCardsController(_mapper, mockLogger.Object, _mockPartyRepository.Object, _mockInvitationCardTemplateRepository.Object, _mockPdfService.Object);
        }

        

        [Fact]
        public async Task ChooseTemplate_Get_ShouldReturnViewWithModelWithAvailableTemplates()
        {
            // ARRANGE
            _mockInvitationCardTemplateRepository.Setup(m => m.GetAll())
                .Returns(Task.FromResult(new List<InvitationCardTemplate> { new InvitationCardTemplate() } as IEnumerable<InvitationCardTemplate>));
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.ChooseTemplate();

            // ASSERT
            _mockInvitationCardTemplateRepository.Verify(m => m.GetAll(), Times.Once);
            GetViewModel<ChooseTemplateViewModel>(result).AvailableTemplates.Count().Should().Be(1);
        }


        [Fact]
        public async Task ChooseTemplate_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        {
            // ARRANGE
            var model = new ChooseTemplateViewModel { SelectedTemplateId = 1 };
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.ChooseTemplate(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.IsAny<Party>()), Times.Never);
            GetViewModel<ChooseTemplateViewModel>(result).Should().Be(model);
        }

        [Fact]
        public async Task ChooseTemplate_Post_GivenInvalidModel_ShouldReturnViewWithModelWithAvailableTemplates()
        {
            // ARRANGE
            _mockInvitationCardTemplateRepository.Setup(m => m.GetAll())
                .Returns(Task.FromResult(new List<InvitationCardTemplate> { new InvitationCardTemplate()} as IEnumerable<InvitationCardTemplate>));
            var model = new ChooseTemplateViewModel { SelectedTemplateId = 1 };
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.ChooseTemplate(model);

            // ASSERT
            _mockInvitationCardTemplateRepository.Verify(m => m.GetAll(), Times.Once);
            GetViewModel<ChooseTemplateViewModel>(result).AvailableTemplates.Count().Should().Be(1);
        }

        [Fact]
        public async Task ChooseTemplate_Post_GivenValidModel_ShouldCallRepository()
        {
            // ARRANGE
            var model = new ChooseTemplateViewModel { SelectedTemplateId = 1 };

            // ACT
            var result = await _sut.ChooseTemplate(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(c => c.InvitationCardTemplateId == model.SelectedTemplateId)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }
        


        [Fact]
        public async Task PartyInformation_Get_GivenNullFromRepository_ShouldReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = await _sut.PartyInformation("PKFN");

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PartyInformation_Get_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var id = "PKFN";

            // ACT
            await _sut.PartyInformation(id);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(id), Times.Once);
        }

        [Fact]
        public async Task PartyInformation_Get_ShouldGetPartyFromRepository_AndReturnModel()
        {
            // ARRANGE
            var id = "PKFN";
            var party = new Party { Id = id, InvitationCardTemplateId = 4 };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.PartyInformation(id);

            // ASSERT
            GetViewModel<PartyInformationViewModel>(result).ShouldBeEquivalentTo(party, opts => opts.ExcludingMissingMembers());
        }

        [Fact]
        public async Task PartyInformation_Get_GivenParty_ShouldMapPartyLocationCorrectly()
        {
            // ARRANGE
            var id = "PKFN";
            var locationName = "Hemma hos oss";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle", LocationName = locationName };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.PartyInformation(id);

            // ASSERT
            GetViewModel<PartyInformationViewModel>(result).LocationName.Should().Be(locationName);
        }

        [Fact]
        public async Task PartyInformation_Get_GivenIfPartyHasNoPartyDate_PartyTimeShouldBeMappedCorrectly()
        {
            // ARRANGE
            var id = "PKFN";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle", StartTime = null, EndTime = null };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.PartyInformation(id);

            // ASSERT
            var viewModel = GetViewModel<PartyInformationViewModel>(result);
            viewModel.PartyDate.Should().BeNull();
            viewModel.PartyStartTime.Should().BeNull();
            viewModel.PartyEndTime.Should().BeNull();
        }

        [Fact]
        public async Task PartyInformation_Get_GivenIfPartyHasPartyDate_PartyTimeShouldBeMappedCorrectly()
        {
            // ARRANGE
            var id = "PKFN";
            var startTime = DateTime.Parse("2017-10-30 13:00");
            var endTime = DateTime.Parse("2017-10-30 15:00");
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle", StartTime = startTime, EndTime = endTime };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.PartyInformation(id);

            // ASSERT
            var viewModel = GetViewModel<PartyInformationViewModel>(result);
            viewModel.PartyDate.Should().Be(startTime.Date);
            viewModel.PartyStartTime.Should().Be(startTime);
            viewModel.PartyEndTime.Should().Be(endTime);
        }
        
        [Fact]
        public async Task PartyInformation_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        {
            // ARRANGE
            var model = new PartyInformationViewModel();
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.PartyInformation(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.IsAny<Party>()), Times.Never);
            GetViewModel<PartyInformationViewModel>(result).Should().Be(model);
        }

        [Fact]
        public async Task PartyInformation_Post_GivenValidModel_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var model = new PartyInformationViewModel { PartyId = "PKFN", StreetAddress = "Korsvägen 11" };

            // ACT
            await _sut.PartyInformation(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(model.PartyId), Times.Once);
        }

        [Fact]
        public async Task PartyInformation_Post_GivenValidModel_ButNoPartyInRepo_ShouldReturnNotFound()
        {
            // ARRANGE
            var model = new PartyInformationViewModel { PartyId = "PKFN", StreetAddress = "Korsvägen 11" };

            // ACT
            var result = await _sut.PartyInformation(model);

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PartyInformation_Post_GivenValidModel_ButNoChanges_ShouldNotCallRepository_ButReturnRedirect()
        {
            // ARRANGE
            var model = new PartyInformationViewModel { PartyId = "PKFN", StreetAddress = "Korsvägen 11" };
            _mockPartyRepository.Setup(m => m.GetById(model.PartyId)).Returns(Task.FromResult(new Party { Id = model.PartyId, StreetAddress = model.StreetAddress }));

            // ACT
            var result = await _sut.PartyInformation(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(p => p.Id == model.PartyId)), Times.Never);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task PartyInformation_Post_GivenValidModel_ShouldCallRepository_AndReturnRedirect()
        {
            // ARRANGE
            var model = new PartyInformationViewModel { PartyId = "PKFN", StreetAddress = "Korsvägen 11" };
            _mockPartyRepository.Setup(m => m.GetById(model.PartyId)).Returns(Task.FromResult(new Party { Id = model.PartyId }));

            // ACT
            var result = await _sut.PartyInformation(model);

            // ASSERT
            _mockPartyRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(p => p.Id == model.PartyId)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }



        [Fact]
        public async Task Guests_Get_GivenNullFromRepository_ShouldReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = await _sut.Guests("PKFN");

            // ASSERT
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Guests_Get_ShouldGetPartyFromRepository()
        {
            // ARRANGE
            var id = "PKFN";

            // ACT
            await _sut.Guests(id);

            // ASSERT
            _mockPartyRepository.Verify(m => m.GetById(id), Times.Once);
        }

        [Fact]
        public async Task Guests_Get_ShouldGetPartyFromRepository_AndReturnModel()
        {
            // ARRANGE
            var id = "PKFN";
            var party = new Party { Id = id, NameOfBirthdayChild = "Kalle" };
            _mockPartyRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult(party));

            // ACT
            var result = await _sut.Guests(id);

            // ASSERT
            GetViewModel<GuestsViewModel>(result).ShouldBeEquivalentTo(party, opts => opts.ExcludingMissingMembers());
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
    }
}

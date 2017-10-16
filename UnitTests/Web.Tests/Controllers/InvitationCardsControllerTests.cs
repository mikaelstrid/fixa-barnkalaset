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

    }
}

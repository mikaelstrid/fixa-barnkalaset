using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Controllers;
using Pixel.FixaBarnkalaset.Web.Models;
using Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace UnitTests.Web.Tests.Controllers
{
    public class InvitationCardsControllerTests : ControllerTestBase<InvitationCardsController>
    {
        private readonly Mock<ILogger<InvitationCardsController>> _mockLogger;
        private readonly Mock<IPartyRepository> _mockInvitationCardRepository;

        private readonly InvitationCardsController _sut;

        public InvitationCardsControllerTests()
        {
            _mockLogger = new Mock<ILogger<InvitationCardsController>>();
            _mockInvitationCardRepository = new Mock<IPartyRepository>();
            _sut = new InvitationCardsController(_mockLogger.Object, _mockInvitationCardRepository.Object);
        }

        [Fact]
        public void Who_Get_ShouldOnlyReturnView()
        {
            // ARRANGE

            // ACT
            var result = _sut.Who();

            // ASSERT
            _mockInvitationCardRepository.Verify(m => m.GetById(It.IsAny<string>()), Times.Never);
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
            _mockInvitationCardRepository.Verify(m => m.AddOrUpdate(It.IsAny<Party>()), Times.Never);
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
            _mockInvitationCardRepository.Verify(m => m.AddOrUpdate(It.Is<Party>(c => c.NameOfBirthdayChild == model.NameOfBirthdayChild)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Controllers;
using Pixel.FixaBarnkalaset.Web.Models;
using UnitTests.Utilities.TestDataExtensions;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace UnitTests.Web.Tests.Controllers
{
    public class ArrangementsControllerTests : ControllerTestBase<ArrangementsController>
    {
        private readonly Mock<ILogger<ArrangementsController>> _mockLogger;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<IArrangementRepository> _mockArrangementRepository;
        private readonly ArrangementsController _sut;

        public ArrangementsControllerTests()
        {
            _mockLogger = new Mock<ILogger<ArrangementsController>>();
            _mockCityRepository = new Mock<ICityRepository>();
            _mockArrangementRepository = new Mock<IArrangementRepository>();
            _sut = new ArrangementsController(_mapper, _mockLogger.Object, _mockCityRepository.Object, _mockArrangementRepository.Object);
        }

        [Fact]
        public async Task Index_GivenNullResponseFromRepository_ShouldLogAndReturnNotFound()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult((City) null));
            
            // ACT
            var result = await _sut.Index("unknown_city");

            // ASSERT
            _mockCityRepository.Verify(m => m.GetBySlug(It.IsAny<string>()), Times.Once);
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Index_GivenEmptyResponseFromRepository_ShouldReturnAnEmptyResponseModel()
        {
            // ARRANGE
            var goteborg = new City().Goteborg();
            goteborg.Arrangements = new List<Arrangement>();
            _mockCityRepository.Setup(m => m.GetBySlug(goteborg.Slug)).Returns(Task.FromResult(goteborg));

            // ACT
            var result = await _sut.Index(goteborg.Slug);

            // ASSERT
            _mockCityRepository.Verify(m => m.GetBySlug(goteborg.Slug), Times.Once);
            var responseModel = (result as ViewResult).Model as ArrangementsIndexViewModel;
            responseModel.ShouldBeEquivalentTo(new ArrangementsIndexViewModel
            {
                CityName = goteborg.Name,
                CitySlug = goteborg.Slug,
                Arrangements = new List<ArrangementsIndexViewModel.ArrangementViewModel>()
            });
        }

        [Fact]
        public async Task Index_GivenOneCityWithNullArrangements_ShouldReturnAnEmptyResponseModel()
        {
            // ARRANGE
            var goteborg = new City().Goteborg();
            goteborg.Arrangements = null;
            _mockCityRepository.Setup(m => m.GetBySlug(goteborg.Slug)).Returns(Task.FromResult(goteborg));

            // ACT
            var result = await _sut.Index(goteborg.Slug);

            // ASSERT
            _mockCityRepository.Verify(m => m.GetBySlug(goteborg.Slug), Times.Once);
            var responseModel = (result as ViewResult).Model as ArrangementsIndexViewModel;
            responseModel.ShouldBeEquivalentTo(new ArrangementsIndexViewModel
            {
                CityName = goteborg.Name,
                CitySlug = goteborg.Slug,
                Arrangements = new List<ArrangementsIndexViewModel.ArrangementViewModel>()
            });
        }

        [Fact]
        public async Task Index_GivenCityWithTwoArrangements_ShouldReturnCityWithTwoArrangementsInAlphabeticalOrder_InTheResponseModel()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var laserdome = halmstad.Laserdome();
            var busfabriken = halmstad.Busfabriken();
            _mockCityRepository.Setup(m => m.GetBySlug(halmstad.Slug)).Returns(Task.FromResult(halmstad));

            // ACT
            var result = await _sut.Index(halmstad.Slug);

            // ASSERT
            _mockCityRepository.Verify(m => m.GetBySlug(halmstad.Slug), Times.Once);
            var responseModel = (result as ViewResult).Model as ArrangementsIndexViewModel;
            responseModel.ShouldBeEquivalentTo(new ArrangementsIndexViewModel
            {
                CityName = halmstad.Name,
                CitySlug = halmstad.Slug,
                Arrangements = _mapper.Map<IEnumerable<Arrangement>, IEnumerable<ArrangementsIndexViewModel.ArrangementViewModel>>(new List<Arrangement> { busfabriken, laserdome })
            });
        }



        [Fact]
        public async Task Details_GivenUnknownCity_ShouldLogAndReturnNotFound()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult((City)null));

            // ACT
            var result = await _sut.Details("unknown_city_slug", "unknown_arrangement_slug");

            // ASSERT
            _mockCityRepository.Verify(m => m.GetBySlug(It.IsAny<string>()), Times.Once);
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Details_GivenKnownCityButUnknownArrangement_ShouldLogAndReturnNotFound()
        {
            // ARRANGE
            var malmo = new City().Malmo();
            _mockCityRepository.Setup(m => m.GetBySlug(malmo.Slug)).Returns(Task.FromResult(malmo));
            _mockArrangementRepository.Setup(m => m.GetBySlug(malmo.Slug, "unknown_arrangement_slug")).Returns(Task.FromResult((Arrangement) null));

            // ACT
            var result = await _sut.Details(malmo.Slug, "unknown_arrangement_slug");

            // ASSERT
            _mockCityRepository.Verify(m => m.GetBySlug(It.IsAny<string>()), Times.Once);
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Details_GivenArrangementMatchingSlugs_ShouldReturnResponseModel()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var busfabriken = halmstad.Busfabriken();
            _mockCityRepository.Setup(m => m.GetBySlug(halmstad.Slug)).Returns(Task.FromResult(halmstad));
            _mockArrangementRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));

            // ACT
            var result = await _sut.Details(halmstad.Slug, busfabriken.Slug);

            // ASSERT
            _mockCityRepository.Verify(m => m.GetBySlug(It.IsAny<string>()), Times.Once);
            _mockArrangementRepository.Verify(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug), Times.Once);
            var responseModel = (result as ViewResult).Model as ArrangementDetailsViewModel;
            responseModel.ShouldBeEquivalentTo(_mapper.Map<Arrangement, ArrangementDetailsViewModel>(busfabriken));
        }
    }
}

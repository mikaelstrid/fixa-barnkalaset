using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web;
using Pixel.FixaBarnkalaset.Web.Controllers;
using Pixel.FixaBarnkalaset.Web.Models;
using UnitTests.Utilities.TestDataExtensions;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace UnitTests.Web.Tests.Controllers
{
    public class ArrangementsControllerTests : ControllerTestBase<ArrangementsController>
    {
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<IArrangementRepository> _mockArrangementRepository;
        private readonly ArrangementsController _sut;

        public ArrangementsControllerTests()
        {
            _mockCityRepository = new Mock<ICityRepository>();
            _mockArrangementRepository = new Mock<IArrangementRepository>();
            _sut = new ArrangementsController(_mapper, _mockCityRepository.Object, _mockArrangementRepository.Object);
        }

        [Fact]
        public async Task Index_GivenNullResponseFromRepository_ShouldReturnNotFound()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult((City) null));
            
            // ACT
            var result = await _sut.Index("unknown_city");

            // ASSERT
            _mockCityRepository.Verify(m => m.GetBySlug(It.IsAny<string>()), Times.Once);
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
        public async Task Index_GivenCityWithTwoArrangements_ShouldReturnCityWithTwoArrangementsInTheResponseModel()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var busfabriken = halmstad.Busfabriken();
            var laserdome = halmstad.Laserdome();
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
                Arrangements = _mapper.Map<IEnumerable<Arrangement>, IEnumerable<ArrangementsIndexViewModel.ArrangementViewModel>>(halmstad.Arrangements)
            });
        }
    }
}

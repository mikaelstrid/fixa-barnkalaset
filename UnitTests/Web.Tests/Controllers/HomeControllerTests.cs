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
using Pixel.FixaBarnkalaset.Web.Utilities;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace UnitTests.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController _sut;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<ISitemapGenerator> _mockSitemapGenerator;

        public HomeControllerTests()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            _mockCityRepository = new Mock<ICityRepository>();
            _mockSitemapGenerator = new Mock<ISitemapGenerator>();

            _sut = new HomeController(mapper, _mockCityRepository.Object, _mockSitemapGenerator.Object);
        }

        [Fact]
        public async Task Index_GivenNullResponseFromRepository_ShouldReturnAnEmptyResponseModel()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<City>) null));
            
            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockCityRepository.Verify(m => m.GetAll(), Times.Once);
            var responseModel = (result as ViewResult).Model as HomeIndexViewModel;
            responseModel.Should().NotBeNull();
            responseModel.Cities.Should().NotBeNull();
            responseModel.Cities.Should().BeEmpty();
        }

        [Fact]
        public async Task Index_GivenEmptyResponseFromRepository_ShouldReturnAnEmptyResponseModel()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<City>) new List<City>()));

            // ACT
            var result =await  _sut.Index();

            // ASSERT
            _mockCityRepository.Verify(m => m.GetAll(), Times.Once);
            var responseModel = (result as ViewResult).Model as HomeIndexViewModel;
            responseModel.Should().NotBeNull();
            responseModel.Cities.Should().NotBeNull();
            responseModel.Cities.Should().BeEmpty();
        }

        [Fact]
        public async Task Index_GivenTwoCities_ShouldReturnToMappedCitiesInTheResponseModel()
        {
            // ARRANGE
            var cities = new List<City>
            {
                new City("Halmstad", "halmstad", 10.1, 11.2),
                new City("Borås", "boras", 78.1, -178.1)
            };
            _mockCityRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<City>) cities));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            ((result as ViewResult).Model as HomeIndexViewModel).Cities.ShouldBeEquivalentTo(new List<HomeIndexViewModel.CityViewModel>
            {
                new HomeIndexViewModel.CityViewModel { Name = "Halmstad", Slug = "halmstad" },
                new HomeIndexViewModel.CityViewModel { Name = "Borås", Slug = "boras" },
            });
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Domain.Commands;
using Pixel.FixaBarnkalaset.Web;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;
using Xunit;

namespace UnitTests.Web.Tests.Admin.Controllers
{
    public class CitiesControllerTests
    {
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<ICityService> _mockCityService;
        private readonly CitiesController _sut;

        public CitiesControllerTests()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            _mockCityService = new Mock<ICityService>();
            _mockCityRepository = new Mock<ICityRepository>();
            _sut = new CitiesController(mapper, _mockCityRepository.Object, _mockCityService.Object);
        }

        [Fact]
        public void Index_GivenOneCityWithTwoArrangements_ShouldReturnOneCity()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetAll()).Returns(new List<City>
            {
                new City
                {
                    Arrangements = new List<Arrangement>
                    {
                        new Arrangement(),
                        new Arrangement()
                    }
                }
            });

            // ACT
            var result = _sut.Index();

            // ASSERT
            var model = (result as ViewResult).Model as IEnumerable<IndexCityViewModel>;
            Assert.Equal(1, model.Count());
            Assert.Equal(2, model.First().ArrangementsCount);
        }

        [Fact]
        public void Create_Get_ShouldOnlyReturnView()
        {
            // ARRANGE

            // ACT
            var result = _sut.Create();

            // ASSERT
            _mockCityService.Verify(m => m.When(It.IsAny<CreateCity>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().BeNull();
        }
        
        [Fact]
        public void Create_Post_GivenValidModel_ShouldCallService()
        {
            // ARRANGE
            var model = new CreateOrEditCityViewModel
            {
                Name = "Halmstad",
                Slug = "halmstad",
                Latitude = 10.1,
                Longitude = 58.7
            };
            CreateCity createdCommand = null;
            _mockCityService.Setup(m => m.When(It.IsAny<CreateCity>())).Callback<CreateCity>(cmd => createdCommand = cmd);

            // ACT
            var result = _sut.Create(model);
            
            // ASSERT
            _mockCityService.Verify(m => m.When(It.IsAny<CreateCity>()), Times.Once);
            Assert.NotNull(createdCommand);
            createdCommand.ShouldBeEquivalentTo(model);
        }

        [Fact]
        public async Task Create_Post_GivenInvalidModel_ShouldOnlyReturnView()
        {
            // ARRANGE
            var model = new CreateOrEditCityViewModel();
            _sut.ModelState.AddModelError("key", "error message");

            // ACT
            var result = await _sut.Create(model);

            // ASSERT
            _mockCityService.Verify(m => m.When(It.IsAny<CreateCity>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().BeNull();
        }
    }
}

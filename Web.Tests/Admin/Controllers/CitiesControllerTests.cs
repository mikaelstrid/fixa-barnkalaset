using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Commands;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;
using Xunit;

namespace Web.Tests.Admin.Controllers
{
    public class CitiesControllerTests
    {
        private readonly Mapper _mapper;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<ICityService> _mockCityService;
        private readonly CitiesController _sut;

        public CitiesControllerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            _mockCityService = new Mock<ICityService>();
            _mockCityRepository = new Mock<ICityRepository>();
            _sut = new CitiesController(_mapper, _mockCityRepository.Object, _mockCityService.Object);
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
        public void Create_GivenValidModel_ShouldCallService()
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
    }
}

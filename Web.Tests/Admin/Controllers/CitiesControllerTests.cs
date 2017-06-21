using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;
using Xunit;

namespace Pixel.FixaBarnkalaset.Web.Tests.Admin.Controllers
{
    public class CitiesControllerTests
    {
        private readonly Mapper _mapper;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<ICityService> _mockCityService;

        public CitiesControllerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            _mockCityService = new Mock<ICityService>();
            _mockCityRepository = new Mock<ICityRepository>();
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

            var sut = new CitiesController(_mapper, _mockCityRepository.Object, _mockCityService.Object);

            // ACT
            var result = sut.Index();

            // ASSERT
            var model = (result as ViewResult).Model as IEnumerable<IndexCityViewModel>;
            Assert.Equal(1, model.Count());
            Assert.Equal(2, model.First().ArrangementsCount);
        }
    }
}

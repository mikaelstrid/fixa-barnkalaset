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
        [Fact]
        public void Index_GivenOneCityWithTwoArrangements_ShouldReturnOneCity()
        {
            // ARRANGE
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            var mockCityRepository = new Mock<ICityRepository>();
            mockCityRepository.Setup(m => m.GetAll()).Returns(new List<City>
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

            var sut = new CitiesController(mapper, mockCityRepository.Object);

            // ACT
            var result = sut.Index();

            // ASSERT
            var model = (result as ViewResult).Model as IEnumerable<IndexCityViewModel>;
            Assert.Equal(1, model.Count());
            Assert.Equal(2, model.First().ArrangementsCount);
        }
    }
}

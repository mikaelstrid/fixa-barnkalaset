using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Domain.Commands;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Pixel.FixaBarnkalaset.Web;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;
using Xunit;

namespace UnitTests.Web.Tests.Admin.Controllers
{
    public class CitiesControllerTests
    {
        private readonly Mock<ILogger<CitiesController>> _mockLogger;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<ICityService> _mockCityService;
        private readonly Mock<IViewRepository> _mockViewRepository;
        private readonly Mock<ISlugDictionary> _mockSlugDictionary;
        private readonly CitiesController _sut;

        public CitiesControllerTests()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            _mockLogger = new Mock<ILogger<CitiesController>>();
            _mockCityService = new Mock<ICityService>();
            _mockCityRepository = new Mock<ICityRepository>();
            _mockViewRepository = new Mock<IViewRepository>();
            _mockSlugDictionary = new Mock<ISlugDictionary>();
            _sut = new CitiesController(mapper, _mockLogger.Object, _mockCityRepository.Object, _mockCityService.Object, _mockSlugDictionary.Object, _mockViewRepository.Object);
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


        [Fact]
        public void Edit_Get_GivenNoCityMatchingSlug_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = _sut.Edit("unknown_slug");

            // ASSERT
            //_mockLogger.Verify(m => m.LogWarning(It.IsAny<string>()));
            _mockLogger.Verify(
                m => m.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    //It.Is<FormattedLogValues>(v => v.ToString().Contains("CreateInvoiceFailed")),
                    It.IsAny<FormattedLogValues>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Edit_Get_GivenNoCityViewMatchingIdFound_ShouldLogError_AndReturnNotFound()
        {
            // ARRANGE
            var slug = "unknown_slug";
            var id = Guid.Parse("3B88F709-C499-4016-AA1F-883A071CE829");
            _mockSlugDictionary.Setup(m => m.GetId(slug)).Returns(id);
            _mockViewRepository.Setup(m => m.Get<CityView>(id)).Returns((CityView)null);

            // ACT
            var result = _sut.Edit(slug);

            // ASSERT
            _mockLogger.Verify(
                m => m.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    //It.Is<FormattedLogValues>(v => v.ToString().Contains("CreateInvoiceFailed")),
                    It.IsAny<FormattedLogValues>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );
            result.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public void Edit_Get_ShouldGetView_AndReturnItAsModel()
        {
            // ARRANGE
            var id = Guid.Parse("3BCC0ADB-3A44-425F-926C-B2202A23D0C7");
            var name = "Kungsbacka";
            var slug = "kungsbacka";
            var latitude = 12.8;
            var longitude = 67.2;
            var view = new CityView
            {
                Name = name,
                Slug = slug,
                Latitude = latitude,
                Longitude = longitude
            };
            _mockSlugDictionary.Setup(m => m.GetId(view.Slug)).Returns(id);
            _mockViewRepository.Setup(m => m.Get<CityView>(id)).Returns(view);

            // ACT
            var result = _sut.Edit(slug);

            // ASSERT
            _mockSlugDictionary.Verify(m => m.GetId(slug), Times.Once);
            _mockViewRepository.Verify(m => m.Get<CityView>(id), Times.Once);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.ShouldBeEquivalentTo(new CreateOrEditCityViewModel
            {
                Name = name,
                Slug = slug,
                Latitude = latitude,
                Longitude = longitude
            });
        }



    }
}

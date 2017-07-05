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
using Pixel.FixaBarnkalaset.Web;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;
using UnitTests.Utilities.TestDataExtensions;
using Xunit;

namespace UnitTests.Web.Tests.Admin.Controllers
{
    public class CitiesControllerTests
    {
        private readonly Mock<ILogger<CitiesController>> _mockLogger;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly CitiesController _sut;

        public CitiesControllerTests()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            _mockLogger = new Mock<ILogger<CitiesController>>();
            _mockCityRepository = new Mock<ICityRepository>();
            _sut = new CitiesController(mapper, _mockLogger.Object, _mockCityRepository.Object);
        }

        [Fact]
        public async Task Index_GivenNullReturnFromRepository_ShouldReturnModelWithNoCities()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<City>) null));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockCityRepository.Verify(m => m.GetAll(), Times.Once);
            var model = (result as ViewResult).Model as CitiesIndexViewModel;
            model.Should().NotBeNull();
            model.Cities.Should().NotBeNull();
            model.Cities.Count().Should().Be(0);
        }

        [Fact]
        public async Task Index_GivenNoCitiesFromRepository_ShouldReturnModelWithNoCities()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<City>) new List<City>()));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockCityRepository.Verify(m => m.GetAll(), Times.Once);
            var model = (result as ViewResult).Model as CitiesIndexViewModel;
            model.Should().NotBeNull();
            model.Cities.Should().NotBeNull();
            model.Cities.Count().Should().Be(0);
        }

        [Fact]
        public async Task Index_GivenTwoCitiesFromRepository_ShouldReturnModelWithTwoCities()
        {
            // ARRANGE
            var cities = new List<City> { new City().Halmstad(), new City().Vaxjo() };
            _mockCityRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<City>) cities));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockCityRepository.Verify(m => m.GetAll(), Times.Once);
            var model = (result as ViewResult).Model as CitiesIndexViewModel;
            model.Cities.Count().Should().Be(2);
            model.Cities.ShouldBeEquivalentTo(cities, opt => opt.ExcludingMissingMembers());
        }



        [Fact]
        public void Create_Get_ShouldOnlyReturnView()
        {
            // ARRANGE

            // ACT
            var result = _sut.Create();

            // ASSERT
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().BeNull();
        }

        [Fact]
        public async Task Create_Post_GivenValidModel_ShouldCallRepository()
        {
            // ARRANGE
            var city = new City().Halmstad();
            var model = CreateCreateOrEditCityViewModel(city);
            City createdCity = null;
            _mockCityRepository.Setup(m => m.AddOrUpdate(It.IsAny<City>()))
                .Callback<City>(c => createdCity = c)
                .Returns(Task.CompletedTask);

            // ACT
            await _sut.Create(model);

            // ASSERT
            createdCity.ShouldBeEquivalentTo(city);
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Once);
        }

        [Fact]
        public async Task Create_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        {
            // ARRANGE
            var model = CreateCreateOrEditCityViewModel(new City().Halmstad());
            AddModelStateError();

            // ACT
            var result = await _sut.Create(model);

            // ASSERT
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().Be(model);
        }

        [Fact]
        public async Task Create_Post_GivenExistingSlug_ShouldAddModelStateError_AndReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var model = CreateCreateOrEditCityViewModel(new City().Halmstad());
            _mockCityRepository.Setup(m => m.GetBySlug(model.Slug)).Returns(Task.FromResult(new City().Halmstad()));

            // ACT
            var result = await _sut.Create(model);

            // ASSERT
            _sut.ModelState.IsValid.Should().BeFalse();
            VerifyLogging(LogLevel.Warning);
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().Be(model);
        }



        [Fact]
        public async Task Edit_Get_GivenUnknownSlug_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult((City) null));

            // ACT
            var result = await _sut.Edit("unknown_slug");

            // ASSERT
            VerifyLogging(LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Get_ShouldGetView_AndReturnItAsModel()
        {
            // ARRANGE
            var city = new City().Vaxjo();
            _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));

            // ACT
            var result = await _sut.Edit(city.Slug);

            // ASSERT
            _mockCityRepository.Verify(m => m.GetBySlug(city.Slug), Times.Once);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.ShouldBeEquivalentTo(city, opt => opt.ExcludingMissingMembers());
        }
        
        [Fact]
        public async Task Edit_Post_GivenUnknownSlug_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = await _sut.Edit("unknown_slug", CreateCreateOrEditCityViewModel(new City().Halmstad()));

            // ASSERT
            VerifyLogging(LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenInvalidModel_ShouldLogWarning_AndOnlyReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var city = new City().Halmstad();
            var viewModel = CreateCreateOrEditCityViewModel(city);
            _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));
            AddModelStateError();

            // ACT
            var result = await _sut.Edit(city.Slug, viewModel);

            // ASSERT
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().Be(viewModel);
            VerifyLogging(LogLevel.Warning);
        }

        [Fact]
        public async Task Edit_Post_GivenNoChanges_ShouldLogInformation_AndNotUpdateTheDatabase()
        {
            // ARRANGE
            var city = new City().Halmstad();
            var viewModel = CreateCreateOrEditCityViewModel(city);
            _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));

            // ACT
            var result = await _sut.Edit(city.Slug, viewModel);

            // ASSERT
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
            VerifyLogging(LogLevel.Information);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedCityName_ShouldLogInformation_AndUpdateCityInDatabase()
        {
            // ARRANGE
            var city = new City().Halmstad();
            var viewModel = CreateCreateOrEditCityViewModel(city);
            _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));
            var changedName = "Halmstad II";
            viewModel.Name = changedName;

            // ACT
            var result = await _sut.Edit(city.Slug, viewModel);

            // ASSERT
            VerifyLogging(LogLevel.Information);
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.Is<City>(c => c.Name == changedName)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedCitySlug_ShouldLogInformation_AndUpdateCityInDatabase()
        {
            // ARRANGE
            var city = new City().Halmstad();
            var viewModel = CreateCreateOrEditCityViewModel(city);
            _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));
            var changedSlug = "halmstad-ii";
            viewModel.Slug = changedSlug;

            // ACT
            var result = await _sut.Edit(city.Slug, viewModel);

            // ASSERT
            VerifyLogging(LogLevel.Information);
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.Is<City>(c => c.Slug == changedSlug)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }



        private static CreateOrEditCityViewModel CreateCreateOrEditCityViewModel(City city)
        {
            return new CreateOrEditCityViewModel
            {
                Name = city.Name,
                Slug = city.Slug,
                Latitude = city.Latitude,
                Longitude = city.Longitude
            };
        }

        private void AddModelStateError()
        {
            _sut.ModelState.AddModelError("key", "error message");
        }

        private void VerifyLogging(LogLevel logLevel)
        {
            _mockLogger.Verify(
                m => m.Log(
                    logLevel,
                    It.IsAny<EventId>(),
                    //It.Is<FormattedLogValues>(v => v.ToString().Contains("CreateInvoiceFailed")),
                    It.IsAny<FormattedLogValues>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );
        }
    }
}

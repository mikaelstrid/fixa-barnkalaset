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
using UnitTests.Utilities.TestDataExtensions;
using Xunit;

namespace UnitTests.Web.Tests.Admin.Controllers
{
    public class CitiesControllerTests
    {
        private readonly Mock<ILogger<CitiesController>> _mockLogger;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<ICityService> _mockCityService;
        private readonly Mock<IViewRepository> _mockViewRepository;
        private readonly Mock<ISlugLookup> _mockSlugLookup;
        private readonly CitiesController _sut;

        public CitiesControllerTests()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            _mockLogger = new Mock<ILogger<CitiesController>>();
            _mockCityRepository = new Mock<ICityRepository>();
            _mockCityService = new Mock<ICityService>();
            _mockViewRepository = new Mock<IViewRepository>();
            _mockSlugLookup = new Mock<ISlugLookup>();
            _sut = new CitiesController(mapper, _mockLogger.Object, _mockCityRepository.Object, _mockCityService.Object, _mockSlugLookup.Object, _mockViewRepository.Object);
        }

        [Fact]
        public void Index_GivenNullReturnFromRepository_ShouldReturnModelWithNoCities()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetAll()).Returns((IEnumerable<City>) null);

            // ACT
            var result = _sut.Index();

            // ASSERT
            _mockCityRepository.Verify(m => m.GetAll(), Times.Once);
            var model = (result as ViewResult).Model as CitiesIndexViewModel;
            model.Should().NotBeNull();
            model.Cities.Should().NotBeNull();
            model.Cities.Count().Should().Be(0);
        }

        [Fact]
        public void Index_GivenNoCitiesFromRepository_ShouldReturnModelWithNoCities()
        {
            // ARRANGE
            _mockCityRepository.Setup(m => m.GetAll()).Returns(new List<City>());

            // ACT
            var result = _sut.Index();

            // ASSERT
            _mockCityRepository.Verify(m => m.GetAll(), Times.Once);
            var model = (result as ViewResult).Model as CitiesIndexViewModel;
            model.Should().NotBeNull();
            model.Cities.Should().NotBeNull();
            model.Cities.Count().Should().Be(0);
        }

        [Fact]
        public void Index_GivenTwoCitiesFromRepository_ShouldReturnModelWithTwoCities()
        {
            // ARRANGE
            var cities = new List<City> { new City().Halmstad(), new City().Vaxjo() };
            _mockCityRepository.Setup(m => m.GetAll()).Returns(cities);

            // ACT
            var result = _sut.Index();

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
                .Callback<City>(c => createdCity = c);

            // ACT
            await _sut.Create(model);

            // ASSERT
            createdCity.ShouldBeEquivalentTo(city);
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Once);
        }

        [Fact]
        public async Task Create_Post_GivenInvalidModel_ShouldOnlyReturnView()
        {
            // ARRANGE
            var model = CreateCreateOrEditCityViewModel(new City().Halmstad());
            AddModelStateError();

            // ACT
            var result = await _sut.Create(model);

            // ASSERT
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
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
            VerifyLogging(LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Edit_Get_GivenNoCityViewMatchingIdFound_ShouldLogError_AndReturnNotFound()
        {
            // ARRANGE
            var slug = "halmstad";
            var id = Guid.Parse("3B88F709-C499-4016-AA1F-883A071CE829");
            SetupSlugAndView(slug, id, null);

            // ACT
            var result = _sut.Edit(slug);

            // ASSERT
            VerifyLogging(LogLevel.Error);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Edit_Get_ShouldGetView_AndReturnItAsModel()
        {
            // ARRANGE
            var view = CreateKungsbackaCityView();
            SetupSlugAndView(view.Slug, view.Id, view);

            // ACT
            var result = _sut.Edit(view.Slug);

            // ASSERT
            _mockSlugLookup.Verify(m => m.GetIdBySlug(view.Slug), Times.Once);
            _mockViewRepository.Verify(m => m.Get<CityView>(view.Id), Times.Once);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.ShouldBeEquivalentTo(new CreateOrEditCityViewModel
            {
                Name = view.Name,
                Slug = view.Slug,
                Latitude = view.Latitude,
                Longitude = view.Longitude
            });
        }


        [Fact]
        public async Task Edit_Post_GivenNoCityMatchingSlug_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = await _sut.Edit("unknown_slug", CreateCreateOrEditCityViewModel(new City().Halmstad()));

            // ASSERT
            VerifyLogging(LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenNoCityViewMatchingIdFound_ShouldLogError_AndReturnNotFound()
        {
            // ARRANGE
            var viewModel = CreateCreateOrEditCityViewModel(new City().Halmstad());
            var id = Guid.Parse("3B88F709-C499-4016-AA1F-883A071CE829");
            SetupSlugAndView(viewModel.Slug, id, null);

            // ACT
            var result = await _sut.Edit(viewModel.Slug, viewModel);

            // ASSERT
            VerifyLogging(LogLevel.Error);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenInvalidModel_ShouldOnlyReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var viewModel = CreateCreateOrEditCityViewModel(new City().Halmstad());
            var view = CreateHalmstadCityView();
            SetupSlugAndView(viewModel.Slug, view.Id, view);
            AddModelStateError();

            // ACT
            var result = await _sut.Edit(viewModel.Slug, viewModel);

            // ASSERT
            _mockCityService.Verify(m => m.When(It.IsAny<CreateCity>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().Be(viewModel);
        }

        [Fact]
        public async Task Edit_Post_GivenNoChanges_ShouldNoSendAndCommands()
        {
            // ARRANGE
            var view = CreateHalmstadCityView();
            var viewModel = CreateCreateOrEditCityViewModel(new City().Halmstad());
            SetupSlugAndView(viewModel.Slug, view.Id, view);

            // ACT
            var result = await _sut.Edit(viewModel.Slug, viewModel);

            // ASSERT
            _mockViewRepository.Verify(m => m.Get<CityView>(view.Id), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<CreateCity>()), Times.Never);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityName>()), Times.Never);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedCityName_ShouldSendChangeCityNameCommand()
        {
            // ARRANGE
            var view = CreateHalmstadCityView();
            var viewModel = CreateCreateOrEditCityViewModel(new City().Halmstad());
            var changedName = "Halmstad II";
            viewModel.Name = changedName;
            SetupSlugAndView(viewModel.Slug, view.Id, view);

            // ACT
            var result = await _sut.Edit(viewModel.Slug, viewModel);

            // ASSERT
            _mockViewRepository.Verify(m => m.Get<CityView>(view.Id), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityName>()), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCitySlug>()), Times.Never);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityPosition>()), Times.Never);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedCitySlug_ShouldSendChangeCitySlugCommand()
        {
            // ARRANGE
            var view = CreateHalmstadCityView();
            var viewModel = CreateCreateOrEditCityViewModel(new City().Halmstad());
            var changedSlug = "Halmstad";
            viewModel.Slug = changedSlug;
            SetupSlugAndView(viewModel.Slug, view.Id, view);

            // ACT
            var result = await _sut.Edit(viewModel.Slug, viewModel);

            // ASSERT
            _mockViewRepository.Verify(m => m.Get<CityView>(view.Id), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityName>()), Times.Never);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCitySlug>()), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityPosition>()), Times.Never);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedCityLatitude_ShouldSendChangeCityPositionCommand()
        {
            // ARRANGE
            var view = CreateHalmstadCityView();
            var viewModel = CreateCreateOrEditCityViewModel(new City().Halmstad());
            viewModel.Latitude = 18.7;
            SetupSlugAndView(viewModel.Slug, view.Id, view);

            // ACT
            var result = await _sut.Edit(viewModel.Slug, viewModel);

            // ASSERT
            _mockViewRepository.Verify(m => m.Get<CityView>(view.Id), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityName>()), Times.Never);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCitySlug>()), Times.Never);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityPosition>()), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedCityLongitude_ShouldSendChangeCityPositionCommand()
        {
            // ARRANGE
            var view = CreateHalmstadCityView();
            var viewModel = CreateCreateOrEditCityViewModel(new City().Halmstad());
            viewModel.Longitude = 18.7;
            SetupSlugAndView(viewModel.Slug, view.Id, view);

            // ACT
            var result = await _sut.Edit(viewModel.Slug, viewModel);

            // ASSERT
            _mockViewRepository.Verify(m => m.Get<CityView>(view.Id), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityName>()), Times.Never);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCitySlug>()), Times.Never);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityPosition>()), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedCityLatitideAndLongitude_ShouldSendChangeCityPositionCommandOnce()
        {
            // ARRANGE
            var view = CreateHalmstadCityView();
            var viewModel = CreateCreateOrEditCityViewModel(new City().Halmstad());
            viewModel.Latitude = 13.7;
            viewModel.Longitude = 18.7;
            SetupSlugAndView(viewModel.Slug, view.Id, view);

            // ACT
            var result = await _sut.Edit(viewModel.Slug, viewModel);

            // ASSERT
            _mockViewRepository.Verify(m => m.Get<CityView>(view.Id), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityName>()), Times.Never);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCitySlug>()), Times.Never);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityPosition>()), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedCityAllProperties_ShouldSendAllCommandsOnce()
        {
            // ARRANGE
            var view = CreateHalmstadCityView();
            var viewModel = CreateCreateOrEditCityViewModel(new City().Halmstad());
            viewModel.Name = "Halmstad II";
            viewModel.Slug = "Halmstad";
            viewModel.Latitude = 13.7;
            viewModel.Longitude = 18.7;
            SetupSlugAndView(viewModel.Slug, view.Id, view);

            // ACT
            var result = await _sut.Edit(viewModel.Slug, viewModel);

            // ASSERT
            _mockViewRepository.Verify(m => m.Get<CityView>(view.Id), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityName>()), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCitySlug>()), Times.Once);
            _mockCityService.Verify(m => m.When(It.IsAny<ChangeCityPosition>()), Times.Once);
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

        private static CityView CreateHalmstadCityView()
        {
            return new CityView
            {
                Id = Guid.Parse("E3E4D12C-2EDF-478E-92B6-5E408A69E961"),
                Name = "Halmstad",
                Slug = "halmstad",
                Latitude = 10.1,
                Longitude = 58.7
            };
        }

        private static CityView CreateKungsbackaCityView()
        {
            return new CityView
            {
                Id = Guid.Parse("3BCC0ADB-3A44-425F-926C-B2202A23D0C7"),
                Name = "Kungsbacka",
                Slug = "kungsbacka",
                Latitude = 12.8,
                Longitude = 67.2
            };
        }

        private void AddModelStateError()
        {
            _sut.ModelState.AddModelError("key", "error message");
        }

        private void SetupSlugAndView(string slug, Guid id, CityView cityView)
        {
            _mockSlugLookup.Setup(m => m.GetIdBySlug(slug)).Returns(id);
            _mockViewRepository.Setup(m => m.Get<CityView>(id)).Returns(cityView);
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

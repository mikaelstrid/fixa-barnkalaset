using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ArrangementsControllerTests : ControllerTestBase<ArrangementsController>
    {
        private readonly Mock<ILogger<ArrangementsController>> _mockLogger;
        private readonly Mock<IArrangementRepository> _mockArrangementsRepository;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly ArrangementsController _sut;

        public ArrangementsControllerTests()
        {
            _mockLogger = new Mock<ILogger<ArrangementsController>>();
            _mockArrangementsRepository = new Mock<IArrangementRepository>();
            _mockCityRepository = new Mock<ICityRepository>();
            _sut = new ArrangementsController(_mapper, _mockLogger.Object, _mockArrangementsRepository.Object, _mockCityRepository.Object);
        }

        [Fact]
        public async Task Index_GivenNullReturnFromRepository_ShouldReturnModelWithNoArrangements()
        {
            // ARRANGE
            _mockArrangementsRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<Arrangement>)null));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.GetAll(), Times.Once);
            var model = (result as ViewResult).Model as ArrangementsIndexViewModel;
            model.Should().NotBeNull();
            model.Arrangements.Should().NotBeNull();
            model.Arrangements.Count().Should().Be(0);
        }

        [Fact]
        public async Task Index_GivenNoArrangements_ShouldReturnModelWithNoArrangements()
        {
            // ARRANGE
            _mockArrangementsRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<Arrangement>)new List<Arrangement>()));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.GetAll(), Times.Once);
            var model = (result as ViewResult).Model as ArrangementsIndexViewModel;
            model.Should().NotBeNull();
            model.Arrangements.Should().NotBeNull();
            model.Arrangements.Count().Should().Be(0);
        }

        [Fact]
        public async Task Index_GivenTwoArrangementsInSameCityShouldReturnModelWithTwoArrangements_InAlphabeticalOrder()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var busfabriken = halmstad.Busfabriken();
            var laserdome = halmstad.Laserdome();
            var arrangements = new List<Arrangement> { laserdome, busfabriken };
            _mockArrangementsRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<Arrangement>)arrangements));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.GetAll(), Times.Once);
            var model = (result as ViewResult).Model as ArrangementsIndexViewModel;
            model.Arrangements.Count().Should().Be(2);
            //model.Arrangements.First().ShouldBeEquivalentTo(busfabriken, opt => opt.ExcludingMissingMembers());
            //model.Arrangements.Skip(1).First().ShouldBeEquivalentTo(laserdome, opt => opt.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Index_GivenTwoArrangementsInDifferentCities_ShouldReturnModelWithTwoArrangements_SortedByCityName()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var halmstadLaserdome = halmstad.Laserdome();
            var malmo = new City().Malmo();
            var malmoBusfabriken = malmo.Busfabriken();
            var arrangements = new List<Arrangement> { malmoBusfabriken, halmstadLaserdome };
            _mockArrangementsRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<Arrangement>)arrangements));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.GetAll(), Times.Once);
            var model = (result as ViewResult).Model as ArrangementsIndexViewModel;
            model.Arrangements.Count().Should().Be(2);
            //model.Arrangements.First().ShouldBeEquivalentTo(halmstadLaserdome, opt => opt.ExcludingMissingMembers());
            //model.Arrangements.Skip(1).First().ShouldBeEquivalentTo(malmoBusfabriken, opt => opt.ExcludingMissingMembers());
        }



        [Fact]
        public async Task Create_Get_ShouldReturnView_WithEmptyModelExceptForAvailableCities()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            _mockCityRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<City>) new[] {halmstad}));

            // ACT
            var result = await _sut.Create();

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            //(result as ViewResult).Model.ShouldBeEquivalentTo(new CreateOrEditArrangementViewModel
            //{
            //    Cities = new [] { new SelectListItem { Text = halmstad.Name, Value = halmstad.Slug } }
            //});
        }


        [Fact]
        public async Task Create_Post_GivenValidModel_ShouldCallRepository()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var arrangement = halmstad.Laserdome();
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(arrangement);
            Arrangement createdArrangement = null;
            _mockCityRepository.Setup(m => m.GetBySlug(halmstad.Slug)).Returns(Task.FromResult(halmstad));
            _mockArrangementsRepository.Setup(m => m.AddOrUpdate(It.IsAny<Arrangement>()))
                .Callback<Arrangement>(c => createdArrangement = c)
                .Returns(Task.CompletedTask);

            // ACT
            await _sut.Create(viewModel);

            // ASSERT
            //createdArrangement.ShouldBeEquivalentTo(arrangement, opt => opt.Excluding(a => a.City));
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Once);
        }

        [Fact]
        public async Task Create_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(halmstad.Laserdome());
            _mockCityRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<City>) new List<City> {halmstad}));
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.Create(viewModel);

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Never);
            _mockCityRepository.Verify(m => m.GetAll(), Times.Once);
            result.Should().BeOfType<ViewResult>();
            var responseModel = (result as ViewResult).Model as CreateOrEditArrangementViewModel;
            responseModel.Should().Be(viewModel);
            responseModel.Cities.Count().Should().Be(1);

        }

        [Fact]
        public async Task Create_Post_GivenUnknownCitySlug_ShouldAddModelStateError_AndReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockCityRepository.Setup(m => m.GetBySlug(halmstad.Slug)).Returns(Task.FromResult((City)null));

            // ACT
            var result = await _sut.Create(viewModel);

            // ASSERT
            _sut.ModelState.IsValid.Should().BeFalse();
            VerifyLogging(_mockLogger, LogLevel.Error);
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().Be(viewModel);
        }

        [Fact]
        public async Task Create_Post_GivenExistingSlug_ShouldAddModelStateError_AndReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockCityRepository.Setup(m => m.GetBySlug(halmstad.Slug)).Returns(Task.FromResult(halmstad));
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));

            // ACT
            var result = await _sut.Create(viewModel);

            // ASSERT
            _sut.ModelState.IsValid.Should().BeFalse();
            VerifyLogging(_mockLogger, LogLevel.Warning);
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().Be(viewModel);
        }



        [Fact]
        public async Task Edit_Get_GivenUnknownSlugs_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE
            _mockArrangementsRepository.Setup(m => m.GetBySlug(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult((Arrangement)null));

            // ACT
            var result = await _sut.Edit("unknown_city_slug", "unknown_slug");

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Get_GivenUnknownCitySlug_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE
            var laserdome = new City().Halmstad().Laserdome();
            _mockArrangementsRepository.Setup(m => m.GetBySlug(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult((Arrangement)null));
            _mockArrangementsRepository.Setup(m => m.GetBySlug(laserdome.City.Slug, laserdome.Slug)).Returns(Task.FromResult(laserdome));

            // ACT
            var result = await _sut.Edit("unknown_city_slug", laserdome.Slug);

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Get_GivenUnknownArrangementSlug_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE
            var laserdome = new City().Halmstad().Laserdome();
            _mockArrangementsRepository.Setup(m => m.GetBySlug(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult((Arrangement)null));
            _mockArrangementsRepository.Setup(m => m.GetBySlug(laserdome.City.Slug, laserdome.Slug)).Returns(Task.FromResult(laserdome));

            // ACT
            var result = await _sut.Edit(laserdome.City.Slug, "unknown_slug");

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Get_ShouldGetView_AndReturnItAsModel()
        {
            // ARRANGE
            var busfabriken = new City().Halmstad().Busfabriken();
            _mockArrangementsRepository.Setup(m => m.GetBySlug(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(busfabriken));

            // ACT
            var result = await _sut.Edit(busfabriken.City.Slug, busfabriken.Slug);

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.GetBySlug(busfabriken.City.Slug, busfabriken.Slug), Times.Once);
            result.Should().BeOfType<ViewResult>();
            //(result as ViewResult).Model.ShouldBeEquivalentTo(busfabriken, opt => opt.ExcludingMissingMembers());
        }


        [Fact]
        public async Task Edit_Post_GivenUnknownCitySlug_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));

            // ACT
            var result = await _sut.Edit("unknown_city_slug", viewModel.CitySlug, viewModel);

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenUnknownArrangementSlug_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));

            // ACT
            var result = await _sut.Edit(viewModel.CitySlug, "unknown_slug", viewModel);

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenUnknownSlugs_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));

            // ACT
            var result = await _sut.Edit("unknown_city_slug", "unknown_slug", viewModel);

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenInvalidModel_AndOnlyReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.Edit(halmstad.Slug, busfabriken.Slug, viewModel);

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().Be(viewModel);
        }

        [Fact]
        public async Task Edit_Post_GivenNoChanges_ShouldLogInformation_AndNotUpdateTheDatabase()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));

            // ACT
            var result = await _sut.Edit(halmstad.Slug, busfabriken.Slug, viewModel);

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Never);
            VerifyLogging(_mockLogger, LogLevel.Information);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedArrangementName_ShouldLogInformation_AndUpdateArrangementInDatabase()
        {
            // ARRANGE
            var halmstad = new City().Halmstad().With(c => c.Id = 1);
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));
            Arrangement arrangementSentToDatabase = null;
            _mockArrangementsRepository
                .Setup(m => m.AddOrUpdate(It.IsAny<Arrangement>()))
                .Callback< Arrangement>(a => arrangementSentToDatabase = a)
                .Returns(Task.CompletedTask);

            var changedName = "Busfabben";
            viewModel.Name = changedName;

            // ACT
            var result = await _sut.Edit(halmstad.Slug, busfabriken.Slug, viewModel);

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Information);
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.Is<Arrangement>(c => c.Name == changedName && c.CityId == halmstad.Id)), Times.Once);
            var expectedArrangement = busfabriken;
            expectedArrangement.Name = changedName;
            //arrangementSentToDatabase.ShouldBeEquivalentTo(expectedArrangement);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedArrangementSlug_ShouldLogInformation_AndUpdateArrangementsInDatabase()
        {
            // ARRANGE
            var halmstad = new City().Halmstad().With(c => c.Id = 1);
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));
            var changedSlug = "busfabben";
            viewModel.Slug = changedSlug;

            // ACT
            var result = await _sut.Edit(halmstad.Slug, busfabriken.Slug, viewModel);

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Information);
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.Is<Arrangement>(c => c.Slug == changedSlug && c.CityId == halmstad.Id)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedCity_ShouldLogInformation_AndUpdateArrangementInDatabase()
        {
            // ARRANGE
            var halmstad = new City().Halmstad().With(c => c.Id = 1);
            var malmo = new City().Malmo().With(c => c.Id = 2);
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));
            _mockCityRepository.Setup(m => m.GetBySlug(malmo.Slug)).Returns(Task.FromResult(malmo));
            var changedCitySlug = malmo.Slug;
            viewModel.CitySlug = changedCitySlug;

            // ACT
            var result = await _sut.Edit(halmstad.Slug, busfabriken.Slug, viewModel);

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Information);
            _mockCityRepository.Verify(m => m.GetBySlug(malmo.Slug), Times.Once);
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.Is<Arrangement>(c => c.CityId == malmo.Id)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenUnknownCitySlug_ShouldAddModelStateError_AndReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var halmstad = new City().Halmstad().With(c => c.Id = 1);
            var malmo = new City().Malmo().With(c => c.Id = 2);
            var busfabriken = new Arrangement().Busfabriken(halmstad);
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabriken);
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabriken.Slug)).Returns(Task.FromResult(busfabriken));
            _mockCityRepository.Setup(m => m.GetBySlug(malmo.Slug)).Returns(Task.FromResult(malmo));
            var changedCitySlug = "unknown_city_slug";
            viewModel.CitySlug = changedCitySlug;

            // ACT
            var result = await _sut.Edit(halmstad.Slug, busfabriken.Slug, viewModel);

            // ASSERT
            _sut.ModelState.IsValid.Should().BeFalse();
            VerifyLogging(_mockLogger, LogLevel.Warning);
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().Be(viewModel);
        }

        [Fact]
        public async Task Edit_Post_GivenCollidingSlugCombination_WhenChangingCitySlug_ShouldAddModelStateError_AndReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var halmstad = new City().Halmstad().With(c => c.Id = 1);
            var malmo = new City().Malmo().With(c => c.Id = 2);
            var busfabrikenHalmstad = halmstad.Busfabriken();
            var busfabrikenMalmo = malmo.Busfabriken();
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabrikenHalmstad);
            _mockCityRepository.Setup(m => m.GetBySlug(halmstad.Slug)).Returns(Task.FromResult(halmstad));
            _mockCityRepository.Setup(m => m.GetBySlug(malmo.Slug)).Returns(Task.FromResult(malmo));
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabrikenHalmstad.Slug)).Returns(Task.FromResult(busfabrikenHalmstad));
            _mockArrangementsRepository.Setup(m => m.GetBySlug(malmo.Slug, busfabrikenMalmo.Slug)).Returns(Task.FromResult(busfabrikenMalmo));
            var changedCitySlug = malmo.Slug;
            viewModel.CitySlug = changedCitySlug;

            // ACT
            var result = await _sut.Edit(halmstad.Slug, busfabrikenHalmstad.Slug, viewModel);

            // ASSERT
            _sut.ModelState.IsValid.Should().BeFalse();
            VerifyLogging(_mockLogger, LogLevel.Warning);
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().Be(viewModel);
        }

        [Fact]
        public async Task Edit_Post_GivenCollidingSlugCombination_WhenChangingArrangementSlug_ShouldAddModelStateError_AndReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var halmstad = new City().Halmstad().With(c => c.Id = 1);
            var busfabrikenHalmstad = halmstad.Busfabriken();
            var laserdomeHalmstad = halmstad.Laserdome();
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(busfabrikenHalmstad);
            _mockCityRepository.Setup(m => m.GetBySlug(halmstad.Slug)).Returns(Task.FromResult(halmstad));
            _mockCityRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<City>) new List<City>{halmstad}));
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, busfabrikenHalmstad.Slug)).Returns(Task.FromResult(busfabrikenHalmstad));
            _mockArrangementsRepository.Setup(m => m.GetBySlug(halmstad.Slug, laserdomeHalmstad.Slug)).Returns(Task.FromResult(laserdomeHalmstad));
            var changedSlug = laserdomeHalmstad.Slug;
            viewModel.Slug = changedSlug;

            // ACT
            var result = await _sut.Edit(halmstad.Slug, busfabrikenHalmstad.Slug, viewModel);

            // ASSERT
            _sut.ModelState.IsValid.Should().BeFalse();
            VerifyLogging(_mockLogger, LogLevel.Warning);
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Never);
            _mockCityRepository.Verify(m => m.GetAll(), Times.Once);
            result.Should().BeOfType<ViewResult>();
            var responseModel = (result as ViewResult).Model as CreateOrEditArrangementViewModel;
            responseModel.Should().Be(viewModel);
            responseModel.Cities.Count().Should().Be(1);
        }
    }
}

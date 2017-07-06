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
    public class ArrangementsControllerTests : ControllerTestBase
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<ArrangementsController>> _mockLogger;
        private readonly Mock<IArrangementRepository> _mockArrangementsRepository;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly ArrangementsController _sut;

        public ArrangementsControllerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
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
        public async Task Index_GivenNoArrangementsFromRepository_ShouldReturnModelWithNoArrangements()
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
        public async Task Index_GivenTwoCitiesFromRepository_ShouldReturnModelWithTwoCities()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var arrangements = new List<Arrangement> { halmstad.Busfabriken(), halmstad.Laserdome() };
            _mockArrangementsRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<Arrangement>)arrangements));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.GetAll(), Times.Once);
            var model = (result as ViewResult).Model as ArrangementsIndexViewModel;
            model.Arrangements.Count().Should().Be(2);
            model.Arrangements.ShouldBeEquivalentTo(arrangements, opt => opt.ExcludingMissingMembers());
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
            (result as ViewResult).Model.ShouldBeEquivalentTo(new CreateOrEditArrangementViewModel
            {
                Cities = new [] { new SelectListItem { Text = halmstad.Name, Value = halmstad.Slug } }
            });
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
            createdArrangement.ShouldBeEquivalentTo(arrangement, opt => opt.Excluding(a => a.City));
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Once);
        }

        [Fact]
        public async Task Create_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        {
            // ARRANGE
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(new City().Halmstad().Laserdome());
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.Create(viewModel);

            // ASSERT
            _mockArrangementsRepository.Verify(m => m.AddOrUpdate(It.IsAny<Arrangement>()), Times.Never);
            _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().Be(viewModel);
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
            VerifyLogging(LogLevel.Error);
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
            VerifyLogging(LogLevel.Warning);
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
            VerifyLogging(LogLevel.Warning);
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
            VerifyLogging(LogLevel.Warning);
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
            VerifyLogging(LogLevel.Warning);
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
            (result as ViewResult).Model.ShouldBeEquivalentTo(busfabriken, opt => opt.ExcludingMissingMembers());
        }

        //[Fact]
        //public async Task Edit_Post_GivenUnknownSlug_ShouldLogWarning_AndReturnNotFound()
        //{
        //    // ARRANGE

        //    // ACT
        //    var result = await _sut.Edit("unknown_slug", CreateCreateOrEditCityViewModel(new City().Halmstad()));

        //    // ASSERT
        //    VerifyLogging(LogLevel.Warning);
        //    result.Should().BeOfType<NotFoundResult>();
        //}

        //[Fact]
        //public async Task Edit_Post_GivenInvalidModel_ShouldLogWarning_AndOnlyReturnViewWithModelReceivedAsInput()
        //{
        //    // ARRANGE
        //    var city = new City().Halmstad();
        //    var viewModel = CreateCreateOrEditCityViewModel(city);
        //    _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));
        //    AddModelStateError();

        //    // ACT
        //    var result = await _sut.Edit(city.Slug, viewModel);

        //    // ASSERT
        //    _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
        //    result.Should().BeOfType<ViewResult>();
        //    (result as ViewResult).Model.Should().Be(viewModel);
        //    VerifyLogging(LogLevel.Warning);
        //}

        //[Fact]
        //public async Task Edit_Post_GivenNoChanges_ShouldLogInformation_AndNotUpdateTheDatabase()
        //{
        //    // ARRANGE
        //    var city = new City().Halmstad();
        //    var viewModel = CreateCreateOrEditCityViewModel(city);
        //    _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));

        //    // ACT
        //    var result = await _sut.Edit(city.Slug, viewModel);

        //    // ASSERT
        //    _mockCityRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
        //    VerifyLogging(LogLevel.Information);
        //    result.Should().BeOfType<RedirectToActionResult>();
        //}

        //[Fact]
        //public async Task Edit_Post_GivenChangedCityName_ShouldLogInformation_AndUpdateCityInDatabase()
        //{
        //    // ARRANGE
        //    var city = new City().Halmstad();
        //    var viewModel = CreateCreateOrEditCityViewModel(city);
        //    _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));
        //    var changedName = "Halmstad II";
        //    viewModel.Name = changedName;

        //    // ACT
        //    var result = await _sut.Edit(city.Slug, viewModel);

        //    // ASSERT
        //    VerifyLogging(LogLevel.Information);
        //    _mockCityRepository.Verify(m => m.AddOrUpdate(It.Is<City>(c => c.Name == changedName)), Times.Once);
        //    result.Should().BeOfType<RedirectToActionResult>();
        //}

        //[Fact]
        //public async Task Edit_Post_GivenChangedCitySlug_ShouldLogInformation_AndUpdateCityInDatabase()
        //{
        //    // ARRANGE
        //    var city = new City().Halmstad();
        //    var viewModel = CreateCreateOrEditCityViewModel(city);
        //    _mockCityRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));
        //    var changedSlug = "halmstad-ii";
        //    viewModel.Slug = changedSlug;

        //    // ACT
        //    var result = await _sut.Edit(city.Slug, viewModel);

        //    // ASSERT
        //    VerifyLogging(LogLevel.Information);
        //    _mockCityRepository.Verify(m => m.AddOrUpdate(It.Is<City>(c => c.Slug == changedSlug)), Times.Once);
        //    result.Should().BeOfType<RedirectToActionResult>();
        //}



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

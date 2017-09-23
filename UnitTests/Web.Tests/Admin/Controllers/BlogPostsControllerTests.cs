using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;
using UnitTests.Utilities.TestDataExtensions;
using Xunit;

namespace UnitTests.Web.Tests.Admin.Controllers
{
    public class BlogPostsControllerTests : ControllerTestBase<BlogPostsController>
    {
        private readonly Mock<IBlogPostRepository> _mockBlogPostRepository;
        private readonly BlogPostsController _sut;

        public BlogPostsControllerTests()
        {
            _mockBlogPostRepository = new Mock<IBlogPostRepository>();
            _sut = new BlogPostsController(_mapper, new Mock<ILogger<BlogPostsController>>().Object, _mockBlogPostRepository.Object);
        }

        [Fact]
        public async Task Index_GivenNullReturnFromRepository_ShouldReturnModelWithNoBlogPosts()
        {
            // ARRANGE
            _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>) null));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
            var model = GetViewModel(result);
            model.Should().NotBeNull();
            model.BlogPosts.Should().NotBeNull();
            model.BlogPosts.Count().Should().Be(0);
        }

        [Fact]
        public async Task Index_GivenNoBlogPosts_ShouldReturnModelWithNoBlogPosts()
        {
            // ARRANGE
            _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>) new List<BlogPost>()));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
            var model = GetViewModel(result);
            model.Should().NotBeNull();
            model.BlogPosts.Should().NotBeNull();
            model.BlogPosts.Count().Should().Be(0);
        }

        [Fact]
        public async Task Index_GivenFourMixedBlogPosts_ShouldReturnModelWithTheSameBlogPosts_InTheSameOrder()
        {
            // ARRANGE
            var blogpost0905 = new BlogPost().PubliceradFemteSeptember();
            var blogpost1017 = new BlogPost().PubliceradSjuttondeOktober();
            var blogpostUnpublished = new BlogPost().EjPubliceradIngetPubliceringsDatum();
            var blogpostFuturePublishedDate = new BlogPost().EjPubliceradPubliceringsDatumElfteNovember();
            var blogPosts = new List<BlogPost> { blogpost0905, blogpost1017, blogpostUnpublished, blogpostFuturePublishedDate };
            _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>) blogPosts));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
            var model = GetViewModel(result);
            model.BlogPosts.Count().Should().Be(4);
            model.BlogPosts.First().ShouldBeEquivalentTo(blogpost0905, opt => opt.ExcludingMissingMembers());
            model.BlogPosts.Skip(1).First().ShouldBeEquivalentTo(blogpost1017, opt => opt.ExcludingMissingMembers());
            model.BlogPosts.Skip(2).First().ShouldBeEquivalentTo(blogpostUnpublished, opt => opt.ExcludingMissingMembers());
            model.BlogPosts.Skip(3).First().ShouldBeEquivalentTo(blogpostFuturePublishedDate, opt => opt.ExcludingMissingMembers());
        }


        //[Fact]
        //public async Task Index_GivenTwoBlogPostsOneNotPublished_ShouldReturnModelWithOnlyThePublishedBlogPost()
        //{
        //    // ARRANGE
        //    var blogpost0905 = new BlogPost().PubliceradFemteSeptember();
        //    var blogpostUnpublished = new BlogPost().EjPubliceradIngetPubliceringsDatum();
        //    var blogPosts = new List<BlogPost> { blogpost0905, blogpostUnpublished };
        //    _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>)blogPosts));

        //    // ACT
        //    var result = await _sut.Index();

        //    // ASSERT
        //    _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
        //    var model = GetViewModel(result);
        //    model.BlogPosts.Count().Should().Be(1);
        //    model.BlogPosts.First().ShouldBeEquivalentTo(blogpost0905, opt => opt.ExcludingMissingMembers());
        //}

        //[Fact]
        //public async Task Index_GivenTwoBlogPostsOneUnpublishedWithFuturePublishedDate_ShouldReturnModelWithOnlyThePublishedBlogPost()
        //{
        //    // ARRANGE
        //    var blogpost0905 = new BlogPost().PubliceradFemteSeptember();
        //    var blogpostUnpublished = new BlogPost().EjPubliceradPubliceringsDatumElfteNovember();
        //    var blogPosts = new List<BlogPost> { blogpost0905, blogpostUnpublished };
        //    _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>)blogPosts));

        //    // ACT
        //    var result = await _sut.Index();

        //    // ASSERT
        //    _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
        //    var model = GetViewModel(result);
        //    model.BlogPosts.Count().Should().Be(1);
        //    model.BlogPosts.First().ShouldBeEquivalentTo(blogpost0905, opt => opt.ExcludingMissingMembers());
        //}

        //[Fact]
        //public async Task Index_GivenTwoBlogPostsOnePublishedWithFuturePublishedDate_ShouldReturnModelWithOnlyThePublishedBlogPost()
        //{
        //    // ARRANGE
        //    var blogpost0905 = new BlogPost().PubliceradFemteSeptember();
        //    var blogpostFuturePublishedDate = new BlogPost().EjPubliceradPubliceringsDatumElfteNovember();
        //    var blogPosts = new List<BlogPost> { blogpost0905, blogpostFuturePublishedDate };
        //    _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>)blogPosts));

        //    // ACT
        //    var result = await _sut.Index();

        //    // ASSERT
        //    _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
        //    var model = GetViewModel(result);
        //    model.BlogPosts.Count().Should().Be(1);
        //    model.BlogPosts.First().ShouldBeEquivalentTo(blogpost0905, opt => opt.ExcludingMissingMembers());
        //}

        //[Fact]
        //public void Create_Get_ShouldOnlyReturnView()
        //{
        //    // ARRANGE

        //    // ACT
        //    var result = _sut.Create();

        //    // ASSERT
        //    _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
        //    result.Should().BeOfType<ViewResult>();
        //    (result as ViewResult).Model.Should().BeNull();
        //}

        //[Fact]
        //public async Task Create_Post_GivenValidModel_ShouldCallRepository()
        //{
        //    // ARRANGE
        //    var city = new City().Halmstad();
        //    var model = CreateCreateOrEditCityViewModel(city);
        //    City createdCity = null;
        //    _mockBlogPostRepository.Setup(m => m.AddOrUpdate(It.IsAny<City>()))
        //        .Callback<City>(c => createdCity = c)
        //        .Returns(Task.CompletedTask);

        //    // ACT
        //    await _sut.Create(model);

        //    // ASSERT
        //    createdCity.ShouldBeEquivalentTo(city);
        //    _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Once);
        //}

        //[Fact]
        //public async Task Create_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        //{
        //    // ARRANGE
        //    var model = CreateCreateOrEditCityViewModel(new City().Halmstad());
        //    AddModelStateError(_sut);

        //    // ACT
        //    var result = await _sut.Create(model);

        //    // ASSERT
        //    _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
        //    result.Should().BeOfType<ViewResult>();
        //    (result as ViewResult).Model.Should().Be(model);
        //}

        //[Fact]
        //public async Task Create_Post_GivenExistingSlug_ShouldAddModelStateError_AndReturnViewWithModelReceivedAsInput()
        //{
        //    // ARRANGE
        //    var model = CreateCreateOrEditCityViewModel(new City().Halmstad());
        //    _mockBlogPostRepository.Setup(m => m.GetBySlug(model.Slug)).Returns(Task.FromResult(new City().Halmstad()));

        //    // ACT
        //    var result = await _sut.Create(model);

        //    // ASSERT
        //    _sut.ModelState.IsValid.Should().BeFalse();
        //    VerifyLogging(_mockLogger, LogLevel.Warning);
        //    _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
        //    result.Should().BeOfType<ViewResult>();
        //    (result as ViewResult).Model.Should().Be(model);
        //}



        //[Fact]
        //public async Task Edit_Get_GivenUnknownSlug_ShouldLogWarning_AndReturnNotFound()
        //{
        //    // ARRANGE
        //    _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult((City) null));

        //    // ACT
        //    var result = await _sut.Edit("unknown_slug");

        //    // ASSERT
        //    VerifyLogging(_mockLogger, LogLevel.Warning);
        //    result.Should().BeOfType<NotFoundResult>();
        //}

        //[Fact]
        //public async Task Edit_Get_ShouldGetView_AndReturnItAsModel()
        //{
        //    // ARRANGE
        //    var city = new City().Vaxjo();
        //    _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));

        //    // ACT
        //    var result = await _sut.Edit(city.Slug);

        //    // ASSERT
        //    _mockBlogPostRepository.Verify(m => m.GetBySlug(city.Slug), Times.Once);
        //    result.Should().BeOfType<ViewResult>();
        //    (result as ViewResult).Model.ShouldBeEquivalentTo(city, opt => opt.ExcludingMissingMembers());
        //}

        //[Fact]
        //public async Task Edit_Post_GivenUnknownSlug_ShouldLogWarning_AndReturnNotFound()
        //{
        //    // ARRANGE

        //    // ACT
        //    var result = await _sut.Edit("unknown_slug", CreateCreateOrEditCityViewModel(new City().Halmstad()));

        //    // ASSERT
        //    VerifyLogging(_mockLogger, LogLevel.Warning);
        //    result.Should().BeOfType<NotFoundResult>();
        //}

        //[Fact]
        //public async Task Edit_Post_GivenInvalidModel_ShouldLogWarning_AndOnlyReturnViewWithModelReceivedAsInput()
        //{
        //    // ARRANGE
        //    var city = new City().Halmstad();
        //    var viewModel = CreateCreateOrEditCityViewModel(city);
        //    _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));
        //    AddModelStateError(_sut);

        //    // ACT
        //    var result = await _sut.Edit(city.Slug, viewModel);

        //    // ASSERT
        //    _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
        //    result.Should().BeOfType<ViewResult>();
        //    (result as ViewResult).Model.Should().Be(viewModel);
        //    VerifyLogging(_mockLogger, LogLevel.Warning);
        //}

        //[Fact]
        //public async Task Edit_Post_GivenDuplicateSlug_ShouldAddModelStateError_AndReturnViewWithModelReceivedAsInput()
        //{
        //    // ARRANGE
        //    var cityUnderTest = new City().Halmstad();
        //    var otherExistingCity = new City().Vaxjo();
        //    _mockBlogPostRepository.Setup(m => m.GetBySlug(cityUnderTest.Slug)).Returns(Task.FromResult(cityUnderTest));
        //    _mockBlogPostRepository.Setup(m => m.GetBySlug(otherExistingCity.Slug)).Returns(Task.FromResult(otherExistingCity));

        //    // ACT
        //    var model = CreateCreateOrEditCityViewModel(cityUnderTest);
        //    var originalSlug = model.Slug;
        //    model.Slug = otherExistingCity.Slug;
        //    var result = await _sut.Edit(originalSlug, model);

        //    // ASSERT
        //    _sut.ModelState.IsValid.Should().BeFalse();
        //    VerifyLogging(_mockLogger, LogLevel.Warning);
        //    _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
        //    result.Should().BeOfType<ViewResult>();
        //    (result as ViewResult).Model.Should().Be(model);
        //}



        //[Fact]
        //public async Task Edit_Post_GivenNoChanges_ShouldLogInformation_AndNotUpdateTheDatabase()
        //{
        //    // ARRANGE
        //    var city = new City().Halmstad();
        //    var viewModel = CreateCreateOrEditCityViewModel(city);
        //    _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));

        //    // ACT
        //    var result = await _sut.Edit(city.Slug, viewModel);

        //    // ASSERT
        //    _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<City>()), Times.Never);
        //    VerifyLogging(_mockLogger, LogLevel.Information);
        //    result.Should().BeOfType<RedirectToActionResult>();
        //}

        //[Fact]
        //public async Task Edit_Post_GivenChangedCityName_ShouldLogInformation_AndUpdateCityInDatabase()
        //{
        //    // ARRANGE
        //    var city = new City().Halmstad();
        //    var viewModel = CreateCreateOrEditCityViewModel(city);
        //    _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(city));
        //    var changedName = "Halmstad II";
        //    viewModel.Name = changedName;

        //    // ACT
        //    var result = await _sut.Edit(city.Slug, viewModel);

        //    // ASSERT
        //    VerifyLogging(_mockLogger, LogLevel.Information);
        //    _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.Is<City>(c => c.Name == changedName)), Times.Once);
        //    result.Should().BeOfType<RedirectToActionResult>();
        //}

        //[Fact]
        //public async Task Edit_Post_GivenChangedCitySlug_ShouldLogInformation_AndUpdateCityInDatabase()
        //{
        //    // ARRANGE
        //    var city = new City().Halmstad();
        //    var viewModel = CreateCreateOrEditCityViewModel(city);
        //    var busfabriken = city.Busfabriken();
        //    _mockBlogPostRepository.Setup(m => m.GetBySlug(city.Slug)).Returns(Task.FromResult(city));
        //    var changedSlug = "halmstad-ii";
        //    viewModel.Slug = changedSlug;

        //    // ACT
        //    var result = await _sut.Edit(city.Slug, viewModel);

        //    // ASSERT
        //    VerifyLogging(_mockLogger, LogLevel.Information);
        //    _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.Is<City>(c => c.Slug == changedSlug)), Times.Once);
        //    result.Should().BeOfType<RedirectToActionResult>();
        //}


        private static BlogPostsIndexViewModel GetViewModel(IActionResult result)
        {
            return (result as ViewResult).Model as BlogPostsIndexViewModel;
        }


        //private static CreateOrEditCityViewModel CreateCreateOrEditCityViewModel(City city)
        //{
        //    return new CreateOrEditCityViewModel
        //    {
        //        Name = city.Name,
        //        Slug = city.Slug,
        //        Latitude = city.Latitude,
        //        Longitude = city.Longitude
        //    };
        //}
    }
}

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
        private readonly Mock<ILogger<BlogPostsController>> _mockLogger;
        private readonly Mock<IBlogPostRepository> _mockBlogPostRepository;
        private readonly BlogPostsController _sut;

        public BlogPostsControllerTests()
        {
            _mockBlogPostRepository = new Mock<IBlogPostRepository>();
            _mockLogger = new Mock<ILogger<BlogPostsController>>();
            _sut = new BlogPostsController(_mapper, _mockLogger.Object, _mockBlogPostRepository.Object);
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
            var model = GetViewModel<BlogPostsIndexViewModel>(result);
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
            var model = GetViewModel<BlogPostsIndexViewModel>(result);
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
            var model = GetViewModel<BlogPostsIndexViewModel>(result);
            model.BlogPosts.Count().Should().Be(4);
            model.BlogPosts.First().ShouldBeEquivalentTo(blogpost0905, opt => opt.ExcludingMissingMembers());
            model.BlogPosts.Skip(1).First().ShouldBeEquivalentTo(blogpost1017, opt => opt.ExcludingMissingMembers());
            model.BlogPosts.Skip(2).First().ShouldBeEquivalentTo(blogpostUnpublished, opt => opt.ExcludingMissingMembers());
            model.BlogPosts.Skip(3).First().ShouldBeEquivalentTo(blogpostFuturePublishedDate, opt => opt.ExcludingMissingMembers());
        }



        [Fact]
        public void Create_Get_ShouldOnlyReturnView()
        {
            // ARRANGE

            // ACT
            var result = _sut.Create();

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<BlogPost>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().BeNull();
        }

        [Fact]
        public async Task Create_Post_GivenValidModel_ShouldCallRepository()
        {
            // ARRANGE
            var blogPost = new BlogPost().PubliceradFemteSeptember();
            var model = CreateCreateOrEditBlogPostViewModel(blogPost);
            BlogPost createdBlogPost = null;
            _mockBlogPostRepository.Setup(m => m.AddOrUpdate(It.IsAny<BlogPost>()))
                .Callback<BlogPost>(p => createdBlogPost = p)
                .Returns(Task.CompletedTask);

            // ACT
            await _sut.Create(model);

            // ASSERT
            createdBlogPost.ShouldBeEquivalentTo(blogPost);
            _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<BlogPost>()), Times.Once);
        }

        [Fact]
        public async Task Create_Post_GivenInvalidModel_ShouldReturnViewWithModel()
        {
            // ARRANGE
            var model = CreateCreateOrEditBlogPostViewModel(new BlogPost().PubliceradFemteSeptember());
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.Create(model);

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<BlogPost>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            GetViewModel<CreateOrEditBlogPostViewModel>(result).Should().Be(model);
        }

        [Fact]
        public async Task Create_Post_GivenExistingSlug_ShouldAddModelStateError_AndReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var model = CreateCreateOrEditBlogPostViewModel(new BlogPost().PubliceradFemteSeptember());
            _mockBlogPostRepository.Setup(m => m.GetBySlug(model.Slug)).Returns(Task.FromResult(new BlogPost().PubliceradSjuttondeOktober()));

            // ACT
            var result = await _sut.Create(model);

            // ASSERT
            _sut.ModelState.IsValid.Should().BeFalse();
            VerifyLogging(_mockLogger, LogLevel.Warning);
            _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<BlogPost>()), Times.Never);
            result.Should().BeOfType<ViewResult>();
            GetViewModel<CreateOrEditBlogPostViewModel>(result).Should().Be(model);
        }



        [Fact]
        public async Task Edit_Get_GivenUnknownSlug_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE
            _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult((BlogPost)null));

            // ACT
            var result = await _sut.Edit("unknown_slug");

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Get_ShouldGetView_AndReturnItAsModel()
        {
            // ARRANGE
            var blogPost = new BlogPost().PubliceradSjuttondeOktober();
            _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(blogPost));

            // ACT
            var result = await _sut.Edit(blogPost.Slug);

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetBySlug(blogPost.Slug), Times.Once);
            GetViewModel<CreateOrEditBlogPostViewModel>(result).ShouldBeEquivalentTo(blogPost, opt => opt.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Edit_Post_GivenUnknownSlug_ShouldLogWarning_AndReturnNotFound()
        {
            // ARRANGE

            // ACT
            var result = await _sut.Edit("unknown_slug", CreateCreateOrEditBlogPostViewModel(new BlogPost().PubliceradSjuttondeOktober()));

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenInvalidModel_ShouldLogWarning_AndOnlyReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var blogPost = new BlogPost().PubliceradSjuttondeOktober();
            var viewModel = CreateCreateOrEditBlogPostViewModel(blogPost);
            _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(blogPost));
            AddModelStateError(_sut);

            // ACT
            var result = await _sut.Edit(blogPost.Slug, viewModel);

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<BlogPost>()), Times.Never);
            GetViewModel<CreateOrEditBlogPostViewModel>(result).Should().Be(viewModel);
            VerifyLogging(_mockLogger, LogLevel.Warning);
        }

        [Fact]
        public async Task Edit_Post_GivenDuplicateSlug_ShouldAddModelStateError_AndReturnViewWithModelReceivedAsInput()
        {
            // ARRANGE
            var blogPostUnderTest = new BlogPost().PubliceradSjuttondeOktober();
            var otherExistingBlogPost = new BlogPost().PubliceradFemteSeptember();
            _mockBlogPostRepository.Setup(m => m.GetBySlug(blogPostUnderTest.Slug)).Returns(Task.FromResult(blogPostUnderTest));
            _mockBlogPostRepository.Setup(m => m.GetBySlug(otherExistingBlogPost.Slug)).Returns(Task.FromResult(otherExistingBlogPost));

            // ACT
            var model = CreateCreateOrEditBlogPostViewModel(blogPostUnderTest);
            var originalSlug = model.Slug;
            model.Slug = otherExistingBlogPost.Slug;
            var result = await _sut.Edit(originalSlug, model);

            // ASSERT
            _sut.ModelState.IsValid.Should().BeFalse();
            VerifyLogging(_mockLogger, LogLevel.Warning);
            _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<BlogPost>()), Times.Never);
            GetViewModel<CreateOrEditBlogPostViewModel>(result).Should().Be(model);
        }



        [Fact]
        public async Task Edit_Post_GivenNoChanges_ShouldLogInformation_AndNotUpdateTheDatabase()
        {
            // ARRANGE
            var blogPost = new BlogPost().PubliceradSjuttondeOktober();
            var viewModel = CreateCreateOrEditBlogPostViewModel(blogPost);
            _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(blogPost));

            // ACT
            var result = await _sut.Edit(blogPost.Slug, viewModel);

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.IsAny<BlogPost>()), Times.Never);
            VerifyLogging(_mockLogger, LogLevel.Information);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task Edit_Post_GivenChangedBlogPostTitle_ShouldLogInformation_AndUpdateBlogPostInDatabase()
        {
            // ARRANGE
            var blogPost = new BlogPost().PubliceradSjuttondeOktober();
            var viewModel = CreateCreateOrEditBlogPostViewModel(blogPost);
            _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(blogPost));
            var changedTitle = "Ny titel";
            viewModel.Title = changedTitle;

            // ACT
            var result = await _sut.Edit(blogPost.Slug, viewModel);

            // ASSERT
            VerifyLogging(_mockLogger, LogLevel.Information);
            _mockBlogPostRepository.Verify(m => m.AddOrUpdate(It.Is<BlogPost>(p => p.Title == changedTitle)), Times.Once);
            result.Should().BeOfType<RedirectToActionResult>();
        }


        private static T GetViewModel<T>(IActionResult result) where T : class
        {
            result.Should().BeOfType<ViewResult>();
            return (result as ViewResult).Model as T;
        }
        
        private static CreateOrEditBlogPostViewModel CreateCreateOrEditBlogPostViewModel(BlogPost blogPost)
        {
            return new CreateOrEditBlogPostViewModel
            {
                Title = blogPost.Title,
                Slug = blogPost.Slug,
                Preamble = blogPost.Preamble,
                Body = blogPost.Body,
                IsPublished = blogPost.IsPublished,
                PublishedUtc = blogPost.PublishedUtc
            };
        }
    }
}

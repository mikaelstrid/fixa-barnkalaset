using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Controllers;
using Pixel.FixaBarnkalaset.Web.Models;
using UnitTests.Utilities.TestDataExtensions;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace UnitTests.Web.Tests.Controllers
{
    public class BlogPostsControllerTests : ControllerTestBase<BlogPostsController>
    {
        private readonly Mock<ILogger<BlogPostsController>> _mockLogger;
        private readonly Mock<IBlogPostRepository> _mockBlogPostRepository;
        private readonly BlogPostsController _sut;

        public BlogPostsControllerTests()
        {
            _mockLogger = new Mock<ILogger<BlogPostsController>>();
            _mockBlogPostRepository = new Mock<IBlogPostRepository>();
            _sut = new BlogPostsController(_mapper, _mockLogger.Object, _mockBlogPostRepository.Object);
        }

        [Fact]
        public async Task Index_GivenNullResponseFromRepository_ShouldReturnAnEmptyResponseModel()
        {
            // ARRANGE
            _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>) null));
            
            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
            GetViewModel<BlogPostsIndexViewModel>(result).BlogPosts.Should().BeEmpty();
        }

        [Fact]
        public async Task Index_GivenEmptyResponseFromRepository_ShouldReturnAnEmptyResponseModel()
        {
            // ARRANGE
            _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>) new List<BlogPost>()));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
            GetViewModel<BlogPostsIndexViewModel>(result).BlogPosts.Should().BeEmpty();
        }

        [Fact]
        public async Task Index_GivenTwoPublishedBlogPosts_ShouldReturnTwoBlogPosts_InDescendingChronologicalOrder()
        {
            // ARRANGE
            var blogPost1 = new BlogPost().PubliceradForraVeckan();
            var blogPost2 = new BlogPost().PubliceradIgar();
            _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>) new [] { blogPost1, blogPost2 }));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
            var viewModel = GetViewModel<BlogPostsIndexViewModel>(result);
            viewModel.BlogPosts.Count().Should().Be(2);
            viewModel.BlogPosts.First().ShouldBeEquivalentTo(_mapper.Map<BlogPost, BlogPostsIndexViewModel.BlogPostViewModel>(blogPost2));
            viewModel.BlogPosts.Skip(1).First().ShouldBeEquivalentTo(_mapper.Map<BlogPost, BlogPostsIndexViewModel.BlogPostViewModel>(blogPost1));
        }

        [Fact]
        public async Task Index_GivenOnePublishedAndOneNotPublisedBlogPost_ShouldReturnOnlyThePublishedBlogPost()
        {
            // ARRANGE
            var blogPost1 = new BlogPost().EjPubliceradIngetPubliceringsDatum();
            var blogPost2 = new BlogPost().PubliceradIgar();
            _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>)new[] { blogPost1, blogPost2 }));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
            var viewModel = GetViewModel<BlogPostsIndexViewModel>(result);
            viewModel.BlogPosts.Count().Should().Be(1);
            viewModel.BlogPosts.First().ShouldBeEquivalentTo(_mapper.Map<BlogPost, BlogPostsIndexViewModel.BlogPostViewModel>(blogPost2));
        }

        [Fact]
        public async Task Index_GivenOnePublishedAndOneNotPublisedBlogPostAndOneWIthFuturePublishedDate_ShouldReturnOnlyThePublishedBlogPostWithPastedPublisedDate()
        {
            // ARRANGE
            var blogPost1 = new BlogPost().EjPubliceradIngetPubliceringsDatum();
            var blogPost2 = new BlogPost().PubliceradIgar();
            var blogPost3 = new BlogPost().PubliceradImorgon();
            blogPost3.PublishedUtc = DateTime.Now.AddDays(1);
            _mockBlogPostRepository.Setup(m => m.GetAll()).Returns(Task.FromResult((IEnumerable<BlogPost>)new[] { blogPost1, blogPost2, blogPost3 }));

            // ACT
            var result = await _sut.Index();

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetAll(), Times.Once);
            var viewModel = GetViewModel<BlogPostsIndexViewModel>(result);
            viewModel.BlogPosts.Count().Should().Be(1);
            viewModel.BlogPosts.First().ShouldBeEquivalentTo(_mapper.Map<BlogPost, BlogPostsIndexViewModel.BlogPostViewModel>(blogPost2));
        }



        [Fact]
        public async Task Details_GivenUnknownSlug_ShouldLogAndReturnNotFound()
        {
            // ARRANGE
            _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult((BlogPost)null));

            // ACT
            var result = await _sut.Details("unknown__slug");

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetBySlug(It.IsAny<string>()), Times.Once);
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Details_GivenSlugOfPublishedBlogPost_ShouldReturnResponseModel()
        {
            // ARRANGE
            var blogPost = new BlogPost().PubliceradIgar();
            _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(blogPost));

            // ACT
            var result = await _sut.Details(blogPost.Slug);

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetBySlug(blogPost.Slug), Times.Once);
            var viewModel = GetViewModel<BlogPostDetailsViewModel>(result);
            viewModel.ShouldBeEquivalentTo(_mapper.Map<BlogPost, BlogPostDetailsViewModel>(blogPost));
        }

        [Fact]
        public async Task Details_GivenSlugOfUnpublishedBlogPost_ShouldLogAndReturnNotFound()
        {
            // ARRANGE
            var blogPost = new BlogPost().EjPubliceradIngetPubliceringsDatum();
            _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(blogPost));

            // ACT
            var result = await _sut.Details(blogPost.Slug);

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetBySlug(blogPost.Slug), Times.Once);
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Details_GivenSlugOfFuturePublishedBlogPost_ShouldLogAndReturnNotFound()
        {
            // ARRANGE
            var blogPost = new BlogPost().PubliceradImorgon();
            _mockBlogPostRepository.Setup(m => m.GetBySlug(It.IsAny<string>())).Returns(Task.FromResult(blogPost));

            // ACT
            var result = await _sut.Details(blogPost.Slug);

            // ASSERT
            _mockBlogPostRepository.Verify(m => m.GetBySlug(blogPost.Slug), Times.Once);
            VerifyLogging(_mockLogger, LogLevel.Warning);
            result.Should().BeOfType<NotFoundResult>();
        }

    }
}

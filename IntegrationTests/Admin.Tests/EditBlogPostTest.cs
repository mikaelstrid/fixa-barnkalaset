using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web;
using UnitTests.Utilities.TestDataExtensions;
using Xunit;

namespace IntegrationTests.Admin.Tests
{
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
    public class EditBlogPostTest : IntegrationTestBase
    {
        public EditBlogPostTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task EditBlogPost_GivenValidModel_ShouldUpdateBlogPostInDatabase()
        {
            // ARRANGE
            var blogPost = new BlogPost().PubliceradSjuttondeOktober();
            PopulateDatabaseWithBlogPosts(_fixture, blogPost);

            var url = $"/admin/bloggposter/{blogPost.Slug}/andra";
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, url);

            var newTitle = "Ny titel";
            var newSlug = "ny-slugg";
            var postRequestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Title", newTitle},
                {"Slug", newSlug},
                {"Preamble", blogPost.Preamble},
                {"Body", blogPost.Body},
                {"IsPublished", blogPost.IsPublished.ToString()},
                {"PublishedUtc", blogPost.PublishedUtc.ToString()}
            };
            var postRequest = CreatePostDataRequest(url, postRequestBody, context);

            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            _fixture.MyDataDbContext.BlogPosts
                .Single(p => p.Slug == blogPost.Slug)
                .ShouldBeEquivalentTo(new BlogPost
                {
                    Title = newTitle,
                    Slug = newSlug,
                    Preamble = blogPost.Preamble,
                    Body = blogPost.Body,
                    IsPublished = blogPost.IsPublished,
                    PublishedUtc = blogPost.PublishedUtc
                },
                opt => opt
                    .Excluding(c => c.Id)
                    .Excluding(c => c.LastUpdatedUtc));
        }
    }
}


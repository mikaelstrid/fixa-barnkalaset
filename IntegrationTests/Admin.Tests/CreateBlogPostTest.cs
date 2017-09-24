using System;
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
    public class CreateBlogPostTest : IntegrationTestBase
    {
        private const string Url = "/admin/bloggposter/skapa";

        public CreateBlogPostTest(TestFixture<Startup> fixture) : base(fixture) { }


        [Theory]
        [InlineData(null, "slug", "preamble", "<p>body</p>", "false", null)]
        [InlineData("   ", "slug", "preamble", "<p>body</p>", "false", null)]
        [InlineData("title", null, "preamble", "<p>body</p>", "false", null)]
        [InlineData("title", "   ", "preamble", "<p>body</p>", "false", null)]
        [InlineData("title", "slug", "preamble", "<p>body</p>", "false", "invalid date")]
        public async Task CreateBlogPost_WithInvalidModel_ShouldReturnView_AndNotRedirect(string title, string slug, string preamble, string body, string isPublished, string publishedUtc)
        {
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, Url);
            var requestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Title", title},
                {"Slug", slug},
                {"Preamble", preamble},
                {"Body", body},
                {"IsPublished", isPublished},
                {"PublishedUtc", publishedUtc}
            };
            var postRequest = CreatePostDataRequest(Url, requestBody, context);

            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().NotBe(HttpStatusCode.Redirect);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task CreateBlogPost_GivenValidModel_ShouldWriteBlogPostToDatabase()
        {
            // ARRANGE
            var blogPost = new BlogPost().PubliceradFemteSeptember();
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, Url);

            var requestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Title", blogPost.Title},
                {"Slug", blogPost.Slug},
                {"Preamble", blogPost.Preamble},
                {"Body", blogPost.Body},
                {"IsPublished", blogPost.IsPublished.ToString()},
                {"PublishedUtc", blogPost.PublishedUtc.ToString()}
            };
            var postRequest = CreatePostDataRequest(Url, requestBody, context);

            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            _fixture.MyDataDbContext.BlogPosts
                .Single(c => c.Slug == blogPost.Slug)
                .ShouldBeEquivalentTo(blogPost, opt => opt
                    .ExcludingMissingMembers()
                    .Excluding(c => c.Id)
                    .Excluding(c => c.LastUpdatedUtc));
        }
    }
}

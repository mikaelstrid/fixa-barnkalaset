using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web;
using UnitTests.Utilities.TestDataExtensions;
using Xunit;

namespace IntegrationTests.Public.Tests
{
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
    public class DetailsBlogPostsTest : IntegrationTestBase
    {
        public DetailsBlogPostsTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task Details_GivenUnknownSlug_ShouldReturn404()
        {
            // ARRANGE

            // ACT
            var response = await Client.GetAsync("/barnkalasbloggen/okand-slug");
            var responseString = await response.Content.ReadAsStringAsync();

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            responseString.Should().Contain("Hoppsan").And.Contain("finns inte");
        }


        [Fact]
        public async Task Details_GivenSlugOfPublishedBlogPost_ShouldReturn200()
        {
            // ARRANGE
            var blogPost = new BlogPost().PubliceradIgar();
            PopulateDatabaseWithBlogPosts(blogPost);

            // ACT
            var response = await Client.GetAsync($"/barnkalasbloggen/{blogPost.Slug}");

            // ASSERT
            response.EnsureSuccessStatusCode();
        }
    }
}

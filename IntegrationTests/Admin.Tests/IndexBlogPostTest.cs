using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.Helpers;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web;
using Xunit;
using UnitTests.Utilities.TestDataExtensions;

namespace IntegrationTests.Admin.Tests
{
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
    public class IndexBlogPostTest : IntegrationTestBase
    {
        public IndexBlogPostTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task IndexBlogPostTest_GivenTwoBlogPostsInDatabase_ShouldReturnTableWithTwoBlogPosts()
        {
            // ARRANGE
            var blogPost1 = new BlogPost().PubliceradFemteSeptember();
            var blogPost2 = new BlogPost().PubliceradSjuttondeOktober();
            PopulateDatabaseWithBlogPosts(_fixture, blogPost1, blogPost2);

            var identityContext = await GetIdentityContext(_adminCredentials.UserName, _adminCredentials.Password);
            var request = GetRequestHelper.CreateWithCookiesFromResponse("/admin/bloggposter", identityContext.IdentityResponse);

            // ACT
            var response = await _client.SendAsync(request);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            Regex.Matches(responseString, "data-test-slug").Count.Should().Be(2);
            Regex.IsMatch(responseString, $"data-test-slug=\"{blogPost1.Slug}\"").Should().BeTrue();
            Regex.IsMatch(responseString, $"data-test-slug=\"{blogPost2.Slug}\"").Should().BeTrue();
        }
    }
}

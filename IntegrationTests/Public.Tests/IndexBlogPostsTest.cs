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
    public class IndexBlogPostsTest : IntegrationTestBase
    {
        public IndexBlogPostsTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task Index_GivenTwoBlogPost_ShouldReturnResponseWithTwoBlogPosts()
        {
            // ARRANGE
            var blogPost1 = new BlogPost().PubliceradIgar();
            var blogPost2 = new BlogPost().PubliceradForraVeckan();
            PopulateDatabaseWithBlogPosts(blogPost1, blogPost2);

            // ACT
            var response = await Client.GetAsync("/barnkalasbloggen");

            // ASSERT
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Regex.IsMatch(responseString, "<h1.*>Barnkalasbloggen</h1>").Should().BeTrue();
            responseString.Should().Contain(blogPost1.Slug);
            responseString.Should().Contain(blogPost2.Slug);
        }
    }
}

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
    public class IndexCityTest : IntegrationTestBase
    {
        public IndexCityTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task IndexCityTest_GivenTwoCititesInDatabase_ShouldReturnTableWithTwoCities()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var malmo = new City().Malmo();
            PopulateDatabase(_fixture, halmstad, malmo);

            var identityContext = await GetIdentityContext(_adminCredentials.UserName, _adminCredentials.Password);
            var request = GetRequestHelper.CreateWithCookiesFromResponse("/admin/stader", identityContext.IdentityResponse);

            // ACT
            var response = await _client.SendAsync(request);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            Regex.Matches(responseString, "data-test-slug").Count.Should().Be(2);
            Regex.IsMatch(responseString, $"data-test-slug=\"{halmstad.Slug}\"").Should().BeTrue();
            Regex.IsMatch(responseString, $"data-test-slug=\"{malmo.Slug}\"").Should().BeTrue();
        }
    }
}

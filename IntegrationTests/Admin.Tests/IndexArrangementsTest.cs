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
    public class IndexArrangementsTest : IntegrationTestBase
    {
        public IndexArrangementsTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task IndexCityTest_GivenTwoArrangementsInDatabase_ShouldReturnTableWithTwoArrangements()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var malmo = new City().Malmo();
            PopulateDatabaseWithCities(_fixture, halmstad, malmo);
            var busfabriken = halmstad.Busfabriken();
            var laserdome = halmstad.Laserdome();
            PopulateDatabaseWithArrangements(_fixture, busfabriken, laserdome);

            var identityContext = await GetIdentityContext(_adminCredentials.UserName, _adminCredentials.Password);
            var request = GetRequestHelper.CreateWithCookiesFromResponse("/admin/arrangemang", identityContext.IdentityResponse);

            // ACT
            var response = await _client.SendAsync(request);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("<h1>Arrangemang</h1>");
            Regex.Matches(responseString, "data-test-slug").Count.Should().Be(2);
            Regex.IsMatch(responseString, $"data-test-slug=\"{busfabriken.Slug}\"").Should().BeTrue();
            Regex.IsMatch(responseString, $"data-test-slug=\"{laserdome.Slug}\"").Should().BeTrue();
        }
    }
}

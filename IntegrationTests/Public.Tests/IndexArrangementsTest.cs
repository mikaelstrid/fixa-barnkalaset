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
    public class IndexArrangementsTest : IntegrationTestBase
    {
        public IndexArrangementsTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task Index_GivenUnknownSlugs_ShouldReturn404()
        {
            // ARRANGE

            // ACT
            var response = await Client.GetAsync("/arrangemang/okand-stad");

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Index_GivenCityWithTwoArrangements_ShouldReturnResponseWithCityName_AndTwoArrangements()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            PopulateDatabaseWithCities(halmstad);
            var busfabriken = halmstad.Busfabriken();
            var laserdome = halmstad.Laserdome();
            PopulateDatabaseWithArrangements(busfabriken, laserdome);

            // ACT
            var response = await Client.GetAsync($"/arrangemang/{halmstad.Slug}");

            // ASSERT
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Regex.IsMatch(responseString, $"<h1.*>{halmstad.Name}</h1>").Should().BeTrue();
            responseString.Should().Contain(busfabriken.Name);
            responseString.Should().Contain(laserdome.Name);
        }
    }
}

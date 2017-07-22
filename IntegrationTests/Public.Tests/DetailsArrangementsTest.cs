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
    public class DetailsArrangementsTest : IntegrationTestBase
    {
        public DetailsArrangementsTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task Details_GivenUnknownSlugs_ShouldReturn404()
        {
            // ARRANGE

            // ACT
            var response = await Client.GetAsync("/arrangemang/okand-stad/okant-arrangemang");
            var responseString = await response.Content.ReadAsStringAsync();

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            responseString.Should().Contain("Hoppsan").And.Contain("finns inte");
        }


        [Fact]
        public async Task Details_GivenCityWithArrangement_ShouldReturnResponseWithCityName_AndArrangementInformation()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            PopulateDatabaseWithCities(halmstad);
            var busfabriken = halmstad.Busfabriken();
            PopulateDatabaseWithArrangements(busfabriken);

            // ACT
            var response = await Client.GetAsync($"/arrangemang/{halmstad.Slug}/{busfabriken.Slug}");

            // ASSERT
            //response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Regex.IsMatch(responseString, $"<h1.*>{busfabriken.Name}</h1>").Should().BeTrue();
            responseString.Should().Contain(busfabriken.Description);
        }
    }
}

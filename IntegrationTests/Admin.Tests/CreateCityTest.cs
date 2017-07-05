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
    public class CreateCityTest : IntegrationTestBase
    {
        private const string Url = "/admin/stader/skapa";

        public CreateCityTest(TestFixture<Startup> fixture) : base(fixture) { }


        [Theory]
        [InlineData(null, "halmstad", "10,0", "67,0")]
        [InlineData("", "halmstad", "10,0", "67,0")]
        [InlineData("   ", "halmstad", "10,0", "67,0")]
        [InlineData("Halmstad", null, "10,0", "67,0")]
        [InlineData("Halmstad", "", "10,0", "67,0")]
        [InlineData("Halmstad", "   ", "10,0", "67,0")]
        [InlineData("Halmstad", "a b", "10,0", "67,0")]
        [InlineData("Halmstad", "borås", "10,0", "67,0")]
        [InlineData("Halmstad", "test?", "10,0", "67,0")]
        [InlineData("Halmstad", "Boras", "10,0", "67,0")]
        [InlineData("Halmstad", "halmstad", null, "67,0")]
        [InlineData("Halmstad", "halmstad", "", "67,0")]
        [InlineData("Halmstad", "halmstad", "   ", "67,0")]
        [InlineData("Halmstad", "halmstad", "abv", "67,0")]
        [InlineData("Halmstad", "halmstad", "19.91", "67,0")]
        [InlineData("Halmstad", "halmstad", "-90.1", "67,0")]
        [InlineData("Halmstad", "halmstad", "90.1", "67,0")]
        [InlineData("Halmstad", "halmstad", "10,0", null)]
        [InlineData("Halmstad", "halmstad", "10,0", "")]
        [InlineData("Halmstad", "halmstad", "10,0", "   ")]
        [InlineData("Halmstad", "halmstad", "10,0", "abv")]
        [InlineData("Halmstad", "halmstad", "10,0", "19.91")]
        [InlineData("Halmstad", "halmstad", "10,0", "-180.1")]
        [InlineData("Halmstad", "halmstad", "10,0", "180.1")]
        public async Task CreateCity_WithInvalidModel_ShouldReturnView_AndNotRedirect(string name, string slug, string latitude, string longitude)
        {
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, Url);
            var requestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Name", name},
                {"Slug", slug},
                {"Latitude", latitude},
                {"Longitude", longitude}
            };
            var postRequest = CreatePostDataRequest(Url, requestBody, context);

            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().NotBe(HttpStatusCode.Redirect);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task CreateCity_GivenValidModel_ShouldWriteEventToDatabase()
        {
            // ARRANGE
            var city = new City().Halmstad();
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, Url);

            var requestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Name", city.Name},
                {"Slug", city.Slug},
                {"Latitude", city.Latitude.ToString(_swedishCultureInfo)},
                {"Longitude", city.Longitude.ToString(_swedishCultureInfo)}
            };
            var postRequest = CreatePostDataRequest(Url, requestBody, context);
            
            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            _fixture.MyDataDbContext.Cities
                .Single(c => c.Slug == city.Slug)
                .ShouldBeEquivalentTo(city, opt => opt.ExcludingMissingMembers().Excluding(c => c.Id));
        }
    }
}

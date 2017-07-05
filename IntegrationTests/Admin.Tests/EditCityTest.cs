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
    public class EditCityTest : IntegrationTestBase
    {
        public EditCityTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task EditCity_GivenValidModel_ShouldUpdateCityInDatabase()
        {
            // ARRANGE
            var city = new City().Malmo();
            PopulateDatabaseWithCities(_fixture, city);

            var url = $"/admin/stader/{city.Slug}/andra";
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, url);

            var newName = "Malmoe";
            var newSlug = "malmoe";
            var newLatitude = 17.8;
            var postRequestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Name", newName},
                {"Slug", newSlug},
                {"Latitude", newLatitude.ToString(_swedishCultureInfo)},
                {"Longitude", city.Longitude.ToString(_swedishCultureInfo)}
            };
            var postRequest = CreatePostDataRequest(url, postRequestBody, context);

            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            _fixture.MyDataDbContext.Cities
                .Single(c => c.Slug == city.Slug)
                .ShouldBeEquivalentTo(new City(newName, newSlug, newLatitude, city.Longitude) { Arrangements = new List<Arrangement>() },
                opt => opt.Excluding(c => c.Id));
        }
    }
}

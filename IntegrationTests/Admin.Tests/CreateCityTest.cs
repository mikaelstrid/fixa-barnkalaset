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
        public CreateCityTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task CreateCity_GivenValidModel_ShouldWriteEventToDatabase()
        {
            // ARRANGE
            var city = new City().Halmstad();
            var url = "/admin/stader/skapa";
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, url);

            var requestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Name", city.Name},
                {"Slug", city.Slug},
                {"Latitude", city.Latitude.ToString(_swedishCultureInfo)},
                {"Longitude", city.Longitude.ToString(_swedishCultureInfo)}
            };
            var postRequest = CreatePostDataRequest(url, requestBody, context);
            
            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            _fixture.MyDataDbContext.Cities.Single(c => c.Slug == city.Slug).ShouldBeEquivalentTo(city, opt => opt.ExcludingMissingMembers());
        }
    }
}

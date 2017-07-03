using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.Helpers;
using Pixel.FixaBarnkalaset.Web;
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
            var url = "/admin/stader/skapa";
            var identityToken = await LoginAndGetIdentityToken("test@test.com", "B1pdsosp!");

            var antiforgeryTokenResponse = await RequestAntiForgeryToken(identityToken, url);
            var antiForgeryToken = await AntiForgeryHelper.ExtractAntiForgeryToken(antiforgeryTokenResponse);

            var postRequestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", antiForgeryToken},
                {"Name", "Halmstad"},
                {"Slug", "halmstad"},
                {"Latitude", "19,5"},
                {"Longitude", "58,7"}
            };
            var postRequest = CreatePostDataRequest(url, postRequestBody, antiforgeryTokenResponse, identityToken);
            
            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            _fixture.MyEventSourcingDbContext.Events.Count().Should().Be(1);
        }
    }
}

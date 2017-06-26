using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.Web;
using Xunit;

namespace IntegrationTests
{
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
    public class AdminCitiesTests : IClassFixture<TestFixture<Startup>>
    {
        private const string IdentityCookieName = ".AspNetCore.Identity.Application";
        private readonly TestFixture<Startup> _fixture;
        private readonly HttpClient _client;

        public AdminCitiesTests(TestFixture<Startup> fixture)
        {
            _fixture = fixture;
            _client = fixture.Client;
        }

        [Fact]
        public async Task ReturnHelloWorld()
        {
            // ACT
            var response = await _client.GetAsync("/");

            // ASSERT
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("<option value=\"/arrangemang/halmstad\">Halmstad</option>");
        }

        [Fact]
        public async Task CreateCity_GivenValidModel_ShouldWriteEventToDatabase()
        {
            // ARRANGE
            var loginRequestMessage = PostRequestHelper.Create(
                "/konto/logga-in",
                new Dictionary<string, string>
            {
                {"Email", "test@test.com"},
                {"Password", "B1pdsosp!"}
            });
            var loginReponse = await _client.SendAsync(loginRequestMessage);
            var authenticationToken = CookiesHelper.ExtractCookiesFromResponse(loginReponse)[IdentityCookieName];

            var getRequest = GetRequestHelper.CreateWithCookiesFromResponse("/admin/stader/skapa", loginReponse);
            var getResponse = await _client.SendAsync(getRequest);
            getResponse.EnsureSuccessStatusCode();
            
            var antiForgeryToken = await AntiForgeryHelper.ExtractAntiForgeryToken(getResponse);

            var formPostBodyData = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", antiForgeryToken},
                {"Name", "Halmstad"},
                {"Slug", "halmstad"},
                {"Latitude", "19,5"},
                {"Longitude", "58,7"}
            };

            var postMessage = CookiesHelper.PutCookiesOnRequest(
                PostRequestHelper.CreateWithCookiesFromResponse("/admin/stader/skapa", formPostBodyData, getResponse),
                new Dictionary<string, string> { { IdentityCookieName, authenticationToken } });
            
            // ACT
            var response = await _client.SendAsync(postMessage);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            _fixture.EventSourcingDbContext.Events.Count().Should().Be(1);
        }
    }
}

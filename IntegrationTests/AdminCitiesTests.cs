using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
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
            var identityToken = await LoginAndGetIdentityToken("test@test.com", "B1pdsosp!");

            var getRequest = CookiesHelper.PutCookiesOnRequest(
                GetRequestHelper.Create("/admin/stader/skapa"),
                CreateCookiesDictionary(IdentityCookieName, identityToken));
            var getResponse = await _client.SendAsync(getRequest);
            getResponse.EnsureSuccessStatusCode();
            
            var antiForgeryToken = await AntiForgeryHelper.ExtractAntiForgeryToken(getResponse);

            var postRequestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", antiForgeryToken},
                {"Name", "Halmstad"},
                {"Slug", "halmstad"},
                {"Latitude", "19,5"},
                {"Longitude", "58,7"}
            };
            var postRequest = CookiesHelper.PutCookiesOnRequest(
                PostRequestHelper.CreateWithCookiesFromResponse("/admin/stader/skapa", postRequestBody, getResponse),
                CreateCookiesDictionary(IdentityCookieName, identityToken));
            
            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            _fixture.EventSourcingDbContext.Events.Count().Should().Be(1);
        }

        private async Task<string> LoginAndGetIdentityToken(string email, string password)
        {
            var loginRequestMessage = PostRequestHelper.Create(
                "/konto/logga-in",
                new Dictionary<string, string>
                {
                    {"Email", email},
                    {"Password", password}
                });
            var loginReponse = await _client.SendAsync(loginRequestMessage);
            var identityToken = CookiesHelper.ExtractCookiesFromResponse(loginReponse)[IdentityCookieName];
            return identityToken;
        }

        private static Dictionary<string, string> CreateCookiesDictionary(string cookieName, string cookieValue)
        {
            return new Dictionary<string, string> { { cookieName, cookieValue } };
        }
    }
}

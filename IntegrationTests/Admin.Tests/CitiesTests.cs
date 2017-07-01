using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.Helpers;
using Pixel.FixaBarnkalaset.Web;
using Xunit;

namespace IntegrationTests.Admin.Tests
{
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
    public class CitiesTests : IClassFixture<TestFixture<Startup>>
    {
        private const string IdentityCookieName = ".AspNetCore.Identity.Application";
        private readonly TestFixture<Startup> _fixture;
        private readonly HttpClient _client;

        public CitiesTests(TestFixture<Startup> fixture)
        {
            _fixture = fixture;
            _client = fixture.Client;
        }

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

        private async Task<HttpResponseMessage> RequestAntiForgeryToken(string identityToken, string url)
        {
            var getRequest = CookiesHelper.PutCookiesOnRequest(
                GetRequestHelper.Create(url),
                CreateCookiesDictionary(IdentityCookieName, identityToken));
            var getResponse = await _client.SendAsync(getRequest);
            getResponse.EnsureSuccessStatusCode();
            return getResponse;
        }

        private static HttpRequestMessage CreatePostDataRequest(string url, Dictionary<string, string> postRequestBody, HttpResponseMessage antiforgeryTokenResponse, string identityToken)
        {
            return CookiesHelper.PutCookiesOnRequest(
                PostRequestHelper.CreateWithCookiesFromResponse(url, postRequestBody, antiforgeryTokenResponse),
                CreateCookiesDictionary(IdentityCookieName, identityToken));
        }

        private static Dictionary<string, string> CreateCookiesDictionary(string cookieName, string cookieValue)
        {
            return new Dictionary<string, string> { { cookieName, cookieValue } };
        }
    }
}

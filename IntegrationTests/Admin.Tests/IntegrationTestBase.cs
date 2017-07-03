using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.Helpers;
using Pixel.FixaBarnkalaset.Web;
using Xunit;

namespace IntegrationTests.Admin.Tests
{
    public abstract class IntegrationTestBase : IClassFixture<TestFixture<Startup>>
    {
        protected const string IdentityCookieName = ".AspNetCore.Identity.Application";
        protected readonly TestFixture<Startup> _fixture;
        protected readonly HttpClient _client;

        protected IntegrationTestBase(TestFixture<Startup> fixture)
        {
            _fixture = fixture;
            _client = fixture.Client;
        }

        protected async Task<string> LoginAndGetIdentityToken(string email, string password)
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

        protected async Task<HttpResponseMessage> RequestAntiForgeryToken(string identityToken, string url)
        {
            var request = CookiesHelper.PutCookiesOnRequest(
                GetRequestHelper.Create(url),
                CreateCookiesDictionary(IdentityCookieName, identityToken));
            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            return response;
        }

        protected static HttpRequestMessage CreatePostDataRequest(string url, Dictionary<string, string> postRequestBody, HttpResponseMessage antiforgeryTokenResponse, string identityToken)
        {
            return CookiesHelper.PutCookiesOnRequest(
                PostRequestHelper.CreateWithCookiesFromResponse(url, postRequestBody, antiforgeryTokenResponse),
                CreateCookiesDictionary(IdentityCookieName, identityToken));
        }

        protected static Dictionary<string, string> CreateCookiesDictionary(string cookieName, string cookieValue)
        {
            return new Dictionary<string, string> { { cookieName, cookieValue } };
        }
    }
}

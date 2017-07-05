using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.Helpers;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web;
using Xunit;

namespace IntegrationTests.Admin.Tests
{
    public abstract class IntegrationTestBase : IClassFixture<TestFixture<Startup>>
    {
        protected readonly (string UserName, string Password) _adminCredentials = ("test@test.com", "B1pdsosp!");
        protected readonly CultureInfo _swedishCultureInfo = new CultureInfo("sv-SE");

        protected const string IdentityCookieName = ".AspNetCore.Identity.Application";
        protected readonly TestFixture<Startup> _fixture;
        protected readonly HttpClient _client;

        protected IntegrationTestBase(TestFixture<Startup> fixture)
        {
            _fixture = fixture;
            _client = fixture.Client;
        }


        protected async Task<IdentityContext> GetIdentityContext(string email, string password)
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
            return new IdentityContext
            {
                IdentityResponse = loginReponse,
                IdentityToken = identityToken
            };
        }

        public class IdentityContext
        {
            public HttpResponseMessage IdentityResponse { get; set; }
            public string IdentityToken { get; set; }
        }

        [Obsolete]
        protected async Task<string> LoginAndGetIdentityToken(string email, string password)
        {
            return (await GetIdentityContext(email, password)).IdentityToken;
        }
        

        protected async Task<IdentityAndAntiForgeryContext> GetIdentityAndAntiForgeryContext(string email, string password, string url)
        {
            var identityContext = await GetIdentityContext(email, password);
            var antiForgeryRequest = GetRequestHelper.CreateWithCookiesFromResponse(url, identityContext.IdentityResponse);
            var antiForgeryResponse = await _client.SendAsync(antiForgeryRequest);
            var antiForgeryToken = await AntiForgeryHelper.ExtractAntiForgeryToken(antiForgeryResponse);
            return new IdentityAndAntiForgeryContext(identityContext)
            {
                AntiForgeryResponse = antiForgeryResponse,
                AntiForgeryToken = antiForgeryToken
            };
        }

        protected class IdentityAndAntiForgeryContext : IdentityContext
        {
            public IdentityAndAntiForgeryContext(IdentityContext context)
            {
                IdentityResponse = context.IdentityResponse;
                IdentityToken = context.IdentityToken;
            }

            public HttpResponseMessage AntiForgeryResponse { get; set; }
            public string AntiForgeryToken { get; set; }
        }

        [Obsolete]
        protected async Task<HttpResponseMessage> RequestAntiForgeryToken(string identityToken, string url)
        {
            var request = CookiesHelper.PutCookiesOnRequest(
                GetRequestHelper.Create(url),
                CreateCookiesDictionary(IdentityCookieName, identityToken));
            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            return response;
        }

        [Obsolete]
        protected static HttpRequestMessage CreatePostDataRequest(string url, Dictionary<string, string> postRequestBody, HttpResponseMessage antiforgeryTokenResponse, string identityToken)
        {
            return CookiesHelper.PutCookiesOnRequest(
                PostRequestHelper.CreateWithCookiesFromResponse(url, postRequestBody, antiforgeryTokenResponse),
                CreateCookiesDictionary(IdentityCookieName, identityToken));
        }

        protected static HttpRequestMessage CreatePostDataRequest(string url, Dictionary<string, string> postRequestBody, IdentityAndAntiForgeryContext context)
        {
            return CookiesHelper.PutCookiesOnRequest(
                PostRequestHelper.CreateWithCookiesFromResponse(url, postRequestBody, context.AntiForgeryResponse),
                CreateCookiesDictionary(IdentityCookieName, context.IdentityToken));
        }

        protected static Dictionary<string, string> CreateCookiesDictionary(string cookieName, string cookieValue)
        {
            return new Dictionary<string, string> { { cookieName, cookieValue } };
        }

        protected static void PopulateDatabaseWithCities(TestFixture<Startup> fixture, params City[] cities)
        {
            fixture.MyDataDbContext.Cities.AddRange(cities);
            fixture.MyDataDbContext.SaveChanges();
        }

        protected static void PopulateDatabaseWithArrangements(TestFixture<Startup> fixture, params Arrangement[] arrangements)
        {
            fixture.MyDataDbContext.Arrangements.AddRange(arrangements);
            fixture.MyDataDbContext.SaveChanges();
        }
    }
}

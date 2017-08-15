using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Net.Http.Headers;

namespace IntegrationTests.Utilities.Helpers
{
    // http://www.stefanhendriks.com/2016/05/11/integration-testing-your-asp-net-core-app-dealing-with-anti-request-forgery-csrf-formdata-and-cookies/
    public class CookiesHelper
    {
        // Inspired from:
        // https://github.com/aspnet/Mvc/blob/538cd9c19121f8d3171cbfddd5d842cbb756df3e/test/Microsoft.AspNet.Mvc.FunctionalTests/TempDataTest.cs#L201-L202

        public static IDictionary<string, string> ExtractCookiesFromResponse(HttpResponseMessage response)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            if (response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                SetCookieHeaderValue.ParseList(values.ToList()).ToList().ForEach(cookie =>
                {
                    result.Add(cookie.Name.ToString(), cookie.Value.ToString());
                });
            }
            return result;
        }

        public static HttpRequestMessage PutCookiesOnRequest(HttpRequestMessage request, IDictionary<string, string> cookies)
        {
            cookies.Keys.ToList().ForEach(key =>
            {
                request.Headers.Add("Cookie", new CookieHeaderValue(key, cookies[key]).ToString());
            });

            return request;
        }

        public static HttpRequestMessage CopyCookiesFromResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            return PutCookiesOnRequest(request, ExtractCookiesFromResponse(response));
        }
    }
}
using System.Net.Http;

namespace IntegrationTests.Utilities.Helpers
{
    // http://www.stefanhendriks.com/2016/05/11/integration-testing-your-asp-net-core-app-dealing-with-anti-request-forgery-csrf-formdata-and-cookies/
    public class GetRequestHelper
    {
        public static HttpRequestMessage Create(string path)
        {
            return new HttpRequestMessage(HttpMethod.Get, path);
        }

        public static HttpRequestMessage CreateWithCookiesFromResponse(string path, HttpResponseMessage response)
        {
            var httpRequestMessage = Create(path);
            return CookiesHelper.CopyCookiesFromResponse(httpRequestMessage, response);
        }
    }
}
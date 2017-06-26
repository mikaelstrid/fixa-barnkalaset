using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntegrationTests.Admin.Tests.Utilities
{
    // http://www.stefanhendriks.com/2016/05/11/integration-testing-your-asp-net-core-app-dealing-with-anti-request-forgery-csrf-formdata-and-cookies/
    public class AntiForgeryHelper
    {
        public static string ExtractAntiForgeryToken(string htmlResponseText)
        {
            if (htmlResponseText == null) throw new ArgumentNullException(nameof(htmlResponseText));
            var match = Regex.Match(htmlResponseText, @"\<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)"" \/\>");
            return match.Success ? match.Groups[1].Captures[0].Value : null;
        }

        public static async Task<string> ExtractAntiForgeryToken(HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            return await Task.FromResult(ExtractAntiForgeryToken(responseAsString));
        }
    }
}
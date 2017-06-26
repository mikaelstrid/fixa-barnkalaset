using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Pixel.FixaBarnkalaset.Web;
using Xunit;

namespace IntegrationTests.Admin.Tests.Public
{
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
    public class HomeTests : IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient _client;

        public HomeTests(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task Index_ShouldContainHalmstad()
        {
            // ACT
            var response = await _client.GetAsync("/");

            // ASSERT
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("<option value=\"/arrangemang/halmstad\">Halmstad</option>");
        }
    }
}

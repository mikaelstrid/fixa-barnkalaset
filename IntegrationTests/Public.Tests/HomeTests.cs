using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web;
using Xunit;

namespace IntegrationTests.Public.Tests
{
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
    public class HomeTests : IClassFixture<TestFixture<Startup>>
    {
        private readonly TestFixture<Startup> _fixture;
        private readonly HttpClient _client;

        public HomeTests(TestFixture<Startup> fixture)
        {
            _fixture = fixture;
            _client = fixture.Client;
        }

        [Fact]
        public async Task Index_ShouldContainHalmstad()
        {
            // ARRANGE
            _fixture.MyDataDbContext.Cities.Add(new City { Name = "Halmstad", Slug = "halmstad", Latitude = 10.0, Longitude = 11.0 });
            _fixture.MyDataDbContext.SaveChanges();

            // ACT
            var response = await _client.GetAsync("/");

            // ASSERT
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("<option value=\"/arrangemang/halmstad\">Halmstad</option>");
        }
    }
}

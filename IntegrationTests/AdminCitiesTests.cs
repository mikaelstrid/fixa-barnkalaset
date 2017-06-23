using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class AdminCitiesTests : IClassFixture<TestFixture<TestStartup>>
    {
        private readonly HttpClient _client;

        public AdminCitiesTests(TestFixture<TestStartup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task ReturnHelloWorld()
        {
            // ACT
            var response = await _client.GetAsync("/");

            // ASSERT
            response.EnsureSuccessStatusCode();
            //var responseString = await response.Content.ReadAsStringAsync();
            //Assert.Equal("Hello World!", responseString);
        }

        [Fact]
        public async Task CreateCity_GivenValidModel_ShouldWriteEventToDatabase()
        {
            // ARRANGE
            var data = new Dictionary<string, string>
            {
                {"Name", "Halmstad"},
                {"Slug", "halmstad"},
                {"Latitude", "19.5"},
                {"Longitude", "58.7"}
            };
            var content = new FormUrlEncodedContent(data);

            // ACT
            var response = await _client.PostAsync("/admin/stader/skapa", content);

            // ASSERT
            response.EnsureSuccessStatusCode();
            //var responseString = await response.Content.ReadAsStringAsync();
            //Assert.Equal("Hello World!", responseString);
        }
    }
}

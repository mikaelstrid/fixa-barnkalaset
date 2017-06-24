using System.Net.Http;
using System.Threading.Tasks;
using Pixel.FixaBarnkalaset.Web;
using Xunit;

namespace IntegrationTests
{
    public class IntegrationTest1 : IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient _client;

        public IntegrationTest1(TestFixture<Startup> fixture)
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
            var responseString = await response.Content.ReadAsStringAsync();
            //Assert.Equal("Hello World!", responseString);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
using Pixel.FixaBarnkalaset.Web;
using Pixel.FixaBarnkalaset.ReadModel;
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
            _fixture.InMemoryViewRepository.Add(new CityListView(
                CityListView.ListViewId,
                new List<CityListView.City> { new CityListView.City(Guid.Parse("111D814A-A4C7-4432-9D1B-FFC55A8FCE71"), "Halmstad", "halmstad", 10.2, 78.1) }
            ));

            // ACT
            var response = await _client.GetAsync("/");

            // ASSERT
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("<option value=\"/arrangemang/halmstad\">Halmstad</option>");
        }
    }
}

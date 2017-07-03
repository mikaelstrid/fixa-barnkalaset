using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.Helpers;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Domain.Model;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.Web;
using Xunit;

namespace IntegrationTests.Admin.Tests
{
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
    public class EditCityTest : IntegrationTestBase
    {
        public EditCityTest(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task EditCity_GivenValidModel_ShouldWriteEventToDatabase_AndUpdateView()
        {
            // ARRANGE
            var id = Guid.Parse("1A822F60-A046-40CB-B6BB-4A1F57EB9F76");
            var oldName = "Kungsbacka";
            var oldSlug = "kungsbacka";
            var oldLatitude = 10.5;
            var oldLongitude = -19.1;
            PopulateDatabaseWithOneCity(_fixture, id, oldName, oldSlug, oldLatitude, oldLongitude);

            var url = "/admin/stader/kungsbacka/andra";
            var identityToken = await LoginAndGetIdentityToken("test@test.com", "B1pdsosp!");

            var antiforgeryTokenResponse = await RequestAntiForgeryToken(identityToken, url);
            var antiForgeryToken = await AntiForgeryHelper.ExtractAntiForgeryToken(antiforgeryTokenResponse);

            var newName = "Kungsbacka II";
            var newSlug = "kungsbacka-ii";
            var postRequestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", antiForgeryToken},
                {"Name", newName},
                {"Slug", newSlug},
                {"Latitude", "18,9"},
                {"Longitude", "-178,1"}
            };
            var postRequest = CreatePostDataRequest(url, postRequestBody, antiforgeryTokenResponse, identityToken);

            // ACT
            var response = await _client.SendAsync(postRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            _fixture.MyEventSourcingDbContext.Events.Count().Should().Be(4);
            _fixture.MyEventSourcingDbContext.Events.Should().ContainSingle(e => e.Metadata.Contains(nameof(CityNameChanged)));
            _fixture.MyEventSourcingDbContext.Events.Should().ContainSingle(e => e.Metadata.Contains(nameof(CitySlugChanged)));
            _fixture.MyEventSourcingDbContext.Events.Should().ContainSingle(e => e.Metadata.Contains(nameof(CityPositionChanged)));

            _fixture.InMemoryViewRepository.Get<CityView>(id).ShouldBeEquivalentTo(new CityView
            {
                Id = id,
                Name = newName,
                Slug = newSlug,
                Latitude = 18.9,
                Longitude = -178.1
            });
            _fixture.InMemoryViewRepository.Get<CityListView>(CityListView.ListViewId)
                .Cities.Should()
                .ContainSingle(c => c.Name == newName && c.Slug == newSlug);
            _fixture.InMemoryViewRepository.GetIdBySlug(oldSlug).Should().BeNull();
            _fixture.InMemoryViewRepository.GetIdBySlug(newSlug).Should().Be(id);
        }


        private static void PopulateDatabaseWithOneCity(TestFixture<Startup> fixture, Guid id, string name, string slug, double latitude, double longitude)
        {
            var @event = new CityCreated(id, name, slug, latitude, longitude);

            fixture.MyEventSourcingDbContext.Events.Add(@event.ToEventData(typeof(CityAggregate).Name, id, 1));
            fixture.MyEventSourcingDbContext.SaveChanges();
            fixture.InMemoryViewRepository.AddSlug(slug, id);
            fixture.InMemoryViewRepository.Add(new CityListView(CityListView.ListViewId, new List<CityListView.City> { new CityListView.City(id, name, slug, latitude, longitude) }));
            var cityView = new CityView
            {
                Id = id,
                Name = name,
                Slug = slug,
                Latitude = latitude,
                Longitude = longitude
            };
            fixture.InMemoryViewRepository.Add(cityView);
        }
    }
}

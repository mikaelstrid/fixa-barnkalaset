using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web;
using UnitTests.Utilities.TestDataExtensions;
using Xunit;

namespace IntegrationTests.Admin.Tests
{
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
    public class CreateArrangementTest : IntegrationTestBase
    {
        private static City _populatedHalmstad;
        private const string Url = "/admin/arrangemang/skapa";

        public CreateArrangementTest(TestFixture<Startup> fixture) : base(fixture)
        {
            if (fixture.IsInitialized) return;
            _populatedHalmstad = new City().Halmstad();
            PopulateDatabaseWithCities(fixture, _populatedHalmstad);
            fixture.IsInitialized = true;
        }
        
        [Theory]
        [InlineData(null, "laserdome", "", "", "", "", "", "", "", "", "test@test.com", "", "10,0", "67,0", "halmstad")]
        [InlineData("Laserdome", "  ", "", "", "", "", "", "", "", "", "test@test.com", "", "10,0", "67,0", "halmstad")]
        [InlineData("Laserdome", "laserdome", "", "", "", "", "", "", "", "", "test@test.com", "", "10,0", "", "halmstad")]
        [InlineData("Laserdome", "laserdome", "", "", "", "", "", "", "", "", "test.test.com", "", "10.0", "67,0", "halmstad")]
        public async Task CreateArrangement_WithInvalidModel_ShouldReturnView_AndNotRedirect(string name, string slug, string pitch, string description, string googlePlacesId, string coverImage, string streetAddress, string postalCode, string postalCity, string phoneNumber, string emailAddress, string website, string latitude, string longitude, string citySlug)
        {
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, Url);
            var requestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Name", name},
                {"Slug", slug},
                {"Pitch", pitch},
                {"Description", description},
                {"GooglePlacesId", googlePlacesId},
                {"CoverImage", coverImage},
                {"StreetAddress", streetAddress},
                {"PostalCode", postalCode},
                {"PostalCity", postalCity},
                {"PhoneNumber", phoneNumber},
                {"EmailAddress", emailAddress},
                {"Website", website},
                {"Latitude", latitude},
                {"Longitude", longitude},
                {"CitySlug", citySlug}
            };
            var postRequest = CreatePostDataRequest(Url, requestBody, context);

            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().NotBe(HttpStatusCode.Redirect);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Theory]
        [InlineData("Laserdome", "laserdome", "", "", "", "", "", "", "", "", "test@test.com", "", "10,0", "67,0", null)]
        [InlineData("Laserdome", "laserdome", "", "", "", "", "", "", "", "", "test@test.com", "", "10,0", "67,0", "")]
        [InlineData("Laserdome", "laserdome", "", "", "", "", "", "", "", "", "test@test.com", "", "10,0", "67,0", "boras")]
        public async Task CreateArrangement_WithInvalidCitySlug_ShouldReturnView_AndNotRedirect(string name, string slug, string pitch, string description, string googlePlacesId, string coverImage, string streetAddress, string postalCode, string postalCity, string phoneNumber, string emailAddress, string website, string latitude, string longitude, string citySlug)
        {
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, Url);
            var requestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Name", name},
                {"Slug", slug},
                {"Pitch", pitch},
                {"Description", description},
                {"GooglePlacesId", googlePlacesId},
                {"CoverImage", coverImage},
                {"StreetAddress", streetAddress},
                {"PostalCode", postalCode},
                {"PostalCity", postalCity},
                {"PhoneNumber", phoneNumber},
                {"EmailAddress", emailAddress},
                {"Website", website},
                {"Latitude", latitude},
                {"Longitude", longitude},
                {"CitySlug", citySlug}
            };
            var postRequest = CreatePostDataRequest(Url, requestBody, context);

            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().NotBe(HttpStatusCode.Redirect);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateArrangement_GivenOneCityInDatabase_ShouldReturnSelectWithOneCity()
        {
            // ARRANGE
            var identityContext = await GetIdentityContext(_adminCredentials.UserName, _adminCredentials.Password);
            var request = GetRequestHelper.CreateWithCookiesFromResponse("/admin/arrangemang/skapa", identityContext.IdentityResponse);

            // ACT
            var response = await _client.SendAsync(request);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("<h1>Lägg till nytt arrangemang</h1>");
            Regex.Matches(responseString, "<option").Count.Should().Be(1);
            responseString.Should().Contain("<option value=\"halmstad\">Halmstad</option>");
        }

        [Fact]
        public async Task CreateArrangement_GivenValidModel_ShouldWriteArrangementToDatabase()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            var arrangement = halmstad.Busfabriken();
            arrangement.EmailAddress = "test@test.com";
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, Url);

            var requestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Name", arrangement.Name},
                {"Slug", arrangement.Slug},
                {"Pitch", arrangement.Pitch},
                {"Description", arrangement.Description},
                {"GooglePlacesId", arrangement.GooglePlacesId},
                {"CoverImage", arrangement.CoverImage},
                {"StreetAddress", arrangement.StreetAddress},
                {"PostalCode", arrangement.PostalCode},
                {"PostalCity", arrangement.PostalCity},
                {"PhoneNumber", arrangement.PhoneNumber},
                {"EmailAddress", arrangement.EmailAddress},
                {"Website", arrangement.Website},
                {"Latitude", arrangement.Latitude.ToString(_swedishCultureInfo)},
                {"Longitude", arrangement.Longitude.ToString(_swedishCultureInfo)},
                {"CitySlug", halmstad.Slug}
            };
            var postRequest = CreatePostDataRequest(Url, requestBody, context);
            
            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            var expectedArrangement = arrangement;
            expectedArrangement.CityId = _populatedHalmstad.Id;
            _fixture.MyDataDbContext
                .Arrangements
                .Include(a => a.City)
                .Single(a => a.City.Slug == halmstad.Slug && a.Slug == arrangement.Slug)
                .ShouldBeEquivalentTo(expectedArrangement, opt => opt
                    .ExcludingMissingMembers()
                    .Excluding(a => a.Id)
                    .Excluding(a => a.City));
        }
    }
}

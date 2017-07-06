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
    public class EditArrangementTest : IntegrationTestBase
    {
        private readonly City _halmstad;
        private readonly City _malmo;
        private readonly Arrangement _busfabriken;
        private readonly Arrangement _laserdome;

        public EditArrangementTest(TestFixture<Startup> fixture) : base(fixture)
        {
            if (fixture.IsInitialized) return;

            _halmstad = new City().Halmstad();
            _malmo = new City().Malmo();
            PopulateDatabaseWithCities(_fixture, _halmstad, _malmo);

            _busfabriken = _halmstad.Busfabriken();
            _laserdome = _halmstad.Laserdome();
            PopulateDatabaseWithArrangements(_fixture, _busfabriken, _laserdome);

            fixture.IsInitialized = true;
        }

        [Fact]
        public async Task EditArrangement_GivenTwoCityInDatabase_ShouldReturnSelectWithTwoCities()
        {
            // ARRANGE
            var identityContext = await GetIdentityContext(_adminCredentials.UserName, _adminCredentials.Password);
            var request = GetRequestHelper.CreateWithCookiesFromResponse("/admin/arrangemang/skapa", identityContext.IdentityResponse);

            // ACT
            var response = await _client.SendAsync(request);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("<h1>Skapa arrangemang</h1>");
            Regex.Matches(responseString, "<option").Count.Should().Be(2);
            responseString.Should().Contain("<option value=\"halmstad\">Halmstad</option>");
            responseString.Should().Contain($"<option value=\"malmo\">Malm&#xF6;</option>");
        }

        [Fact]
        public async Task EditArrangement_GivenValidModel_ShouldUpdateArrangementInDatabase()
        {
            // ARRANGE
            var url = $"/admin/arrangemang/{_halmstad.Slug}/{_busfabriken.Slug}/andra";
            var context = await GetIdentityAndAntiForgeryContext(_adminCredentials.UserName, _adminCredentials.Password, url);

            var newName = "BusFabriken";
            var newSlug = "bus-fabriken";
            var newLatitude = 17.8;
            var postRequestBody = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", context.AntiForgeryToken},
                {"Name", newName},
                {"Slug", newSlug},
                {"Pitch", _busfabriken.Pitch},
                {"Description", _busfabriken.Description},
                {"GooglePlacesId", _busfabriken.GooglePlacesId},
                {"CoverImage", _busfabriken.CoverImage},
                {"StreetAddress", _busfabriken.StreetAddress},
                {"PostalCode", _busfabriken.PostalCode},
                {"PostalCity", _busfabriken.PostalCity},
                {"PhoneNumber", _busfabriken.PhoneNumber},
                {"EmailAddress", _busfabriken.EmailAddress},
                {"Website", _busfabriken.Website},
                {"Latitude", newLatitude.ToString(_swedishCultureInfo)},
                {"Longitude", _busfabriken.Longitude.ToString(_swedishCultureInfo)},
                {"CitySlug", _halmstad.Slug}
            };
            var postRequest = CreatePostDataRequest(url, postRequestBody, context);

            // ACT
            var response = await _client.SendAsync(postRequest);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            var updatedBusfabriken = _busfabriken;
            updatedBusfabriken.Name = newName;
            updatedBusfabriken.Slug = newSlug;
            updatedBusfabriken.Latitude = newLatitude;
            _fixture.MyDataDbContext
                .Arrangements
                .Include(a => a.City)
                .Single(a => a.City.Slug == _halmstad.Slug && a.Slug == newSlug)
                .ShouldBeEquivalentTo(updatedBusfabriken);
        }
    }
}

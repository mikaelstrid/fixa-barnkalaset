using System.Collections.Generic;
using Pixel.FixaBarnkalaset.Core;

namespace UnitTests.Utilities.TestDataExtensions
{
    public static class ArrangementTestDataExtensions
    {
        public static Arrangement Busfabriken(this Arrangement arrangement, City city = null)
        {
            var arrangementCity = city ?? new City().Halmstad();

            arrangement.Name = "Busfabriken";
            arrangement.Slug = "busfabriken";
            arrangement.Type = "Lekland";
            arrangement.City = arrangementCity;
            arrangement.CityId = arrangementCity.Id;
            arrangement.Pitch = "Busfabriken är ett inomhuslekland i djungelmiljö!";
            arrangement.Description = "<p>Här får alla barn hoppa, leka, busa, stoja, springa, pyssla, mysa och knasa sig så mycket de vill. Här finns klätterställningar, hängbroar, krypgångar, jättebollar, jättestor rutschkana, bollhav, studsmattor, bilbanor, hoppborgar, multisportarena, airbazoka samt babyhörna för de allra minsta. Vi finns i Halmstad, Linköping, Norrköping, Helsingborg och Malmö.Vi finns även i Norge och Danmark.</p><p>I Danmark har vi även öppnat ett megacenter.<p>";
            arrangement.GooglePlacesId = "ChIJlWz_GQ-jUUYR-Aw0nf1tDdY";
            arrangement.CoverImage = "https://lh5.googleusercontent.com/-t59RQKBlAzw/WOjo10hTpPI/AAAAAAAANcg/bvmnsULizAYACrGOQ3hepXQSm_eKfdeXgCLIB/w1000-k/";
            arrangement.StreetAddress = "Snöstorpsvägen 105";
            arrangement.PostalCode = "302 53";
            arrangement.PostalCity = "Halmstad";
            arrangement.PhoneNumber = "035-10 15 30";
            arrangement.EmailAddress = "";
            arrangement.Website = "http://www.busfabriken.se";
            arrangement.Latitude = 56.6727278;
            arrangement.Longitude = 12.8881324;

            arrangementCity.Arrangements = new List<Arrangement> {arrangement};

            return arrangement;
        }

        public static Arrangement Laserdome(this Arrangement arrangement, City city = null)
        {
            var arrangementCity = city ?? new City().Halmstad();

            arrangement.Name = "Laserdome";
            arrangement.Slug = "laserdome";
            arrangement.Type = "Laserdome";
            arrangement.City = arrangementCity;
            arrangement.CityId = arrangementCity.Id;
            arrangement.Pitch = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            arrangement.Description = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>";
            arrangement.GooglePlacesId = "ChIJHfODe0GjUUYRnJcieN_oZWw";
            arrangement.CoverImage = "https://lh3.googleusercontent.com/-7ToTCsf3HGw/V4aGccSg9PI/AAAAAAAETQ8/1ifxTijHpBko6KOqIDO8gvGeeDEm6TaYACJkC/w1000-k/";
            arrangement.StreetAddress = "Slottsmöllevägen";
            arrangement.PostalCode = "302 31";
            arrangement.PostalCity = "Halmstad";
            arrangement.PhoneNumber = "035-21 47 00";
            arrangement.EmailAddress = "";
            arrangement.Website = "http://www.aktivitetscenter.nu/";
            arrangement.Latitude = 56.689355;
            arrangement.Longitude = 12.870398;

            arrangementCity.Arrangements = new List<Arrangement> { arrangement };

            return arrangement;
        }
    }
}


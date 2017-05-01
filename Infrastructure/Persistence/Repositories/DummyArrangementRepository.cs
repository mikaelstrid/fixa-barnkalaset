using System;
using System.Collections.Generic;
using System.Linq;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Infrastructure.Persistence.Repositories
{
    public class DummyArrangementRepository : IArrangementRepository
    {
        private readonly List<Arrangement> _arrangements;

        public DummyArrangementRepository()
        {
            _arrangements = new List<Arrangement>
            {
                new Arrangement
                {
                    Id = 1,
                    Name = "Laserdome",
                    Slug = "laserdome",
                    Latitude = 56.689355,
                    Longitude = 12.8682093,
                    CitySlug = "halmstad",
                    CoverImage = "/images/laserdome-halmstad.jpg",
                    StreetAddress = "Slottsmöllan",
                    PostalCode = "302 31",
                    PostalCity = "Halmstad",
                    PhoneNumber = "035-21 47 00",
                    EmailAddress = "halmstad@aktivitetscenter.nu"
                },
                new Arrangement
                {
                    Id = 2,
                    Name = "Parkour",
                    Slug = "parkour",
                    Latitude = 57.6906169,
                    Longitude = 11.9152949,
                    CitySlug = "goteborg",
                    CoverImage = "/images/parkour-goteborg.jpg",
                    StreetAddress = "Backavägen 6",
                    PostalCode = "417 05",
                    PostalCity = "Göteborg",
                    PhoneNumber = "0723-95 28 58",
                    EmailAddress = "info@fearlessmovement.se",
                    Pitch = "Vill du fira din födelsedag med världens coolaste sport?",
                    Description = "<p>Då har du hamnat helt rätt! <br><br>Vi <strong>börjar med att värma upp</strong> leder och muskler, sedan går vi över till att <strong>lära oss grunderna</strong> inom parkour, till exempel enkla rörelser som <em>monkey</em>.<br><br>När vi väl bemästrat dessa rörelser kan vi börja med att ha kul på riktigt! Då bygger vi <strong>hinderbanor</strong> som gradvis blir svårare och roligare där vi kör <strong>parkourjage</strong> och parkourversionen av <strong>evighetsstafett</strong>!<br><br>Om ni vill kan ni <strong>ta med egen fika eller mat</strong> som ni kan mumsa på efter träningen. Sittplatser finns i hallen!<br><br>Vi befinner oss på <strong>Backavägen 6, 417 05 Göteborg</strong>. Leta efter <strong>Actionhallen</strong> så hittar ni vår lokal.<br><br>För <strong>bokning</strong>, besök <a style=\"text-decoration: underline;\" title=\"Boka nu!\" href=\"bokning.php\"><strong>bokningssidan</strong></a>.<br><br><strong>Betalning</strong> sker när bokningen har bekräftats. 1500 kr betalas då för att slutföra bokningen, om det är fler än 10 deltagare betalas resten efter aktiviteten. All betalning sker via Swish.<br><br>Vid <strong>avbokning</strong> gäller följande <a href=\"avbokning.php\" target=\"_blank\">regler</a>.<br><br>Vid <strong>frågor</strong>: besök <a style=\"text-decoration: underline;\" href=\"FAQ.php\" target=\"_blank\">frågor och svar</a>!<br><br>Du kan också maila oss på <em><strong>info@fearlessmovement.se</strong></em> eller ringa <strong>0723-95 28 58</strong>.<br><br></p>"
                },
                new Arrangement
                {
                    Id = 3,
                    Name = "Laserdome",
                    Slug = "laserdome",
                    Latitude = 57.6006462,
                    Longitude = 11.9772345,
                    CitySlug = "goteborg",
                    CoverImage = "/images/laserdome-goteborg.jpg",
                    StreetAddress = "Grafiska vägen 32",
                    PostalCode = "412 63",
                    PostalCity = "Göteborg",
                    PhoneNumber = "031-155105",
                    EmailAddress = "gbg@laserdome.se",
                    Pitch = "Världens bästa barnkalas!",
                    Description = "<p>Fira barnkalas på Laserdome Göteborg och få ett barnkalas utöver det vanliga. LASERDOME kan spelas av alla, tjej som kille och stor som liten. Laserdome har inget med styrka att göra – det är snabbhet, pricksäkerhet och samarbetsförmåga som är avgörande. Åldersgräns för barnkalas är 7-12 år.</p><p>Laserdome, världens mest spännande lagspel spelas i en dedikerad laserarena med närmare 1500 m2 spelyta! Laserdome är det ultimata äventyret perfekt för barnkalas: med sensorförsedda västar på kroppen och futuristiska laservapen i händerna jagar ni varandra genom mörkret. Göm er, smyg och spela smart. Hoppas att ni hittar fienderna innan de hittar er. Spelarenan är ett futuristiskt landskap fullt med ljud och ljuseffekter med massor av hinder och skiljeväggar perfekta för bakhåll…</p>"
                },
            };
        }

        public IEnumerable<Arrangement> GetAll()
        {
            return _arrangements;
        }

        public IEnumerable<Arrangement> GetByCitySlug(string citySlug)
        {
            return _arrangements
                .Where(a =>
                    a.CitySlug.Equals(citySlug, StringComparison.CurrentCultureIgnoreCase)
                );
        }

        public Arrangement GetBySlug(string citySlug, string arrangementSlug)
        {
            return _arrangements
                .SingleOrDefault(a =>
                    a.CitySlug.Equals(citySlug, StringComparison.CurrentCultureIgnoreCase)
                    && a.Slug.Equals(arrangementSlug, StringComparison.CurrentCultureIgnoreCase)
                );
        }

        public Arrangement GetById(int id)
        {
            return _arrangements
                .SingleOrDefault(a =>
                    a.Id == id
                );
        }

        public void AddOrUpdate(Arrangement arrangement)
        {
            var existigArrangement = GetById(arrangement.Id);
            if (existigArrangement != null)
            {
                _arrangements.Remove(existigArrangement);
            }
            else
            {
                arrangement.Id = GetNextId();
            }
            _arrangements.Add(arrangement);
        }

        public void Remove(int id)
        {
            _arrangements.RemoveAll(a => a.Id == id);
        }

        public int GetNextId() => _arrangements.Max(a => a.Id) + 1;
    }
}

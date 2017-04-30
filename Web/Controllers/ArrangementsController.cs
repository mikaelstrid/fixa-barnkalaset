using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Web.Controllers
{
    [Route("arrangemang")]
    public class Arrangements : Controller
    {
        private readonly ICityRepository _cityRepository;
        private readonly IArrangementRepository _arrangementRepository;

        public Arrangements(
            ICityRepository cityRepository,
            IArrangementRepository arrangementRepository)
        {
            _cityRepository = cityRepository;
            _arrangementRepository = arrangementRepository;
        }

        [Route("{citySlug}")]
        public IActionResult Index(string citySlug)
        {
            var city = _cityRepository.GetBySlug(citySlug);
            if (city == null) return NotFound();

            var arrangements = _arrangementRepository.GetByCitySlug(citySlug);
            return View(new ArrangementIndexViewModel
            {
                CityName = city.Name,
                CitySlug = citySlug,
                Arrangements = arrangements
            });
        }

        [Route("{citySlug}/{arrangementSlug}")]
        public IActionResult Details(string citySlug, string arrangementSlug)
        {
            var arrangement = _arrangementRepository.GetBySlug(citySlug, arrangementSlug);
            if (arrangement == null) return NotFound();
            return View(arrangement);
        }
    }

    public class ArrangementIndexViewModel
    {
        public string CityName { get; set; }
        public string CitySlug { get; set; }
        public IEnumerable<Arrangement> Arrangements { get; set; }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Web.Controllers
{
    [Route("")]
    public class Arrangements : Controller
    {
        private readonly IArrangementRepository _arrangementRepository;

        public Arrangements(IArrangementRepository arrangementRepository)
        {
            _arrangementRepository = arrangementRepository;
        }

        [Route("{citySlug}")]
        public IActionResult Index(string citySlug)
        {
            var cityName = _arrangementRepository.GetCityName(citySlug);
            if (cityName == null) return NotFound();

            var arrangements = _arrangementRepository.GetByCity(cityName);
            return View(new ArrangementIndexViewModel
            {
                CityName = cityName,
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
        public IEnumerable<Arrangement> Arrangements { get; set; }
    }
}

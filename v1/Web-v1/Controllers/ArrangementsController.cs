using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Models;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    [Route("barnkalas")]
    public class ArrangementsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ArrangementsController> _logger;
        private readonly ICityRepository _cityRepository;
        private readonly IArrangementRepository _arrangementRepository;

        public ArrangementsController(IMapper mapper, ILogger<ArrangementsController> logger, ICityRepository cityRepository, IArrangementRepository arrangementRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _cityRepository = cityRepository;
            _arrangementRepository = arrangementRepository;
        }

        [Route("{citySlug}")]
        public async Task<IActionResult> Index(string citySlug)
        {
            var city = await _cityRepository.GetBySlug(citySlug);
            if (city == null)
            {
                _logger.LogWarning("Index: No city with slug {Slug} found", citySlug);
                return NotFound();
            }

            ViewData["Title"] = $"Barnkalas i {city.Name} | Fixa barnkalaset";
            ViewData["Description"] = $"Här hittar du bra idéer för barnkalas i {city.Name}.";

            ViewData["OgTitle"] = $"Barnkalas i {city.Name}";
            ViewData["OgDescription"] = $"Här hittar du bra idéer för barnkalas i {city.Name}.";
            ViewData["OgImage"] = Request?.Scheme + "://" + Request?.Host + "/images/balloons-1869790_1200_630.jpg";

            var arrangements = city.Arrangements ?? new List<Arrangement>();
            return View(new ArrangementsIndexViewModel
            {
                CityName = city.Name,
                CitySlug = citySlug,
                Arrangements = _mapper.Map<IEnumerable<Arrangement>, IEnumerable<ArrangementsIndexViewModel.ArrangementViewModel>>(arrangements.OrderBy(a => a.Name))
            });
        }

        [Route("{citySlug}/{arrangementSlug}")]
        public async Task<IActionResult> Details(string citySlug, string arrangementSlug)
        {
            var city = await _cityRepository.GetBySlug(citySlug);
            if (city == null)
            {
                _logger.LogWarning("Details: No city with slug {CitySlug} found", citySlug);
                return NotFound();
            }

            var arrangement = await _arrangementRepository.GetBySlug(citySlug, arrangementSlug);
            if (arrangement == null)
            {
                _logger.LogWarning("Details: No arrangement with slug {ArrangementSlug} found", arrangementSlug);
                return NotFound();
            }

            ViewData["Title"] = $"Barnkalas på {arrangement.Name}, {city.Name} | Fixa barnkalaset";
            ViewData["Description"] = $"{arrangement.Pitch}";

            ViewData["OgTitle"] = $"Barnkalas på {arrangement.Name}, {city.Name}";
            ViewData["OgDescription"] = $"{arrangement.Pitch}";

            var viewModel = _mapper.Map<Arrangement, ArrangementDetailsViewModel>(arrangement);
            viewModel.CityName = city.Name;
            return View(viewModel);
        }
    }
}

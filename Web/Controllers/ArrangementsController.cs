using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Models;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    [Route("arrangemang")]
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

            return View(new ArrangementsIndexViewModel
            {
                CityName = city.Name,
                CitySlug = citySlug,
                Arrangements = _mapper.Map<IEnumerable<Arrangement>, IEnumerable<ArrangementsIndexViewModel.ArrangementViewModel>>(city.Arrangements)
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

            var viewModel = _mapper.Map<Arrangement, ArrangementDetailsViewModel>(arrangement);
            viewModel.CityName = city.Name;
            return View(viewModel);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Models;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    [Route("arrangemang")]
    public class Arrangements : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        private readonly IArrangementRepository _arrangementRepository;

        public Arrangements(
            IMapper mapper,
            ICityRepository cityRepository,
            IArrangementRepository arrangementRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _arrangementRepository = arrangementRepository;
        }

        [Route("{citySlug}")]
        public async Task<IActionResult> Index(string citySlug)
        {
            var city = await _cityRepository.GetBySlug(citySlug);
            if (city == null) return NotFound();

            return View(new ArrangementIndexViewModel
            {
                CityName = city.Name,
                CitySlug = citySlug,
                Arrangements =
                    _mapper.Map<IEnumerable<Arrangement>, IEnumerable<ArrangementIndexViewModel.ArrangementViewModel>>(
                        city.Arrangements)
            });
        }

        [Route("{citySlug}/{arrangementSlug}")]
        public async Task<IActionResult> Details(string citySlug, string arrangementSlug)
        {
            var city = await _cityRepository.GetBySlug(citySlug);
            if (city == null) return NotFound();

            var arrangement = _arrangementRepository.GetBySlug(citySlug, arrangementSlug);
            if (arrangement == null) return NotFound();

            var viewModel = _mapper.Map<Arrangement, ArrangementDetailsViewModel>(arrangement);
            viewModel.CityName = city.Name;
            return View(viewModel);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;
using Pixel.Kidsparties.Web.Models;
using System.Collections.Generic;

namespace Pixel.Kidsparties.Web.Controllers
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
        public IActionResult Index(string citySlug)
        {
            var city = _cityRepository.GetBySlug(citySlug);
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
        public IActionResult Details(string citySlug, string arrangementSlug)
        {
            var city = _cityRepository.GetBySlug(citySlug);
            if (city == null) return NotFound();

            var arrangement = _arrangementRepository.GetBySlug(citySlug, arrangementSlug);
            if (arrangement == null) return NotFound();

            var viewModel = _mapper.Map<Arrangement, ArrangementDetailsViewModel>(arrangement);
            viewModel.CityName = city.Name;
            return View(viewModel);
        }
    }
}

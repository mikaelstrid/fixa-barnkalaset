using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/arrangemang")]
    [Authorize(Roles = "Admin")]
    public class ArrangementsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ArrangementsController> _logger;
        private readonly IArrangementRepository _arrangementRepository;
        private readonly ICityRepository _cityRepository;

        public ArrangementsController(
            IMapper mapper,
            ILogger<ArrangementsController> logger,
            IArrangementRepository arrangementRepository, 
            ICityRepository cityRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _arrangementRepository = arrangementRepository;
            _cityRepository = cityRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index: Called");
            var arrangements = await _arrangementRepository.GetAll();
            var model = new ArrangementsIndexViewModel
            {
                Arrangements = _mapper.Map<IEnumerable<Arrangement>, IEnumerable<ArrangementsIndexViewModel.ArrangementViewModel>>(arrangements)
            };
            return View(model);
        }

        [Route("skapa")]
        public async Task<IActionResult> Create(int id)
        {
            return View(new CreateOrEditArrangementViewModel
            {
                Cities = (await _cityRepository.GetAll()).Select(c => new SelectListItem { Value = c.Slug, Text = c.Name })
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("skapa")]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,CitySlug,Pitch,Description,GooglePlacesId,CoverImage,StreetAddress,PostalCode,PostalCity,PhoneNumber,EmailAddress,Website,Latitude,Longitude")] CreateOrEditArrangementViewModel arrangement)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<CreateOrEditArrangementViewModel, Arrangement>(arrangement);
                _arrangementRepository.AddOrUpdate(model);
                return RedirectToAction("Index");
            }

            arrangement.Cities = (await _cityRepository.GetAll()).Select(c => new SelectListItem { Value = c.Slug, Text = c.Name });
            return View(arrangement);
        }

        [Route("{id}/andra")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(_arrangementRepository.GetById(id));
            model.Cities = (await _cityRepository.GetAll()).Select(c => new SelectListItem { Value = c.Slug, Text = c.Name });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/andra")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,CitySlug,Pitch,Description,GooglePlacesId,CoverImage,StreetAddress,PostalCode,PostalCity,PhoneNumber,EmailAddress,Website,Latitude,Longitude")] CreateOrEditArrangementViewModel arrangement)
        {
            if (id != arrangement.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var model = _mapper.Map<CreateOrEditArrangementViewModel, Arrangement>(arrangement);
                _arrangementRepository.AddOrUpdate(model);
                return RedirectToAction("Index");
            }

            arrangement.Cities = (await _cityRepository.GetAll()).Select(c => new SelectListItem { Value = c.Slug, Text = c.Name });
            return View(arrangement);
        }
    }
}

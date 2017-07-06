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
        public async Task<IActionResult> Create()
        {
            return View(new CreateOrEditArrangementViewModel {Cities = await GetCitySelectListItems()});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("skapa")]
        public async Task<IActionResult> Create([Bind("Name,Slug,CitySlug,Pitch,Description,GooglePlacesId,CoverImage,StreetAddress,PostalCode,PostalCity,PhoneNumber,EmailAddress,Website,Latitude,Longitude")] CreateOrEditArrangementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Cities = await GetCitySelectListItems();
                return View(model);
            }

            var city = await _cityRepository.GetBySlug(model.CitySlug);
            if (city == null)
            {
                _logger.LogError("Edit GET: No city with slug {CitySlug} found when creating arrangement", model.CitySlug);
                ModelState.AddModelError("CitySlug", "The city slug does not exist");
                return View(model);
            }

            if (await _arrangementRepository.GetBySlug(model.CitySlug, model.Slug) != null)
            {
                _logger.LogWarning("Create POST: There already exist a arrangement with {Slug} and city with slug {CitySlug}", model.Slug, model.CitySlug);
                ModelState.AddModelError("Slug", "The slug already exists");
                ModelState.AddModelError("CitySlug", "The slug already exists");
                return View(model);
            }

            var arrangement = _mapper.Map<CreateOrEditArrangementViewModel, Arrangement>(model);
            arrangement.CityId = city.Id; 
            await _arrangementRepository.AddOrUpdate(arrangement);
            return RedirectToAction("Index");
        }

        [Route("{urlCitySlug}/{urlSlug}/andra")]
        public async Task<IActionResult> Edit(string urlCitySlug, string urlSlug)
        {
            var existingArrangement = await _arrangementRepository.GetBySlug(urlCitySlug, urlSlug);
            if (existingArrangement == null)
            {
                _logger.LogWarning("Edit GET: No arrangement with slug {Slug} and city slug {CitySlug} found when getting city", urlSlug, urlCitySlug);
                return NotFound();
            }
            
            var viewModel = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(existingArrangement);
            viewModel.Cities = await GetCitySelectListItems();
            return View(viewModel);
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
                await _arrangementRepository.AddOrUpdate(model);
                return RedirectToAction("Index");
            }

            arrangement.Cities = (await _cityRepository.GetAll()).Select(c => new SelectListItem { Value = c.Slug, Text = c.Name });
            return View(arrangement);
        }




        private async Task<IEnumerable<SelectListItem>> GetCitySelectListItems()
        {
            return (await _cityRepository.GetAll()).Select(c => new SelectListItem {Text = c.Name, Value = c.Slug});
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Domain.Commands;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/stader")]
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICityRepository _cityRepository;
        private readonly ICityService _cityService;
        private readonly ISlugLookup _slugLookup;
        private readonly IViewRepository _viewRepository;

        public CitiesController(
            IMapper mapper,
            ILogger<CitiesController> logger,
            ICityRepository cityRepository,
            ICityService cityService,
            ISlugLookup slugLookup,
            IViewRepository viewRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _cityRepository = cityRepository;
            _cityService = cityService;
            _slugLookup = slugLookup;
            _viewRepository = viewRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index: Called");
            var cities = await _cityRepository.GetAll();
            var model = new CitiesIndexViewModel
            {
                Cities = _mapper.Map<IEnumerable<City>, IEnumerable<CitiesIndexViewModel.CityViewModel>>(cities)
            };
            return View(model);
        }

        [Route("skapa")]
        public IActionResult Create()
        {
            _logger.LogDebug("Create GET: Called");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("skapa")]
        public async Task<IActionResult> Create([Bind("Name,Slug,Latitude,Longitude")] CreateOrEditCityViewModel model)
        {
            _logger.LogDebug("Create POST: called");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                return View(model);
            }

            if (await _cityRepository.GetBySlug(model.Slug) != null)
            {
                _logger.LogWarning("Create POST: There already exist a city with slug {Slug}", model.Slug);
                ModelState.AddModelError("Slug", "The slug already exists");
                return View(model);
            }

            var city = new City(model.Name, model.Slug, model.Latitude, model.Longitude);
            await _cityRepository.AddOrUpdate(city);
            _logger.LogInformation("Create POST: Created city {City} with slug {Slug}", JsonConvert.SerializeObject(city), model.Slug);
            return RedirectToAction("Index");
        }


        [Route("{urlSlug}/andra")]
        public async Task<IActionResult> Edit(string urlSlug)
        {
            _logger.LogDebug("Edit GET: Edit called with slug {Slug}", urlSlug);
            var city = await _cityRepository.GetBySlug(urlSlug);
            if (city == null)
            {
                _logger.LogWarning("Edit GET: No city with slug {Slug} found when getting city", urlSlug);
                return NotFound();
            }

            var model = _mapper.Map<City, CreateOrEditCityViewModel>(city);
            _logger.LogDebug("Edit GET: Returned model {Model}", JsonConvert.SerializeObject(model));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{urlSlug}/andra")]
        public async Task<IActionResult> Edit(string urlSlug, [Bind("Name,Slug,Latitude,Longitude")] CreateOrEditCityViewModel model)
        {
            _logger.LogDebug("Edit POST: Edit called with slug {Slug} and model {Model}", urlSlug, JsonConvert.SerializeObject(model));

            var id = _slugLookup.GetIdBySlug(urlSlug);
            if (!id.HasValue)
            {
                _logger.LogWarning("Edit POST: No city with slug {Slug} found when updating city", urlSlug);
                return NotFound();
            }

            var view = _viewRepository.Get<CityView>(id.Value);
            if (view == null)
            {
                _logger.LogError("Edit POST: No city view with id {id} found when updating city with slug {Slug}", id, urlSlug);
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Edit POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                return View(model);
            }

            if (view.Name != model.Name)
            {
                var cmd = new ChangeCityName(id.Value, model.Name);
                _logger.LogInformation("Edit POST: Edited name of city with slug {Slug} and id {Id} using the following command {Command}", urlSlug, id, JsonConvert.SerializeObject(cmd));
                await _cityService.When(cmd);
            }

            if (view.Slug != model.Slug)
            {
                var cmd = new ChangeCitySlug(id.Value, model.Slug);
                _logger.LogInformation("Edit POST: Edited slug of city with slug {Slug} and id {Id} using the following command {Command}", urlSlug, id, JsonConvert.SerializeObject(cmd));
                await _cityService.When(cmd);
            }

            if (view.Latitude != model.Latitude || view.Longitude != model.Longitude)
            {
                var cmd = new ChangeCityPosition(id.Value, model.Latitude, model.Longitude);
                _logger.LogInformation("Edit POST: Edited position of city with slug {Slug} and id {Id} using the following command {Command}", urlSlug, id, JsonConvert.SerializeObject(cmd));
                await _cityService.When(cmd);
            }

            return RedirectToAction("Index");
        }
    }
}

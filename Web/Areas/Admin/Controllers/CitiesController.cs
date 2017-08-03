using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core.Interfaces;
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

        public CitiesController(
            IMapper mapper,
            ILogger<CitiesController> logger,
            ICityRepository cityRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _cityRepository = cityRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index: Called");
            var cities = await _cityRepository.GetAll() ?? new List<City>();
            var model = new CitiesIndexViewModel
            {
                Cities = _mapper.Map<IEnumerable<City>, IEnumerable<CitiesIndexViewModel.CityViewModel>>(cities.OrderBy(c => c.Name))
            };
            ViewData["Title"] = "Städer | Fixa barnkalaset";
            return View(model);
        }

        [Route("skapa")]
        public IActionResult Create()
        {
            _logger.LogDebug("Create GET: Called");
            ViewData["Title"] = "Lägg till ny stad | Fixa barnkalaset";
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
                ModelState.AddModelError("Slug", $"Det finns redan en stad med sluggen {model.Slug}");
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
            ViewData["Title"] = $"Ändra {city.Name} | Fixa barnkalaset";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{urlSlug}/andra")]
        public async Task<IActionResult> Edit(string urlSlug, [Bind("Name,Slug,Latitude,Longitude")] CreateOrEditCityViewModel model)
        {
            _logger.LogDebug("Edit POST: Edit called with slug {Slug} and model {Model}", urlSlug, JsonConvert.SerializeObject(model));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Edit POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                return View(model);
            }

            var existingCity = await _cityRepository.GetBySlug(urlSlug);
            if (existingCity == null)
            {
                _logger.LogWarning("Edit POST: No city with slug {Slug} found when updating city", urlSlug);
                return NotFound();
            }

            if (urlSlug != model.Slug && await _cityRepository.GetBySlug(model.Slug) != null)
            {
                _logger.LogWarning("Edit POST: A city with slug {Slug} already exists.", model.Slug);
                ModelState.AddModelError("Slug", $"Det finns redan en stad med sluggen {model.Slug}");
                return View(model);
            }

            if (existingCity.Name != model.Name 
                || existingCity.Slug != model.Slug 
                || existingCity.Latitude != model.Latitude 
                || existingCity.Longitude != model.Longitude)
            {
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                _logger.LogInformation("Edit POST: Edited city from {OldCity} to {NewCity}", JsonConvert.SerializeObject(existingCity, settings), JsonConvert.SerializeObject(model, settings));
                existingCity.Name = model.Name;
                existingCity.Slug = model.Slug;
                existingCity.Latitude = model.Latitude;
                existingCity.Longitude = model.Longitude;
                await _cityRepository.AddOrUpdate(existingCity);
            }
            else
            {
                _logger.LogInformation("Edit POST: No changes detected");
            }

            return RedirectToAction("Index");
        }
    }
}

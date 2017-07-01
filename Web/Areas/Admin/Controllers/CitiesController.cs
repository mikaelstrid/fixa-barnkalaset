using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Domain.Commands;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;

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
        private readonly ISlugDictionary _slugDictionary;
        private readonly IViewRepository _viewRepository;

        public CitiesController(
            IMapper mapper,
            ILogger<CitiesController> logger,
            ICityRepository cityRepository,
            ICityService cityService,
            ISlugDictionary slugDictionary,
            IViewRepository viewRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _cityRepository = cityRepository;
            _cityService = cityService;
            _slugDictionary = slugDictionary;
            _viewRepository = viewRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            var model = _mapper.Map<IEnumerable<City>, IEnumerable<IndexCityViewModel>>(_cityRepository.GetAll());
            return View(model);
        }

        [Route("skapa")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("skapa")]
        public async Task<IActionResult> Create([Bind("Name,Slug,Latitude,Longitude")] CreateOrEditCityViewModel city)
        {
            if (ModelState.IsValid)
            {
                await _cityService.When(new CreateCity(city.Name, city.Slug, city.Latitude, city.Longitude));
                return RedirectToAction("Index");
            }
            return View();
        }


        [Route("{slug}/andra")]
        public IActionResult Edit(string slug)
        {
            var id = _slugDictionary.GetId(slug);
            if (!id.HasValue)
            {
                _logger.LogWarning("Edit: No city with slug {Slug} found when getting city", slug);
                return NotFound();
            }

            var view = _viewRepository.Get<CityView>(id.Value);
            if (view == null)
            {
                _logger.LogError("Edit: No city view with id {id} found when getting city", id);
                return NotFound();
            }

            var model = _mapper.Map<CityView, CreateOrEditCityViewModel>(view);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{slug}/andra")]
        public IActionResult Edit(string slug, [Bind("Name,Slug,Latitude,Longitude")] CreateOrEditCityViewModel city)
        {
            var id = _slugDictionary.GetId(slug);
            if (!id.HasValue)
            {
                _logger.LogWarning("Edit: No city with slug {Slug} found when updating city", slug);
                return NotFound();
            }

            var view = _viewRepository.Get<CityView>(id.Value);
            if (view == null)
            {
                _logger.LogError("Edit: No city view with id {id} found when updating city", id);
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View(city);

            if (view.Name != city.Name)
                _cityService.When(new ChangeCityName(id.Value, city.Name));

            if (view.Slug != city.Slug)
                _cityService.When(new ChangeCitySlug(id.Value, city.Slug));

            if (view.Latitude != city.Latitude || view.Longitude != city.Longitude)
                _cityService.When(new ChangeCityPosition(id.Value, city.Latitude, city.Longitude));

            return RedirectToAction("Index");
        }
    }
}

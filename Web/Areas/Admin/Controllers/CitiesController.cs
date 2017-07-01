using System.Collections.Generic;
using System.Linq;
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
using Newtonsoft.Json;

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
            _logger.LogDebug("Index: Called");
            var model = _mapper.Map<IEnumerable<City>, IEnumerable<IndexCityViewModel>>(_cityRepository.GetAll());
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
        public async Task<IActionResult> Create([Bind("Name,Slug,Latitude,Longitude")] CreateOrEditCityViewModel city)
        {
            _logger.LogDebug("Create POST: called");
            if (ModelState.IsValid)
            {
                var cmd = new CreateCity(city.Name, city.Slug, city.Latitude, city.Longitude);
                var id = await _cityService.When(cmd);
                _logger.LogInformation("Create POST: Created city with id {Id} using the following command {Command}", id, JsonConvert.SerializeObject(cmd));
                return RedirectToAction("Index");
            }
            _logger.LogWarning("Create POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
            return View();
        }


        [Route("{slug}/andra")]
        public IActionResult Edit(string slug)
        {
            _logger.LogDebug("Edit GET: Edit called with slug {Slug}", slug);
            var id = _slugDictionary.GetId(slug);
            if (!id.HasValue)
            {
                _logger.LogWarning("Edit GET: No city with slug {Slug} found when getting city", slug);
                return NotFound();
            }

            var view = _viewRepository.Get<CityView>(id.Value);
            if (view == null)
            {
                _logger.LogError("Edit GET: No city view with id {id} found when getting city", id);
                return NotFound();
            }

            var model = _mapper.Map<CityView, CreateOrEditCityViewModel>(view);
            _logger.LogDebug("Edit GET: Returned model {Model}", JsonConvert.SerializeObject(model));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{slug}/andra")]
        public IActionResult Edit(string slug, [Bind("Name,Slug,Latitude,Longitude")] CreateOrEditCityViewModel model)
        {
            _logger.LogDebug("Edit POST: Edit called with slug {Slug} and model {Model}", slug, JsonConvert.SerializeObject(model));

            var id = _slugDictionary.GetId(slug);
            if (!id.HasValue)
            {
                _logger.LogWarning("Edit POST: No city with slug {Slug} found when updating city", slug);
                return NotFound();
            }

            var view = _viewRepository.Get<CityView>(id.Value);
            if (view == null)
            {
                _logger.LogError("Edit POST: No city view with id {id} found when updating city with slug {Slug}", id, slug);
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
                _logger.LogInformation("Edit POST: Edited name of city with slug {Slug} and id {Id} using the following command {Command}", slug, id, JsonConvert.SerializeObject(cmd));
                _cityService.When(cmd);
            }

            if (view.Slug != model.Slug)
            {
                var cmd = new ChangeCitySlug(id.Value, model.Slug);
                _logger.LogInformation("Edit POST: Edited slug of city with slug {Slug} and id {Id} using the following command {Command}", slug, id, JsonConvert.SerializeObject(cmd));
                _cityService.When(cmd);
            }

            if (view.Latitude != model.Latitude || view.Longitude != model.Longitude)
            {
                var cmd = new ChangeCityPosition(id.Value, model.Latitude, model.Longitude);
                _logger.LogInformation("Edit POST: Edited position of city with slug {Slug} and id {Id} using the following command {Command}", slug, id, JsonConvert.SerializeObject(cmd));
                _cityService.When(cmd);
            }

            return RedirectToAction("Index");
        }
    }
}

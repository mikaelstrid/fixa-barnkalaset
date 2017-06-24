using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Commands;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/stader")]
    //[Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        private readonly ICityService _cityService;

        public CitiesController(
            IMapper mapper,
            ICityRepository cityRepository,
            ICityService cityService)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _cityService = cityService;
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

        [Route("{citySlug}/andra")]
        public IActionResult Edit(string citySlug)
        {
            var model = _mapper.Map<City, CreateOrEditCityViewModel>(_cityRepository.GetBySlug(citySlug));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{citySlug}/andra")]
        public IActionResult Edit(string citySlug, [Bind("Name,Slug,Latitude,Longitude")] CreateOrEditCityViewModel city)
        {
            if (citySlug != city.Slug) return NotFound();

            if (ModelState.IsValid)
            {
                var model = _mapper.Map<CreateOrEditCityViewModel, City>(city);
                _cityRepository.AddOrUpdate(model);
                return RedirectToAction("Index");
            }

            return View(city);
        }
    }
}

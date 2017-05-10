using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;
using Pixel.Kidsparties.Web.Areas.Admin.ViewModels;

namespace Pixel.Kidsparties.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/arrangemang")]
    public class ArrangementsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IArrangementRepository _arrangementRepository;
        private readonly ICityRepository _cityRepository;

        public ArrangementsController(
            IMapper mapper,
            IArrangementRepository arrangementRepository, 
            ICityRepository cityRepository)
        {
            _mapper = mapper;
            _arrangementRepository = arrangementRepository;
            _cityRepository = cityRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(_arrangementRepository.GetAll());
        }

        [Route("skapa")]
        public IActionResult Create(int id)
        {
            return View(new CreateOrEditArrangementViewModel
            {
                Cities = _cityRepository.GetAll().Select(c => new SelectListItem { Value = c.Slug, Text = c.Name })
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("skapa")]
        public IActionResult Create([Bind("Id,Name,Slug,CitySlug,Pitch,Description,GooglePlacesId,CoverImage,StreetAddress,PostalCode,PostalCity,PhoneNumber,EmailAddress,Website,Latitude,Longitude")] CreateOrEditArrangementViewModel arrangement)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<CreateOrEditArrangementViewModel, Arrangement>(arrangement);
                _arrangementRepository.AddOrUpdate(model);
                return RedirectToAction("Index");
            }

            arrangement.Cities = _cityRepository.GetAll().Select(c => new SelectListItem { Value = c.Slug, Text = c.Name });
            return View(arrangement);
        }

        [Route("{id}/andra")]
        public IActionResult Edit(int id)
        {
            var model = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(_arrangementRepository.GetById(id));
            model.Cities = _cityRepository.GetAll().Select(c => new SelectListItem { Value = c.Slug, Text = c.Name });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/andra")]
        public IActionResult Edit(int id, [Bind("Id,Name,Slug,CitySlug,Pitch,Description,GooglePlacesId,CoverImage,StreetAddress,PostalCode,PostalCity,PhoneNumber,EmailAddress,Website,Latitude,Longitude")] CreateOrEditArrangementViewModel arrangement)
        {
            if (id != arrangement.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var model = _mapper.Map<CreateOrEditArrangementViewModel, Arrangement>(arrangement);
                _arrangementRepository.AddOrUpdate(model);
                return RedirectToAction("Index");
            }

            arrangement.Cities = _cityRepository.GetAll().Select(c => new SelectListItem { Value = c.Slug, Text = c.Name });
            return View(arrangement);
        }
    }
}

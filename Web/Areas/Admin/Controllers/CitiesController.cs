using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/stader")]
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;

        public CitiesController(
            IMapper mapper,
            ICityRepository cityRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
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
        public IActionResult Create([Bind("Name,Slug,Latitude,Longitude")] CreateOrEditCityViewModel city)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<CreateOrEditCityViewModel, City>(city);
                _cityRepository.AddOrUpdate(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        //[Route("{id}/andra")]
        //public IActionResult Edit(int id)
        //{
        //    var model = _mapper.Map<Arrangement, CreateOrEditArrangementViewModel>(_arrangementRepository.GetById(id));
        //    model.Cities = _cityRepository.GetAll().Select(c => new SelectListItem { Value = c.Slug, Text = c.Name });
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("{id}/andra")]
        //public IActionResult Edit(int id, [Bind("Id,Name,Slug,CitySlug,Pitch,Description,GooglePlacesId,CoverImage,StreetAddress,PostalCode,PostalCity,PhoneNumber,EmailAddress,Website,Latitude,Longitude")] CreateOrEditArrangementViewModel arrangement)
        //{
        //    if (id != arrangement.Id) return NotFound();

        //    if (ModelState.IsValid)
        //    {
        //        var model = _mapper.Map<CreateOrEditArrangementViewModel, Arrangement>(arrangement);
        //        _arrangementRepository.AddOrUpdate(model);
        //        return RedirectToAction("Index");
        //    }

        //    arrangement.Cities = _cityRepository.GetAll().Select(c => new SelectListItem { Value = c.Slug, Text = c.Name });
        //    return View(arrangement);
        //}
    }
}

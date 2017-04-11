using Microsoft.AspNetCore.Mvc;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/arrangemang")]
    public class ArrangementsController : Controller
    {
        private readonly IArrangementRepository _arrangementRepository;

        public ArrangementsController(IArrangementRepository arrangementRepository)
        {
            _arrangementRepository = arrangementRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(_arrangementRepository.GetAll());
        }

        [Route("skapa")]
        public IActionResult Create(int id)
        {
            return View(new Arrangement
            {
                Id = _arrangementRepository.GetNextId(),
                Country = "SE"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("skapa")]
        public IActionResult Create([Bind("Id,Name,Slug,CategoryCity,Pitch,Description,CoverImage,StreetAddress,PostalCode,PostalCity,Country,PhoneNumber,EmailAddress,Latitude,Longitude")] Arrangement arrangement)
        {
            if (ModelState.IsValid)
            {
                _arrangementRepository.AddOrUpdate(arrangement);
                return RedirectToAction("Index");
            }

            return View(arrangement);
        }

        [Route("{id}/andra")]
        public IActionResult Edit(int id)
        {
            return View(_arrangementRepository.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/andra")]
        public IActionResult Edit(int id, [Bind("Id,Name,Slug,CategoryCity,Pitch,Description,CoverImage,StreetAddress,PostalCode,PostalCity,Country,PhoneNumber,EmailAddress,Latitude,Longitude")] Arrangement arrangement)
        {
            if (id != arrangement.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _arrangementRepository.AddOrUpdate(arrangement);
                return RedirectToAction("Index");
            }

            return View(arrangement);
        }
    }
}

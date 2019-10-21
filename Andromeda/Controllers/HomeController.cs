using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Andromeda.Models;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;

namespace Andromeda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICityRepository _cityRepository;

        public HomeController(ILogger<HomeController> logger, ICityRepository cityRepository)
        {
            _logger = logger;
            _cityRepository = cityRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Hitta det perfekta barnkalaset! | Fixa barnkalaset";
            ViewData["Description"] = "Vi har samlat en massa bra ställen som erbjuder nya spännande barnkalas för era barn och deras kompisar.";

            ViewData["OgTitle"] = "Hitta det perfekta barnkalaset!";
            ViewData["OgDescription"] = "Vi har samlat en massa bra ställen som erbjuder nya spännande barnkalas för era barn och deras kompisar.";
            ViewData["OgImage"] = Request?.Scheme + "://" + Request?.Host + "/images/balloons-1869790_1200_630.jpg";

            var cities = await _cityRepository.GetAll() ?? new List<City>();
            return View(new HomeIndexViewModel
            {
                Cities = cities.Select(HomeIndexViewModel.CityViewModel.MapFromBusinessModel).OrderBy(c => c.Name)
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

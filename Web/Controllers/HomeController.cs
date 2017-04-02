using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArrangementRepository _arrangementRepository;

        public HomeController(IArrangementRepository arrangementRepository)
        {
            _arrangementRepository = arrangementRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(new HomeIndexViewModel
            {
                Arrangements = _arrangementRepository.GetAll(),
                CitiesInSouth = new[] { "Göteborg", "Malmö", "Linköping", "Helsingborg", "Jönköping", "Norrköping", "Lund", "Borås", "Halmstad", "Växjö" },
                CitiesInMiddle = new[] { "Stockholm", "Uppsala", "Västerås", "Örebro", "Gävle", "Södertälje", "Eskilstuna", "Karlstad" },
                CitiesInNorth = new[] { "Umeå" }
            });
        }

        public IActionResult Error()
        {
            return View();
        }
    }

    public class HomeIndexViewModel
    {
        public IEnumerable<Arrangement> Arrangements { get; set; }
        public IEnumerable<string> CitiesInSouth { get; set; }
        public IEnumerable<string> CitiesInMiddle { get; set; }
        public IEnumerable<string> CitiesInNorth { get; set; }
    }
}

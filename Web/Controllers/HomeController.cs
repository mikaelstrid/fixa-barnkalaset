using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Models;
using Pixel.FixaBarnkalaset.Web.Utilities;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        private readonly ISitemapGenerator _sitemapGenerator;

        public HomeController(
            IMapper mapper,
            ICityRepository cityRepository,
            ISitemapGenerator sitemapGenerator)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _sitemapGenerator = sitemapGenerator;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Hitta det perfekta barnkalaset! | Fixa barnkalaset";
            ViewData["Description"] = "Vi har samlat en massa bra ställen som hjälper dig att fixa det perfekta barnkalaset.";

            var cities = await _cityRepository.GetAll() ?? new List<City>();
            return View(new HomeIndexViewModel
            {
                Cities = _mapper.Map<IEnumerable<City>, IEnumerable<HomeIndexViewModel.CityViewModel>>(cities.OrderBy(c => c.Name))
                //CitiesInSouth = new[] { "Göteborg", "Malmö", "Linköping", "Helsingborg", "Jönköping", "Norrköping", "Lund", "Borås", "Halmstad", "Växjö" },
                //CitiesInMiddle = new[] { "Stockholm", "Uppsala", "Västerås", "Örebro", "Gävle", "Södertälje", "Eskilstuna", "Karlstad" },
                //CitiesInNorth = new[] { "Umeå" }
            });
        }

        [Route("cookies")]
        public IActionResult Cookies()
        {
            ViewData["Title"] = "Cookies | Fixa barnkalaset";
            return View();
        }

        [Route("sitemap.xml")]
        public async Task<ActionResult> SitemapXml()
        {
            var xml = await _sitemapGenerator.GetAsString(_cityRepository);
            return Content(xml, "application/xml");
        }
    }
}

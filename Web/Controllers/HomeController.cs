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
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ISitemapGenerator _sitemapGenerator;

        public HomeController(
            IMapper mapper,
            ICityRepository cityRepository,
            IBlogPostRepository blogPostRepository,
            ISitemapGenerator sitemapGenerator)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _blogPostRepository = blogPostRepository;
            _sitemapGenerator = sitemapGenerator;
        }

        [HttpGet]
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
                Cities = _mapper.Map<IEnumerable<City>, IEnumerable<HomeIndexViewModel.CityViewModel>>(cities.OrderBy(c => c.Name))
            });
        }

        [HttpGet("cookies")]
        public IActionResult Cookies()
        {
            ViewData["Title"] = "Cookies | Fixa barnkalaset";
            return View();
        }

        [HttpGet("sitemap.xml")]
        public async Task<ActionResult> SitemapXml()
        {
            var xml = await _sitemapGenerator.GetAsString(_cityRepository, _blogPostRepository);
            return Content(xml, "application/xml");
        }
    }
}

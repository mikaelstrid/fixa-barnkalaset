using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Models;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;

        //public HomeController(
        //    IMapper mapper, 
        //    ICityRepository cityRepository)
        //{
        //    _mapper = mapper;
        //    _cityRepository = cityRepository;
        //}

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(new HomeIndexViewModel
            {
                Cities = _mapper.Map<IEnumerable<City>, IEnumerable<HomeIndexViewModel.CityViewModel>>(new List<City>())
                //CitiesInSouth = new[] { "Göteborg", "Malmö", "Linköping", "Helsingborg", "Jönköping", "Norrköping", "Lund", "Borås", "Halmstad", "Växjö" },
                //CitiesInMiddle = new[] { "Stockholm", "Uppsala", "Västerås", "Örebro", "Gävle", "Södertälje", "Eskilstuna", "Karlstad" },
                //CitiesInNorth = new[] { "Umeå" }
            });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

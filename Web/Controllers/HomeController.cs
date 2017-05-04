using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;
using Pixel.Kidsparties.Web.Models;

namespace Pixel.Kidsparties.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;

        public HomeController(
            IMapper mapper, 
            ICityRepository cityRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(new HomeIndexViewModel
            {
                Cities = _mapper.Map<IEnumerable<City>, IEnumerable<HomeIndexViewModel.CityViewModel>>(_cityRepository.GetAll())
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

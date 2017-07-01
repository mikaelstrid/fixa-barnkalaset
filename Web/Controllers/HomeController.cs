using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Pixel.FixaBarnkalaset.Web.Models;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IViewRepository _viewRepository;

        public HomeController(
            IMapper mapper,
            IViewRepository viewRepository)
        {
            _mapper = mapper;
            _viewRepository = viewRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            var view = _viewRepository.Get<CityListView>(CityListView.ListViewId);
            return View(new HomeIndexViewModel
            {
                Cities = _mapper.Map<IEnumerable<CityListView.City>, IEnumerable<HomeIndexViewModel.CityViewModel>>(view?.Cities ?? new List<CityListView.City>())
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

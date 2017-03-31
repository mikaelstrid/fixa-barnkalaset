using Microsoft.AspNetCore.Mvc;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Web.Controllers
{
    [Route("stad")]
    public class CityController : Controller
    {
        private readonly IArrangementRepository _arrangementRepository;

        public CityController(IArrangementRepository arrangementRepository)
        {
            _arrangementRepository = arrangementRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(_arrangementRepository.GetAll());
        }
    }
}

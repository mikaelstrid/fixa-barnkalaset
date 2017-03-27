using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View(_arrangementRepository.GetAll());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

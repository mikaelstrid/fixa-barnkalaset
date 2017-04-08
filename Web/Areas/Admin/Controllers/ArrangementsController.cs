using Microsoft.AspNetCore.Mvc;
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

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            return View(_arrangementRepository.GetById(id));
        }

    }
}

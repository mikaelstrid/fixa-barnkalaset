using Microsoft.AspNetCore.Mvc;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    [Route("fel")]
    public class ErrorController : Controller
    {
        [Route("404")]
        public IActionResult Error404()
        {
            return View();
        }

        [Route("{code:int}")]
        public IActionResult Error(int code)
        {
            return View();
        }

        [Route("")]
        public IActionResult Error()
        {
            return View();
        }
    }
}

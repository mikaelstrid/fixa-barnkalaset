﻿using Microsoft.AspNetCore.Mvc;

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
    }
}

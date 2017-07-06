using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Web.Tests.Admin.Controllers
{
    public abstract class ControllerTestBase
    {
        protected static void AddModelStateError(ControllerBase controller)
        {
            controller.ModelState.AddModelError("key", "error message");
        }
    }
}
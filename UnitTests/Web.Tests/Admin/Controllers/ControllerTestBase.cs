using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;

namespace UnitTests.Web.Tests.Admin.Controllers
{
    public abstract class ControllerTestBase<T> where T : Controller
    {
        protected static void AddModelStateError(ControllerBase controller)
        {
            controller.ModelState.AddModelError("key", "error message");
        }

        protected void VerifyLogging(Mock<ILogger<T>> mockLogger, LogLevel logLevel)
        {
            mockLogger.Verify(
                m => m.Log(
                    logLevel,
                    It.IsAny<EventId>(),
                    //It.Is<FormattedLogValues>(v => v.ToString().Contains("CreateInvoiceFailed")),
                    It.IsAny<FormattedLogValues>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );
        }
    }
}
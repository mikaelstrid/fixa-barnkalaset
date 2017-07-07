using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using Pixel.FixaBarnkalaset.Web;

namespace UnitTests.Web.Tests
{
    public abstract class ControllerTestBase<T> where T : Controller
    {
        // ReSharper disable once InconsistentNaming
        protected readonly IMapper _mapper;

        protected ControllerTestBase()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
        }

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
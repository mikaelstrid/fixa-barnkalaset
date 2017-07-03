using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Messaging;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Xunit;
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace UnitTests.Infrastructure.Tests.Messaging
{
    public class EventPublisherTests
    {
        private readonly Mock<MyEventSourcingDbContext> _mockDbContext;
        private readonly Mock<IProjectionRegistry> _mockObserverRegistry;
        private readonly EventPublisher _sut;
        private readonly Mock<IProjection> _mockObserver1;
        private readonly Mock<IProjection> _mockObserver2;
        private readonly Mock<ILogger<EventPublisher>> _mockLogger;

        public EventPublisherTests()
        {
            var contextOptions = new DbContextOptions<MyEventSourcingDbContext>();
            _mockDbContext = new Mock<MyEventSourcingDbContext>(contextOptions);
            _mockObserver1 = new Mock<IProjection>();
            _mockObserver2 = new Mock<IProjection>();
            _mockObserverRegistry = new Mock<IProjectionRegistry>();
            _mockObserverRegistry.Setup(m => m.GetObservers()).Returns(new List<IProjection>
            {
                _mockObserver1.Object,
                _mockObserver2.Object
            });
            _mockLogger = new Mock<ILogger<EventPublisher>>();
            _sut = new EventPublisher(_mockDbContext.Object, _mockObserverRegistry.Object, _mockLogger.Object);
        }

        [Fact]
        public void Constructor_ShouldGetObserversFromTheRegistry()
        {
            // ARRANGE
            var mockObserverRegistry = new Mock<IProjectionRegistry>();

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            new EventPublisher(_mockDbContext.Object, mockObserverRegistry.Object, _mockLogger.Object);

            // ASSERT
            mockObserverRegistry.Verify(m => m.GetObservers(), Times.Once);
        }
        
        [Fact]
        public void Publish_GivenTwoObservers_ShouldCallEachObserverOnce()
        {
            // ARRANGE
            var @event = new CityCreated(Guid.Parse("5FC8CE37-E90F-4D45-8D52-C93A8939B4A2"), "Halmstad", "halmstad", 10.0, 10.0);

            // ACT
            _sut.Publish(@event);

            // ASSERT
            _mockObserver1.Verify(m => m.Handle(@event), Times.Once);
            _mockObserver2.Verify(m => m.Handle(@event), Times.Once);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Messaging;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using UnitTests.Infrastructure.Tests.Persistence.Repositories;
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
            _mockDbContext = MyEventSourcingDbContextHelper.CreateMockDbContext(new List<EventData>());
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


        [Fact]
        public void CatchUp_GivenLastPositionZero_ButNoEvents_ShouldNotPublishAnyEvents()
        {
            // ARRANGE

            // ACT
            _sut.CatchUp(0);

            // ASSERT
            _mockObserver1.Verify(m => m.Handle(It.IsAny<IEvent>()), Times.Never());
            _mockObserver2.Verify(m => m.Handle(It.IsAny<IEvent>()), Times.Never());
        }

        [Fact]
        public void CatchUp_GivenLastPositionZero_ShouldPublishAllEvents()
        {
            // ARRANGE
            var id = Guid.Parse("1FA1C88A-9B76-4134-AFB4-216639F91975");
            var localMockDbContext = MyEventSourcingDbContextHelper.CreateMockDbContext(new List<EventData>
            {
                new EventData()
                {
                    Id = 1,
                    Metadata = "{\"EventClrType\":\"Pixel.FixaBarnkalaset.Domain.Events.CityCreated, Pixel.FixaBarnkalaset.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\"}",
                    Event = "{\"Name\":\"Halmstad\",\"Slug\":\"halmstad\",\"Latitude\":56.6743748,\"Longitude\":12.8577885,\"Id\":\"123c6607-9884-4dc4-8835-2e02b52ef279\"}"
                },
                new EventData()
                {
                    Id = 2,
                    Metadata = "{\"EventClrType\":\"Pixel.FixaBarnkalaset.Domain.Events.CityCreated, Pixel.FixaBarnkalaset.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\"}",
                    Event = "{\"Name\":\"Borås\",\"Slug\":\"boras\",\"Latitude\":57.721035,\"Longitude\":12.9398189999999,\"Id\":\"ba8e50a1-55fd-4cb5-8e92-95585044963b\"}"
                }
            });
            var localSut = new EventPublisher(localMockDbContext.Object, _mockObserverRegistry.Object, _mockLogger.Object);

            // ACT
            localSut.CatchUp(0);

            // ASSERT
            _mockObserver1.Verify(m => m.Handle(It.IsAny<IEvent>()), Times.Exactly(2));
            _mockObserver2.Verify(m => m.Handle(It.IsAny<IEvent>()), Times.Exactly(2));
        }
    }
}

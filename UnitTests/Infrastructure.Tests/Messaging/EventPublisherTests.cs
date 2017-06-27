using System.Collections.Generic;
using Moq;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Messaging;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Xunit;
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace UnitTests.Infrastructure.Tests.Messaging
{
    public class EventPublisherTests
    {
        private readonly Mock<IObserverRegistry> _mockObserverRegistry;
        private readonly EventPublisher _sut;
        private readonly Mock<IObserver> _mockObserver1;
        private readonly Mock<IObserver> _mockObserver2;

        public EventPublisherTests()
        {
            _mockObserver1 = new Mock<IObserver>();
            _mockObserver2 = new Mock<IObserver>();
            _mockObserverRegistry = new Mock<IObserverRegistry>();
            _mockObserverRegistry.Setup(m => m.GetObservers()).Returns(new List<IObserver>
            {
                _mockObserver1.Object,
                _mockObserver2.Object
            });
            _sut = new EventPublisher(_mockObserverRegistry.Object);
        }

        [Fact]
        public void Constructor_ShouldGetObserversFromTheRegistry()
        {
            // ARRANGE
            var mockObserverRegistry = new Mock<IObserverRegistry>();

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            new EventPublisher(mockObserverRegistry.Object);

            // ASSERT
            mockObserverRegistry.Verify(m => m.GetObservers(), Times.Once);
        }
        
        [Fact]
        public void Publish_GivenTwoObservers_ShouldCallEachObserverOnce()
        {
            // ARRANGE
            var mockEvent = new Mock<IEvent>();

            // ACT
            _sut.Publish(mockEvent.Object);

            // ASSERT
            _mockObserver1.Verify(m => m.Handle(mockEvent.Object), Times.Once);
            _mockObserver2.Verify(m => m.Handle(mockEvent.Object), Times.Once);
        }
    }
}

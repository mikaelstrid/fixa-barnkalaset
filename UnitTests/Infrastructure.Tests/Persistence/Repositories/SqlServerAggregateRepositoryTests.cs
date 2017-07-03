using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Domain.Model;
using Pixel.FixaBarnkalaset.Infrastructure.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories;
using Xunit;
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace UnitTests.Infrastructure.Tests.Persistence.Repositories
{
    public class SqlServerAggregateRepositoryTests
    {
        private readonly Mock<MyEventSourcingDbContext> _mockDbContext;
        private readonly Mock<IAggregateFactory> _mockAggregateFactory;
        private readonly Mock<IEventPublisher> _mockEventPublisher;
        private readonly Mock<IAggregate> _mockAggregate;
        private readonly SqlServerAggregateRepository _sut;

        public SqlServerAggregateRepositoryTests()
        {
            _mockDbContext = MyEventSourcingDbContextHelper.CreateMockDbContext(Enumerable.Empty<EventData>());
            _mockAggregateFactory = new Mock<IAggregateFactory>();
            _mockEventPublisher = new Mock<IEventPublisher>();
            _mockAggregate = new Mock<IAggregate>();
            _sut = new SqlServerAggregateRepository(_mockDbContext.Object, _mockAggregateFactory.Object, _mockEventPublisher.Object);
        }

        [Fact]
        public async Task Save_GivenNoUncommitedEvents_ShouldNotCallEventPublisher()
        {
            // ARRANGE
            _mockAggregate.Setup(m => m.GetUncommittedEvents()).Returns(new List<IEvent>());
            
            // ACT
            await _sut.Save(_mockAggregate.Object);

            // ASSERT
            _mockEventPublisher.Verify(m => m.Publish(It.IsAny<IEvent>()), Times.Never);
        }

        [Fact]
        public async Task Save_GivenOneUncommitedEvent_ShouldPublishThatEvent()
        {
            // ARRANGE
            _mockAggregate.Setup(m => m.GetUncommittedEvents()).Returns(new List<IEvent>
            {
                new CityCreated(Guid.NewGuid(), "Halmstad", "halmstad", 10.0, 56.1)
            });
            
            // ACT
            await _sut.Save(_mockAggregate.Object);

            // ASSERT
            _mockEventPublisher.Verify(m => m.Publish(It.IsAny<IEvent>()), Times.Once);

        }

        [Fact]
        public async Task Save_GivenUncommitedEvents_ShouldPublishThoseEvents()
        {
            // ARRANGE
            _mockAggregate.Setup(m => m.GetUncommittedEvents()).Returns(new List<IEvent>
            {
                new CityCreated(Guid.NewGuid(), "Halmstad", "halmstad", 10.0, 56.1),
                new CityCreated(Guid.NewGuid(), "Borås", "boras", 10.0, 56.1)
            });

            // ACT
            await _sut.Save(_mockAggregate.Object);

            // ASSERT
            _mockEventPublisher.Verify(m => m.Publish(It.IsAny<IEvent>()), Times.Exactly(2));
        }
    }
}

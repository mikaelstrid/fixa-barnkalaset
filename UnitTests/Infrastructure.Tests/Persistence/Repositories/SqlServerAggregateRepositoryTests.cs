using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var companyProducts = Enumerable.Empty<EventData>().AsQueryable();

            var mockSet = new Mock<DbSet<EventData>>();

            mockSet.As<IAsyncEnumerable<EventData>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<EventData>(companyProducts.GetEnumerator()));

            mockSet.As<IQueryable<EventData>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<EventData>(companyProducts.Provider));

            mockSet.As<IQueryable<EventData>>().Setup(m => m.Expression).Returns(companyProducts.Expression);
            mockSet.As<IQueryable<EventData>>().Setup(m => m.ElementType).Returns(companyProducts.ElementType);
            mockSet.As<IQueryable<EventData>>().Setup(m => m.GetEnumerator()).Returns(() => companyProducts.GetEnumerator());

            var contextOptions = new DbContextOptions<MyEventSourcingDbContext>();
            _mockDbContext = new Mock<MyEventSourcingDbContext>(contextOptions);
            _mockDbContext.SetupGet(c => c.Events).Returns(mockSet.Object);

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

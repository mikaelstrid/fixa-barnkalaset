using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace UnitTests.Infrastructure.Tests.Persistence.Repositories
{
    public static class MyEventSourcingDbContextHelper
    {
        public static Mock<MyEventSourcingDbContext> CreateMockDbContext(IEnumerable<EventData> events)
        {
            var queryableEvents = events.AsQueryable();

            var mockSet = new Mock<DbSet<EventData>>();

            mockSet.As<IAsyncEnumerable<EventData>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<EventData>(queryableEvents.GetEnumerator()));

            mockSet.As<IQueryable<EventData>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<EventData>(queryableEvents.Provider));

            mockSet.As<IQueryable<EventData>>().Setup(m => m.Expression).Returns(queryableEvents.Expression);
            mockSet.As<IQueryable<EventData>>().Setup(m => m.ElementType).Returns(queryableEvents.ElementType);
            mockSet.As<IQueryable<EventData>>().Setup(m => m.GetEnumerator()).Returns(() => queryableEvents.GetEnumerator());

            var contextOptions = new DbContextOptions<MyEventSourcingDbContext>();
            var mockDbContext = new Mock<MyEventSourcingDbContext>(contextOptions);
            mockDbContext.SetupGet(c => c.Events).Returns(mockSet.Object);
            return mockDbContext;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace UnitTests.Infrastructure.Tests.Persistence.Repositories
{
    public static class MyDataDbContextHelper
    {
        public static Mock<MyDataDbContext> CreateMockDbContext(IEnumerable<City> cities = null, IEnumerable<Party> parties = null)
        {
            var contextOptions = new DbContextOptions<MyDataDbContext>();
            var mockDbContext = new Mock<MyDataDbContext>(contextOptions, new Mock<IHttpContextAccessor>().Object);

            if (cities != null)
                mockDbContext.SetupGet(c => c.Cities).Returns(CreateMockSet(cities).Object);

            if (parties != null)
                mockDbContext.SetupGet(c => c.Parties).Returns(CreateMockSet(parties).Object);

            return mockDbContext;
        }

        private static Mock<DbSet<T>> CreateMockSet<T>(IEnumerable<T> cities) where T : class
        {
            var queryableEvents = cities.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<T>(queryableEvents.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<City>(queryableEvents.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableEvents.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableEvents.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableEvents.GetEnumerator());
            return mockSet;
        }
    }
}

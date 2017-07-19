using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace UnitTests.Infrastructure.Tests.Persistence.Repositories
{
    public static class MyDataDbContextHelper
    {
        public static Mock<MyDataDbContext> CreateMockDbContext(IEnumerable<City> cities)
        {
            var queryableEvents = cities.AsQueryable();

            var mockSet = new Mock<DbSet<City>>();

            mockSet.As<IAsyncEnumerable<City>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<City>(queryableEvents.GetEnumerator()));

            mockSet.As<IQueryable<City>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<City>(queryableEvents.Provider));

            mockSet.As<IQueryable<City>>().Setup(m => m.Expression).Returns(queryableEvents.Expression);
            mockSet.As<IQueryable<City>>().Setup(m => m.ElementType).Returns(queryableEvents.ElementType);
            mockSet.As<IQueryable<City>>().Setup(m => m.GetEnumerator()).Returns(() => queryableEvents.GetEnumerator());

            var contextOptions = new DbContextOptions<MyDataDbContext>();
            var mockDbContext = new Mock<MyDataDbContext>(contextOptions, new Mock<IHttpContextAccessor>().Object);
            mockDbContext.SetupGet(c => c.Cities).Returns(mockSet.Object);
            return mockDbContext;
        }
    }
}

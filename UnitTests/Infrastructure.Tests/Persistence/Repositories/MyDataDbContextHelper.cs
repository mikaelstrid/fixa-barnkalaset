using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace UnitTests.Infrastructure.Tests.Persistence.Repositories
{
    public static class MyDataDbContextHelper
    {
        public static Mock<MyDataDbContext> CreateMockDbContext(IEnumerable<City> cities = null, IEnumerable<Party> parties = null, IEnumerable<Invitation> invitations = null)
        {
            var contextOptions = new DbContextOptions<MyDataDbContext>();
            var mockDbContext = new Mock<MyDataDbContext>(contextOptions, new Mock<IHttpContextAccessor>().Object);

            if (cities != null)
            {
                var mockSet = CreateMockSet(cities);
                mockDbContext.SetupGet(c => c.Cities).Returns(mockSet.Object);
                mockDbContext.Setup(c => c.Set<City>()).Returns(mockSet.Object);
            }

            if (parties != null)
            {
                var mockSet = CreateMockSet(parties);
                mockDbContext.SetupGet(c => c.Parties).Returns(mockSet.Object);
                mockDbContext.Setup(c => c.Set<Party>()).Returns(mockSet.Object);
                mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                    .Returns<object[]>(ids => Task.FromResult(parties.FirstOrDefault(e => e.Id == (string) ids[0])));
            }

            if (invitations != null)
            {
                var mockSet = CreateMockSet(invitations);
                mockDbContext.SetupGet(c => c.Invitations).Returns(mockSet.Object);
                mockDbContext.Setup(c => c.Set<Invitation>()).Returns(mockSet.Object);
                mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                    .Returns<object[]>(ids => Task.FromResult(invitations.FirstOrDefault(e => e.PartyId == (string)ids[0] && e.Id == (string)ids[1])));
            }

            return mockDbContext;
        }

        private static Mock<DbSet<T>> CreateMockSet<T>(IEnumerable<T> entities) where T : class
        {
            var queryableEntities = entities.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<T>(queryableEntities.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<City>(queryableEntities.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableEntities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableEntities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableEntities.GetEnumerator());

            return mockSet;
        }
    }
}

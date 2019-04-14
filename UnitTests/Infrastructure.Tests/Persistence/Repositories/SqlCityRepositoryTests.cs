using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories;
using UnitTests.Utilities.TestDataExtensions;
using Xunit;

namespace UnitTests.Infrastructure.Tests.Persistence.Repositories
{
    public class SqlCityRepositoryTests
    {
        [Fact]
        public async Task AddOrUpdate_GivenCityWithArrangement_ShouldNotThrowJsonSerializationException()
        {
            // ARRANGE
            var halmstad = new City().Halmstad();
            halmstad.Busfabriken();
            var mockContext = MyDataDbContextHelper.CreateMockDbContext(new[] {halmstad});
            var sut = new SqlCityRepository(mockContext.Object, new Mock<ILogger<SqlCityRepository>>().Object);

            var updatedHalmstad = new City().Halmstad();
            updatedHalmstad.Busfabriken();
            updatedHalmstad.Slug = "halmstad-ii";

            // ACT
            await sut.AddOrUpdate(updatedHalmstad);
            
            // ASSERT
            mockContext.Verify(m => m.Update(halmstad), Times.Once());
        }
    }
}

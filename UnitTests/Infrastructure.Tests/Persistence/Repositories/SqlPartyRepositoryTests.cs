using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories;
using Xunit;

namespace UnitTests.Infrastructure.Tests.Persistence.Repositories
{
    public class SqlPartyRepositoryTests
    {
        private readonly Party _existingParty;
        private readonly Mock<IPartyIdGenerator> _mockPartyIdGenerator;
        private readonly SqlPartyRepository _sut;

        public SqlPartyRepositoryTests()
        {
            _existingParty = new Party { Id = "ABCD-12" };
            var mockContext = MyDataDbContextHelper.CreateMockDbContext(parties: new[] { _existingParty });
            _mockPartyIdGenerator = new Mock<IPartyIdGenerator>();
            _mockPartyIdGenerator.Setup(m => m.Next()).Returns("DCBA-21");
            _sut = new SqlPartyRepository(mockContext.Object, new Mock<ILogger<SqlPartyRepository>>().Object, _mockPartyIdGenerator.Object);
        }

        [Fact]
        public async Task AddOrUpdate_GivenEntityWithoutId_ShouldCallIdGenerator()
        {
            // ARRANGE
            var party = new Party();

            // ACT
            await _sut.AddOrUpdate(party);
            
            // ASSERT
            _mockPartyIdGenerator.Verify(m => m.Next(), Times.Once());
        }

        [Fact]
        public async Task AddOrUpdate_GivenEntityWithId_ShouldNotCallIdGenerator()
        {
            // ARRANGE
            var updatedParty = new Party { Id = _existingParty.Id };

            // ACT
            await _sut.AddOrUpdate(updatedParty);

            // ASSERT
            _mockPartyIdGenerator.Verify(m => m.Next(), Times.Never);
        }

    }
}

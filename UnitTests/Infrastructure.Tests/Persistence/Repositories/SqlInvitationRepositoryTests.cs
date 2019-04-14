using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories;
using Xunit;

namespace UnitTests.Infrastructure.Tests.Persistence.Repositories
{
    public class SqlInvitationRepositoryTests
    {
        private readonly Invitation _existingInvitation;
        private readonly Mock<IInvitationIdGenerator> _mockInvitationIdGenerator;
        private readonly SqlInvitationRepository _sut;

        public SqlInvitationRepositoryTests()
        {
            _existingInvitation = new Invitation { Id = "12", PartyId  = "ABCD" };
            var mockContext = MyDataDbContextHelper.CreateMockDbContext(invitations: new[] { _existingInvitation });
            _mockInvitationIdGenerator = new Mock<IInvitationIdGenerator>();
            _mockInvitationIdGenerator.Setup(m => m.Next()).Returns("21");
            _mockInvitationIdGenerator.Setup(m => m.Concatenate("ABCD", "12")).Returns("ABCD-12");
            _mockInvitationIdGenerator.Setup(m => m.Split("ABCD-12")).Returns(("ABCD", "12"));
            _sut = new SqlInvitationRepository(mockContext.Object, new Mock<ILogger<SqlInvitationRepository>>().Object, _mockInvitationIdGenerator.Object);
        }

        [Fact]
        public async Task AddOrUpdate_GivenEntityWithoutId_ShouldCallIdGenerator()
        {
            // ARRANGE
            var invitation = new Invitation();

            // ACT
            await _sut.AddOrUpdate(invitation);

            // ASSERT
            _mockInvitationIdGenerator.Verify(m => m.Next(), Times.Once());
        }

        [Fact]
        public async Task AddOrUpdate_GivenEntityWithId_ShouldNotCallIdGenerator()
        {
            // ARRANGE
            var updatedInvitation = new Invitation { Id = _existingInvitation.Id, PartyId = _existingInvitation.PartyId };

            // ACT
            await _sut.AddOrUpdate(updatedInvitation);

            // ASSERT
            _mockInvitationIdGenerator.Verify(m => m.Next(), Times.Never);
        }
    }
}

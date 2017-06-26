using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Pixel.FixaBarnkalaset.Infrastructure.Identity;
using Pixel.FixaBarnkalaset.Web.Controllers;
using Xunit;

namespace UnitTests.Web.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async void AddAsAdminIfNoAdminExists_GivenNoAdmins_ShouldAddUserAsAdmin()
        {
            // ARRANGE
            var user = new ApplicationUser();
            var mockUserManager = CreateMockFakeUserManager(existingAdmins: new ApplicationUser[0]);
            
            // ACT
            await AccountController.AddAsAdminIfNoAdminExists(user, mockUserManager.Object, new Mock<ILogger>().Object);

            // ASSERT
            mockUserManager.Verify(m => m.AddToRoleAsync(user, Roles.Admin), Times.Once);
        }

        [Fact]
        public async void AddAsAdminIfNoAdminExists_GivenOneAdmin_ShouldNotAddUserAsAdmin()
        {
            // ARRANGE
            var user = new ApplicationUser();
            var mockUserManager = CreateMockFakeUserManager(existingAdmins: new[] { new ApplicationUser() });

            // ACT
            await AccountController.AddAsAdminIfNoAdminExists(user, mockUserManager.Object, new Mock<ILogger>().Object);

            // ASSERT
            mockUserManager.Verify(m => m.AddToRoleAsync(user, Roles.Admin), Times.Never);
        }



        private static Mock<FakeUserManager> CreateMockFakeUserManager(IList<ApplicationUser> existingAdmins)
        {
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager
                .Setup(m => m.GetUsersInRoleAsync(Roles.Admin))
                .Returns(Task.FromResult(existingAdmins));
            return mockUserManager;
        }
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class FakeUserManager : UserManager<ApplicationUser>
    {
        public FakeUserManager()
                    : base(
                          new Mock<IUserRoleStore<ApplicationUser>>().Object,
                          new Mock<IOptions<IdentityOptions>>().Object,
                          new Mock<IPasswordHasher<ApplicationUser>>().Object,
                          new IUserValidator<ApplicationUser>[0],
                          new IPasswordValidator<ApplicationUser>[0],
                          new Mock<ILookupNormalizer>().Object,
                          new Mock<IdentityErrorDescriber>().Object,
                          new Mock<IServiceProvider>().Object,
                          new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        { }
    }
}

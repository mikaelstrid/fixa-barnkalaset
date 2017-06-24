using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Identity;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories;

namespace IntegrationTests
{
    public sealed class TestFixture<TStartup> : IDisposable
    {
        private const string SolutionName = "fixa-barnkalaset.sln";
        private readonly TestServer _server;

        public TestFixture()
            : this(Path.Combine(""))
        {
        }

        private TestFixture(string solutionRelativeTargetProjectParentDir)
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath(solutionRelativeTargetProjectParentDir, startupAssembly);

            var builder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(InitializeServices)
                .UseEnvironment("Testing")
                .UseApplicationInsights()
                .UseStartup(typeof(TStartup));

            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost");
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }

        private static void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;

            // Inject a custom application part manager. Overrides AddMvcCore() because that uses TryAdd().
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(startupAssembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            manager.FeatureProviders.Add(new ViewComponentFeatureProvider());
            services.AddSingleton(manager);

            // Define and register your own services
            services.AddTransient<SignInManager<ApplicationUser>, FakeSignInManager>();
            services.AddTransient<UserManager<ApplicationUser>, FakeUserManager>();
            //var mockFakeUserManager = CreateMockFakeUserManager();
            //services.AddSingleton(mockFakeUserManager.Object);
            services.AddTransient<ICityRepository, SqlCityRepository>();

            //var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var options = new DbContextOptionsBuilder<MyDataDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())
                     .Options;
            var context = new MyDataDbContext(options);
            context.Cities.Add(new City() {Name = "Halmstad", Slug = "halmstad", Latitude = 10.0, Longitude = 11.0});
            context.SaveChanges();
            services.AddSingleton(context);

            //services.AddDbContext<MyDataDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        }

        /// <summary>
        /// Gets the full path to the target project path that we wish to test
        /// </summary>
        /// <param name="solutionRelativePath">
        /// The parent directory of the target project.
        /// e.g. src, samples, test, or test/Websites
        /// </param>
        /// <param name="startupAssembly">The target project's assembly.</param>
        /// <returns>The full path to the target project.</returns>
        private static string GetProjectPath(string solutionRelativePath, Assembly startupAssembly)
        {
            // Get name of the target project which we want to test
            var projectName = startupAssembly.GetName().Name.Replace("Pixel.FixaBarnkalaset.", "");

            // Get currently executing test project path
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;

            // Find the folder which contains the solution file. We then use this information to find the target
            // project which we want to test.
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, SolutionName));
                if (solutionFileInfo.Exists)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
        }


        private static Mock<FakeUserManager> CreateMockFakeUserManager()
        {
            var mockUserManager = new Mock<FakeUserManager>();
            //mockUserManager
            //    .Setup(m => m.GetUsersInRoleAsync(Roles.Admin))
            //    .Returns(Task.FromResult(existingAdmins));
            return mockUserManager;
        }
    }


    // ReSharper disable once ClassNeverInstantiated.Global
    public class FakeSignInManager : SignInManager<ApplicationUser>
    {
        public FakeSignInManager()
            : base(new Mock<FakeUserManager>().Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<ApplicationUser>>>().Object
            )
        {}
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

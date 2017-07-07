using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace IntegrationTests.Utilities
{
    public sealed class TestFixture<TStartup> : IDisposable
    {
        private const string SolutionName = "fixa-barnkalaset.sln";
        private readonly TestServer _server;

        public bool IsInitialized { get; set; }

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
                .ConfigureServices(ConfigureServices)
                .UseEnvironment("Testing")
                .UseApplicationInsights()
                .UseStartup(typeof(TStartup));

            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost");
        }
        
        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }

        
        // === PUBLIC PROPERTIES

        public HttpClient Client { get; }

        public MyDataDbContext MyDataDbContext { get; private set; }


        // === CONFIG METHODS ===

        private void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesApplicationPartManager(services);
            ConfigureServicesDatabase(services);
        }

        private static void ConfigureServicesApplicationPartManager(IServiceCollection services)
        {
            // Inject a custom application part manager. Overrides AddMvcCore() because that uses TryAdd().
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(startupAssembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            manager.FeatureProviders.Add(new ViewComponentFeatureProvider());
            services.AddSingleton(manager);
        }

        private void ConfigureServicesDatabase(IServiceCollection services)
        {
            // https://stormpath.com/blog/tutorial-entity-framework-core-in-memory-database-asp-net-core
            // http://gunnarpeipman.com/2017/04/aspnet-core-ef-inmemory/
            //var httpContextAccessor = new Mock<IHttpContextAccessor>();
            MyDataDbContext = new MyDataDbContext(
                new DbContextOptionsBuilder<MyDataDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options,
                new HttpContextAccessor());
            services.AddSingleton(MyDataDbContext);
            
            var myIdentityDataDbContext = new MyIdentityDbContext(
                new DbContextOptionsBuilder<MyIdentityDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options);
            services.AddSingleton(myIdentityDataDbContext);
        }

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
    }
}

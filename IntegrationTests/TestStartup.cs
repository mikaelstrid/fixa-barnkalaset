//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
//using Pixel.FixaBarnkalaset.Web;

//namespace IntegrationTests
//{
//    // ReSharper disable once ClassNeverInstantiated.Global
//    public class TestStartup : Startup
//    {
//        public TestStartup(IHostingEnvironment env) : base(env)
//        {
//        }

//        protected override void ConfigureDatabase(IServiceCollection services)
//        {
//            //services.AddDbContext<MyDataDbContext>(options => options.UseInMemoryDatabase("MyData"));
//            //services.AddDbContext<MyIdentityDbContext>(options => options.UseInMemoryDatabase("MyIdentity"));
//            //services.AddDbContext<MyEventSourcingDbContext>(options => options.UseInMemoryDatabase("MyEventSourcing"));
//        }
//    }
//}

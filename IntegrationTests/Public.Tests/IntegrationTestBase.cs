using System.Globalization;
using System.Net.Http;
using IntegrationTests.Utilities;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web;
using Xunit;

namespace IntegrationTests.Public.Tests
{
    public abstract class IntegrationTestBase : IClassFixture<TestFixture<Startup>>
    {
        protected readonly CultureInfo SwedishCultureInfo = new CultureInfo("sv-SE");

        protected readonly TestFixture<Startup> Fixture;
        protected readonly HttpClient Client;

        protected IntegrationTestBase(TestFixture<Startup> fixture)
        {
            Fixture = fixture;
            Client = fixture.Client;
        }

        protected void PopulateDatabaseWithCities( params City[] cities)
        {
            Fixture.MyDataDbContext.Cities.AddRange(cities);
            Fixture.MyDataDbContext.SaveChanges();
        }

        protected void PopulateDatabaseWithArrangements(params Arrangement[] arrangements)
        {
            Fixture.MyDataDbContext.Arrangements.AddRange(arrangements);
            Fixture.MyDataDbContext.SaveChanges();
        }
    }
}

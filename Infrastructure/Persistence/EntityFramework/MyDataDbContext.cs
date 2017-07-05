using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pixel.FixaBarnkalaset.Core;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework
{
    // Add-Migration InitialCreate -Project Infrastructure -StartupProject Web -Context MyDataDbContext -OutputDir Persistence/EntityFramework/MyDataDbContextMigrations
    // Update-Database -Project Infrastructure -StartupProject Web -Context MyDataDbContext
    // Remove-Migration
    public class MyDataDbContext : DbContext
    {
        // Move to Startup if needed
        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public MyDataDbContext(DbContextOptions<MyDataDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}

        // https://csharp.christiannagel.com/2016/11/07/efcorefields/

        public MyDataDbContext(DbContextOptions<MyDataDbContext> options) : base(options)
        {
        }
        
        public DbSet<Arrangement> Arrangements { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<City>().Property<DateTime>("LastUpdatedUtc");
            builder.Entity<City>().Property<string>("UpdatedBy");

            builder.Entity<Arrangement>().Property<DateTime>("LastUpdatedUtc");
            builder.Entity<Arrangement>().Property<string>("UpdatedBy");
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var entries = ChangeTracker.Entries()
                .Where(e => 
                    e.State == EntityState.Added 
                    || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Property("LastUpdatedUtc").CurrentValue = DateTime.UtcNow;
                //entry.Property("UpdatedBy").CurrentValue = _httpContextAccessor.HttpContext.User.Identity.Name;
            }

            return base.SaveChanges();
        }
    }
}

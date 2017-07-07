﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        // https://csharp.christiannagel.com/2016/11/07/efcorefields/

        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyDataDbContext(DbContextOptions<MyDataDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
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
            SaveUpdatedByInformation();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveUpdatedByInformation();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SaveUpdatedByInformation()
        {
            ChangeTracker.DetectChanges();
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entry in entries)
            {
                entry.Property("LastUpdatedUtc").CurrentValue = DateTime.UtcNow;
                entry.Property("UpdatedBy").CurrentValue = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "";
            }
        }
    }
}

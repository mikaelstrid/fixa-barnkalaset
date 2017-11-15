using System;
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
    // Update-Database -Project Infrastructure -StartupProject Web -Context MyDataDbContext –TargetMigration: AddArrangementType
    // Remove-Migration -Project Infrastructure -Context MyDataDbContext
    public class MyDataDbContext : DbContext
    {
        // https://csharp.christiannagel.com/2016/11/07/efcorefields/

        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyDataDbContext(DbContextOptions<MyDataDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public virtual DbSet<Arrangement> Arrangements { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<BlogPost> BlogPosts { get; set; }
        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<InvitationCardTemplate> InvitationCardTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<City>().Property<DateTime>("LastUpdatedUtc").HasField("_lastUpdatedUtc");
            builder.Entity<City>().Property<string>("UpdatedBy").HasField("_updatedBy");

            builder.Entity<Arrangement>().Property<DateTime>("LastUpdatedUtc").HasField("_lastUpdatedUtc");
            builder.Entity<Arrangement>().Property<string>("UpdatedBy").HasField("_updatedBy");

            builder.Entity<BlogPost>().Property<DateTime>("LastUpdatedUtc").HasField("_lastUpdatedUtc");
            builder.Entity<BlogPost>().Property<string>("UpdatedBy").HasField("_updatedBy");

            builder.Entity<Party>().Property<DateTime>("LastUpdatedUtc").HasField("_lastUpdatedUtc");
            builder.Entity<Party>().Property<string>("UpdatedBy").HasField("_updatedBy");

            builder.Entity<Guest>().Property<DateTime>("LastUpdatedUtc").HasField("_lastUpdatedUtc");
            builder.Entity<Guest>().Property<string>("UpdatedBy").HasField("_updatedBy");

            builder.Entity<Invitation>().HasKey(i => new { i.PartyId, i.Id });
            builder.Entity<Invitation>().Property<DateTime>("LastUpdatedUtc").HasField("_lastUpdatedUtc");
            builder.Entity<Invitation>().Property<string>("UpdatedBy").HasField("_updatedBy");

            builder.Entity<InvitationCardTemplate>().Property<DateTime>("LastUpdatedUtc").HasField("_lastUpdatedUtc");
            builder.Entity<InvitationCardTemplate>().Property<string>("UpdatedBy").HasField("_updatedBy");

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

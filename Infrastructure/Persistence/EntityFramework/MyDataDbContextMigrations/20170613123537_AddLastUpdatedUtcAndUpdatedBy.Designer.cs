using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework.MyDataDbContextMigrations
{
    [DbContext(typeof(MyDataDbContext))]
    [Migration("20170613123537_AddLastUpdatedUtcAndUpdatedBy")]
    partial class AddLastUpdatedUtcAndUpdatedBy
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.Arrangement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CitySlug");

                    b.Property<string>("CoverImage");

                    b.Property<string>("Description");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("GooglePlacesId");

                    b.Property<DateTime>("LastUpdatedUtc");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Pitch");

                    b.Property<string>("PostalCity");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Slug")
                        .IsRequired();

                    b.Property<string>("StreetAddress");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.HasIndex("CitySlug");

                    b.ToTable("Arrangements");
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.City", b =>
                {
                    b.Property<string>("Slug")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastUpdatedUtc");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Slug");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.Arrangement", b =>
                {
                    b.HasOne("Pixel.FixaBarnkalaset.Core.City", "City")
                        .WithMany("Arrangements")
                        .HasForeignKey("CitySlug");
                });
        }
    }
}

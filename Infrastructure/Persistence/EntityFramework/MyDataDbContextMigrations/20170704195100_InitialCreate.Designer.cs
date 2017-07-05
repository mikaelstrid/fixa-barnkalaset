using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework.MyDataDbContextMigrations
{
    [DbContext(typeof(MyDataDbContext))]
    [Migration("20170704195100_InitialCreate")]
    partial class InitialCreate
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

                    b.Property<string>("CityId");

                    b.Property<int?>("CityId1");

                    b.Property<string>("CoverImage");

                    b.Property<string>("Description");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("GooglePlacesId");

                    b.Property<DateTime>("LastUpdatedUtc");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Pitch");

                    b.Property<string>("PostalCity");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Slug");

                    b.Property<string>("StreetAddress");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.HasIndex("CityId1");

                    b.ToTable("Arrangements");
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastUpdatedUtc");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("Slug");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.Arrangement", b =>
                {
                    b.HasOne("Pixel.FixaBarnkalaset.Core.City", "City")
                        .WithMany("Arrangements")
                        .HasForeignKey("CityId1");
                });
        }
    }
}

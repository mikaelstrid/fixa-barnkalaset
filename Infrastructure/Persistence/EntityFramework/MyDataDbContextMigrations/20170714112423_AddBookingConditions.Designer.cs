using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework.MyDataDbContextMigrations
{
    [DbContext(typeof(MyDataDbContext))]
    [Migration("20170714112423_AddBookingConditions")]
    partial class AddBookingConditions
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

                    b.Property<string>("BookingConditions");

                    b.Property<int>("CityId");

                    b.Property<string>("CoverImage");

                    b.Property<string>("CoverImageAttributions");

                    b.Property<string>("Description");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("GooglePlacesId");

                    b.Property<bool>("IsRemoved");

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

                    b.HasIndex("CityId");

                    b.ToTable("Arrangements");
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsRemoved");

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
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

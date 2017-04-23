using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pixel.Kidsparties.Infrastructure;

namespace Pixel.Kidsparties.Infrastructure.Migrations
{
    [DbContext(typeof(KidsPartiesContext))]
    [Migration("20170423153046_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pixel.Kidsparties.Core.Arrangement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CitySlug");

                    b.Property<string>("CoverImage");

                    b.Property<string>("Description");

                    b.Property<string>("EmailAddress");

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

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.HasIndex("CitySlug");

                    b.ToTable("Arrangements");
                });

            modelBuilder.Entity("Pixel.Kidsparties.Core.City", b =>
                {
                    b.Property<string>("Slug")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Slug");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Pixel.Kidsparties.Core.Arrangement", b =>
                {
                    b.HasOne("Pixel.Kidsparties.Core.City", "City")
                        .WithMany("Arrangements")
                        .HasForeignKey("CitySlug");
                });
        }
    }
}

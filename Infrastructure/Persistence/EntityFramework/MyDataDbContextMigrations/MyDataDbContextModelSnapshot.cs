﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;
using System;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework.MyDataDbContextMigrations
{
    [DbContext(typeof(MyDataDbContext))]
    partial class MyDataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
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

                    b.Property<string>("PriceInformation");

                    b.Property<string>("Slug");

                    b.Property<string>("StreetAddress");

                    b.Property<string>("Type");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Arrangements");
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<bool>("IsPublished");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTime>("LastUpdatedUtc");

                    b.Property<DateTime?>("PublishedUtc");

                    b.Property<string>("Slug");

                    b.Property<string>("Title");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("BlogPosts");
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

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastUpdatedUtc");

                    b.Property<string>("PostalCity");

                    b.Property<string>("PostalCode");

                    b.Property<string>("StreetAddress");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.Invitation", b =>
                {
                    b.Property<string>("PartyId");

                    b.Property<string>("Id");

                    b.Property<int>("GuestId");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTime>("LastUpdatedUtc");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("PartyId", "Id");

                    b.HasIndex("GuestId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.InvitationCardTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HtmlTemplateText");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTime>("LastUpdatedUtc");

                    b.Property<string>("PreviewUrl");

                    b.Property<string>("TemplateUrl");

                    b.Property<string>("ThumbnailUrl");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("InvitationCardTemplates");
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.Party", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("EndTime");

                    b.Property<int?>("InvitationCardTemplateId");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTime>("LastUpdatedUtc");

                    b.Property<string>("LocationName");

                    b.Property<string>("NameOfBirthdayChild");

                    b.Property<string>("PartyType");

                    b.Property<string>("PostalCity");

                    b.Property<string>("PostalCode");

                    b.Property<DateTime?>("RsvpDate");

                    b.Property<string>("RsvpEmail");

                    b.Property<string>("RsvpPhoneNumber");

                    b.Property<DateTime?>("StartTime");

                    b.Property<string>("StreetAddress");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.HasIndex("InvitationCardTemplateId");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.Arrangement", b =>
                {
                    b.HasOne("Pixel.FixaBarnkalaset.Core.City", "City")
                        .WithMany("Arrangements")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.Invitation", b =>
                {
                    b.HasOne("Pixel.FixaBarnkalaset.Core.Guest", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pixel.FixaBarnkalaset.Core.Party", "Party")
                        .WithMany("Invitations")
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Core.Party", b =>
                {
                    b.HasOne("Pixel.FixaBarnkalaset.Core.InvitationCardTemplate", "InvitationCardTemplate")
                        .WithMany()
                        .HasForeignKey("InvitationCardTemplateId");
                });
#pragma warning restore 612, 618
        }
    }
}

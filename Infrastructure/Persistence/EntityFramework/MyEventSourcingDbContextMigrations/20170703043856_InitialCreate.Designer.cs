using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework.MyEventSourcingDbContextMigrations
{
    [DbContext(typeof(MyEventSourcingDbContext))]
    [Migration("20170703043856_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pixel.FixaBarnkalaset.Infrastructure.Persistence.EventData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AggregateId");

                    b.Property<string>("AggregateType");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Event");

                    b.Property<string>("Metadata");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });
        }
    }
}

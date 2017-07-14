using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework.MyDataDbContextMigrations
{
    public partial class AddBookingConditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingConditions",
                table: "Arrangements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingConditions",
                table: "Arrangements");
        }
    }
}

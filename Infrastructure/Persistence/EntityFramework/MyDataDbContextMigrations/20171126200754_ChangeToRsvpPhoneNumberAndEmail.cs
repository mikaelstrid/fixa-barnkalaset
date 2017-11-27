using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework.MyDataDbContextMigrations
{
    public partial class ChangeToRsvpPhoneNumberAndEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RsvpDescription",
                table: "Parties");

            migrationBuilder.AddColumn<string>(
                name: "RsvpEmail",
                table: "Parties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RsvpPhoneNumber",
                table: "Parties",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RsvpEmail",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "RsvpPhoneNumber",
                table: "Parties");

            migrationBuilder.AddColumn<string>(
                name: "RsvpDescription",
                table: "Parties",
                nullable: true);
        }
    }
}

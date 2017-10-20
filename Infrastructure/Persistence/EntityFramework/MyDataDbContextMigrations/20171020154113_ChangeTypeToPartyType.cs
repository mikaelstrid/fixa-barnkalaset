using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework.MyDataDbContextMigrations
{
    public partial class ChangeTypeToPartyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Parties");

            migrationBuilder.AddColumn<string>(
                name: "PartyType",
                table: "Parties",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyType",
                table: "Parties");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Parties",
                nullable: true);
        }
    }
}

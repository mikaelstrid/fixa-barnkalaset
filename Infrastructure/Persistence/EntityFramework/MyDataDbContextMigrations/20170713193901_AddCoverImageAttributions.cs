using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework.MyDataDbContextMigrations
{
    public partial class AddCoverImageAttributions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageAttributions",
                table: "Arrangements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageAttributions",
                table: "Arrangements");
        }
    }
}

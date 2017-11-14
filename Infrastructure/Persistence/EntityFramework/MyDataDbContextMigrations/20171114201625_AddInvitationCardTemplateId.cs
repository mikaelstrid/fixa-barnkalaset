using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework.MyDataDbContextMigrations
{
    public partial class AddInvitationCardTemplateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvitationCardTemplateId",
                table: "Parties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_InvitationCardTemplateId",
                table: "Parties",
                column: "InvitationCardTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_InvitationCardTemplates_InvitationCardTemplateId",
                table: "Parties",
                column: "InvitationCardTemplateId",
                principalTable: "InvitationCardTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_InvitationCardTemplates_InvitationCardTemplateId",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_InvitationCardTemplateId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "InvitationCardTemplateId",
                table: "Parties");
        }
    }
}

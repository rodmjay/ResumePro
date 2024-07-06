using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicationDate",
                table: "Application",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ApplicationStatus",
                table: "Application",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "Application",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Application_OrganizationId_ResumeId",
                table: "Application",
                columns: new[] { "OrganizationId", "ResumeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Resume_OrganizationId_ResumeId",
                table: "Application",
                columns: new[] { "OrganizationId", "ResumeId" },
                principalTable: "Resume",
                principalColumns: new[] { "OrganizationId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Resume_OrganizationId_ResumeId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_OrganizationId_ResumeId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ApplicationDate",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ApplicationStatus",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Application");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reference_Job_OrganizationId_JobId",
                table: "Reference");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "Reference",
                newName: "PersonaId");

            migrationBuilder.RenameIndex(
                name: "IX_Reference_OrganizationId_JobId",
                table: "Reference",
                newName: "IX_Reference_OrganizationId_PersonaId");

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 2, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 5, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 6, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 7, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 8, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 9, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Reference_Persona_OrganizationId_PersonaId",
                table: "Reference",
                columns: new[] { "OrganizationId", "PersonaId" },
                principalTable: "Persona",
                principalColumns: new[] { "OrganizationId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reference_Persona_OrganizationId_PersonaId",
                table: "Reference");

            migrationBuilder.RenameColumn(
                name: "PersonaId",
                table: "Reference",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Reference_OrganizationId_PersonaId",
                table: "Reference",
                newName: "IX_Reference_OrganizationId_JobId");

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 2, 1 },
                column: "JobId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 5, 1 },
                column: "JobId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 6, 1 },
                column: "JobId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 7, 1 },
                column: "JobId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 8, 1 },
                column: "JobId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 9, 1 },
                column: "JobId",
                value: 7);

            migrationBuilder.AddForeignKey(
                name: "FK_Reference_Job_OrganizationId_JobId",
                table: "Reference",
                columns: new[] { "OrganizationId", "JobId" },
                principalTable: "Job",
                principalColumns: new[] { "OrganizationId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}

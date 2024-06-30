using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "ResumeJob");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Highlight",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => new { x.Id, x.JobId });
                    table.ForeignKey(
                        name: "FK_Project_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 5,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 6,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 7,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 8,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 9,
                column: "ProjectId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Highlight_ProjectId_JobId",
                table: "Highlight",
                columns: new[] { "ProjectId", "JobId" });

            migrationBuilder.CreateIndex(
                name: "IX_Project_JobId",
                table: "Project",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Highlight_Project_ProjectId_JobId",
                table: "Highlight",
                columns: new[] { "ProjectId", "JobId" },
                principalTable: "Project",
                principalColumns: new[] { "Id", "JobId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Highlight_Project_ProjectId_JobId",
                table: "Highlight");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Highlight_ProjectId_JobId",
                table: "Highlight");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Highlight");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ResumeJob",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ResumeJob",
                keyColumns: new[] { "JobId", "PersonaId", "ResumeId" },
                keyValues: new object[] { 1, 1, 1 },
                column: "Order",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ResumeJob",
                keyColumns: new[] { "JobId", "PersonaId", "ResumeId" },
                keyValues: new object[] { 2, 1, 1 },
                column: "Order",
                value: 0);
        }
    }
}

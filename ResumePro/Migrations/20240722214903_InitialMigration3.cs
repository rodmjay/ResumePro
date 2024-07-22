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
            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_Job_OrganizationId_JobId",
                table: "JobSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_PersonaSkill_OrganizationId_PersonaId_SkillId",
                table: "JobSkill");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_Job_OrganizationId_JobId",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "JobId" },
                principalTable: "Job",
                principalColumns: new[] { "OrganizationId", "Id" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_PersonaSkill_OrganizationId_PersonaId_SkillId",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "PersonaId", "SkillId" },
                principalTable: "PersonaSkill",
                principalColumns: new[] { "OrganizationId", "PersonaId", "SkillId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_Job_OrganizationId_JobId",
                table: "JobSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_PersonaSkill_OrganizationId_PersonaId_SkillId",
                table: "JobSkill");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_Job_OrganizationId_JobId",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "JobId" },
                principalTable: "Job",
                principalColumns: new[] { "OrganizationId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_PersonaSkill_OrganizationId_PersonaId_SkillId",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "PersonaId", "SkillId" },
                principalTable: "PersonaSkill",
                principalColumns: new[] { "OrganizationId", "PersonaId", "SkillId" });
        }
    }
}

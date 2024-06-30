using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GitHub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Resume",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resume", x => x.Id);
                    table.UniqueConstraint("AK_Resume_PersonaId_Id", x => new { x.PersonaId, x.Id });
                    table.ForeignKey(
                        name: "FK_Resume_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonaSkill",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaSkill", x => new { x.PersonaId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_PersonaSkill_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Highlight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highlight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Highlight_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reference_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeJob",
                columns: table => new
                {
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeJob", x => new { x.PersonaId, x.ResumeId, x.JobId });
                    table.UniqueConstraint("AK_ResumeJob_ResumeId_JobId", x => new { x.ResumeId, x.JobId });
                    table.ForeignKey(
                        name: "FK_ResumeJob_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResumeJob_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumeJob_Resume_PersonaId_ResumeId",
                        columns: x => new { x.PersonaId, x.ResumeId },
                        principalTable: "Resume",
                        principalColumns: new[] { "PersonaId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "ResumeSkill",
                columns: table => new
                {
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSkill", x => new { x.PersonaId, x.ResumeId, x.SkillId });
                    table.UniqueConstraint("AK_ResumeSkill_ResumeId_SkillId", x => new { x.ResumeId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_ResumeSkill_PersonaSkill_PersonaId_SkillId",
                        columns: x => new { x.PersonaId, x.SkillId },
                        principalTable: "PersonaSkill",
                        principalColumns: new[] { "PersonaId", "SkillId" });
                    table.ForeignKey(
                        name: "FK_ResumeSkill_Resume_PersonaId_ResumeId",
                        columns: x => new { x.PersonaId, x.ResumeId },
                        principalTable: "Resume",
                        principalColumns: new[] { "PersonaId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "JobSkill",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkill", x => new { x.SkillId, x.JobId });
                    table.ForeignKey(
                        name: "FK_JobSkill_ResumeJob_ResumeId_JobId",
                        columns: x => new { x.ResumeId, x.JobId },
                        principalTable: "ResumeJob",
                        principalColumns: new[] { "ResumeId", "JobId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkill_ResumeSkill_ResumeId_SkillId",
                        columns: x => new { x.ResumeId, x.SkillId },
                        principalTable: "ResumeSkill",
                        principalColumns: new[] { "ResumeId", "SkillId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persona",
                columns: new[] { "Id", "City", "Email", "FirstName", "GitHub", "LastName", "LinkedIn", "PhoneNumber", "State" },
                values: new object[] { 1, "Salt Lake City", "rodmjay@gmail.com", "Rod", "https://www.linkedin.com/in/rodmjay", "Johnson", "https://www.github.com/rodmjay", "3853526026", "UT" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "C# Development" },
                    { 2, "Azure" },
                    { 3, "Sql Server" },
                    { 4, "Angular" }
                });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "Id", "Company", "Description", "EndDate", "Location", "PersonaId", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, "X Co", "Test Description", null, "Salt Lake City", 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect" },
                    { 2, "Y Co", "Test Description", null, "Salt Lake City", 1, new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect" }
                });

            migrationBuilder.InsertData(
                table: "PersonaSkill",
                columns: new[] { "PersonaId", "SkillId", "Rating" },
                values: new object[] { 1, 1, 10 });

            migrationBuilder.InsertData(
                table: "Resume",
                columns: new[] { "Id", "Description", "PersonaId", "ShortName" },
                values: new object[] { 1, "Hello There", 1, "C# Developer" });

            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "JobId", "Order", "Text" },
                values: new object[] { 1, 1, 1, "Hello" });

            migrationBuilder.InsertData(
                table: "ResumeJob",
                columns: new[] { "JobId", "PersonaId", "ResumeId", "Order" },
                values: new object[,]
                {
                    { 1, 1, 1, 0 },
                    { 2, 1, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "PersonaId", "ResumeId", "SkillId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "JobSkill",
                columns: new[] { "JobId", "SkillId", "ResumeId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Highlight_JobId",
                table: "Highlight",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_PersonaId",
                table: "Job",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_ResumeId_JobId",
                table: "JobSkill",
                columns: new[] { "ResumeId", "JobId" });

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_ResumeId_SkillId",
                table: "JobSkill",
                columns: new[] { "ResumeId", "SkillId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonaSkill_SkillId",
                table: "PersonaSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_JobId",
                table: "Reference",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeJob_JobId",
                table: "ResumeJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkill_PersonaId_SkillId",
                table: "ResumeSkill",
                columns: new[] { "PersonaId", "SkillId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Highlight");

            migrationBuilder.DropTable(
                name: "JobSkill");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropTable(
                name: "ResumeJob");

            migrationBuilder.DropTable(
                name: "ResumeSkill");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "PersonaSkill");

            migrationBuilder.DropTable(
                name: "Resume");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}

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
                name: "Country",
                columns: table => new
                {
                    Iso2 = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CapsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iso3 = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    NumberCode = table.Column<int>(type: "int", nullable: true),
                    PhoneCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Iso2);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Code3 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NativeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Code3);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationSettings",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeYearHistory = table.Column<int>(type: "int", nullable: false),
                    AttachAllJobs = table.Column<bool>(type: "bit", nullable: false),
                    AttachAllSkills = table.Column<bool>(type: "bit", nullable: false),
                    DefaultTemplateId = table.Column<int>(type: "int", nullable: false),
                    ShowTechnologyPerJob = table.Column<bool>(type: "bit", nullable: false),
                    ShowDuration = table.Column<bool>(type: "bit", nullable: false),
                    ShowContactInfo = table.Column<bool>(type: "bit", nullable: false),
                    SkillView = table.Column<int>(type: "int", nullable: false),
                    ShowRatings = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationSettings", x => x.OrganizationId);
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
                name: "SkillCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Engine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateProvince",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iso2 = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbrev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateProvince", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateProvince_Country_Iso2",
                        column: x => x.Iso2,
                        principalTable: "Country",
                        principalColumn: "Iso2");
                });

            migrationBuilder.CreateTable(
                name: "SkillCategorySkill",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    SkillCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCategorySkill", x => new { x.SkillCategoryId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_SkillCategorySkill_SkillCategory_SkillCategoryId",
                        column: x => x.SkillCategoryId,
                        principalTable: "SkillCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillCategorySkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GitHub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => new { x.OrganizationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Persona_StateProvince_StateId",
                        column: x => x.StateId,
                        principalTable: "StateProvince",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => new { x.OrganizationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Certification_Persona_OrganizationId_PersonaId",
                        columns: x => new { x.OrganizationId, x.PersonaId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => new { x.OrganizationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Job_Persona_OrganizationId_PersonaId",
                        columns: x => new { x.OrganizationId, x.PersonaId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "PersonaLanguage",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Code3 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Proficiency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaLanguage", x => new { x.OrganizationId, x.PersonaId, x.Code3 });
                    table.ForeignKey(
                        name: "FK_PersonaLanguage_Language_Code3",
                        column: x => x.Code3,
                        principalTable: "Language",
                        principalColumn: "Code3",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaLanguage_Persona_OrganizationId_PersonaId",
                        columns: x => new { x.OrganizationId, x.PersonaId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "PersonaSkill",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaSkill", x => new { x.OrganizationId, x.PersonaId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_PersonaSkill_Persona_OrganizationId_PersonaId",
                        columns: x => new { x.OrganizationId, x.PersonaId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                    table.ForeignKey(
                        name: "FK_PersonaSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reference",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference", x => new { x.OrganizationId, x.PersonaId, x.Id });
                    table.ForeignKey(
                        name: "FK_Reference_Persona_OrganizationId_PersonaId",
                        columns: x => new { x.OrganizationId, x.PersonaId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resume",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resume", x => new { x.OrganizationId, x.Id });
                    table.UniqueConstraint("AK_Resume_OrganizationId_PersonaId_Id", x => new { x.OrganizationId, x.PersonaId, x.Id });
                    table.ForeignKey(
                        name: "FK_Resume_Persona_OrganizationId_PersonaId",
                        columns: x => new { x.OrganizationId, x.PersonaId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => new { x.OrganizationId, x.Id });
                    table.ForeignKey(
                        name: "FK_School_Persona_OrganizationId_PersonaId",
                        columns: x => new { x.OrganizationId, x.PersonaId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => new { x.OrganizationId, x.Id, x.JobId });
                    table.UniqueConstraint("AK_Project_Id_JobId", x => new { x.Id, x.JobId });
                    table.ForeignKey(
                        name: "FK_Project_Job_OrganizationId_JobId",
                        columns: x => new { x.OrganizationId, x.JobId },
                        principalTable: "Job",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkill",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkill", x => new { x.OrganizationId, x.PersonaId, x.JobId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_JobSkill_Job_OrganizationId_JobId",
                        columns: x => new { x.OrganizationId, x.JobId },
                        principalTable: "Job",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkill_PersonaSkill_OrganizationId_PersonaId_SkillId",
                        columns: x => new { x.OrganizationId, x.PersonaId, x.SkillId },
                        principalTable: "PersonaSkill",
                        principalColumns: new[] { "OrganizationId", "PersonaId", "SkillId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rendering",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    RenderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rendering", x => new { x.OrganizationId, x.ResumeId, x.TemplateId });
                    table.ForeignKey(
                        name: "FK_Rendering_Resume_OrganizationId_ResumeId",
                        columns: x => new { x.OrganizationId, x.ResumeId },
                        principalTable: "Resume",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rendering_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeJob",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeJob", x => new { x.OrganizationId, x.ResumeId, x.JobId });
                    table.ForeignKey(
                        name: "FK_ResumeJob_Job_OrganizationId_JobId",
                        columns: x => new { x.OrganizationId, x.JobId },
                        principalTable: "Job",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumeJob_Resume_OrganizationId_ResumeId",
                        columns: x => new { x.OrganizationId, x.ResumeId },
                        principalTable: "Resume",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeSettings",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    AttachAllJobs = table.Column<bool>(type: "bit", nullable: true),
                    AttachAllSkills = table.Column<bool>(type: "bit", nullable: true),
                    ResumeYearHistory = table.Column<int>(type: "int", nullable: true),
                    DefaultTemplateId = table.Column<int>(type: "int", nullable: true),
                    ShowTechnologyPerJob = table.Column<bool>(type: "bit", nullable: true),
                    ShowDuration = table.Column<bool>(type: "bit", nullable: true),
                    ShowContactInfo = table.Column<bool>(type: "bit", nullable: true),
                    SkillView = table.Column<int>(type: "int", nullable: true),
                    ShowRatings = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSettings", x => new { x.OrganizationId, x.ResumeId });
                    table.ForeignKey(
                        name: "FK_ResumeSettings_OrganizationSettings_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationSettings",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumeSettings_Resume_OrganizationId_ResumeId",
                        columns: x => new { x.OrganizationId, x.ResumeId },
                        principalTable: "Resume",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumeSettings_Template_DefaultTemplateId",
                        column: x => x.DefaultTemplateId,
                        principalTable: "Template",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResumeSkill",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSkill", x => new { x.OrganizationId, x.PersonaId, x.ResumeId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_ResumeSkill_PersonaSkill_OrganizationId_PersonaId_SkillId",
                        columns: x => new { x.OrganizationId, x.PersonaId, x.SkillId },
                        principalTable: "PersonaSkill",
                        principalColumns: new[] { "OrganizationId", "PersonaId", "SkillId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumeSkill_Resume_OrganizationId_PersonaId_ResumeId",
                        columns: x => new { x.OrganizationId, x.PersonaId, x.ResumeId },
                        principalTable: "Resume",
                        principalColumns: new[] { "OrganizationId", "PersonaId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Degree",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degree", x => new { x.OrganizationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Degree_School_OrganizationId_SchoolId",
                        columns: x => new { x.OrganizationId, x.SchoolId },
                        principalTable: "School",
                        principalColumns: new[] { "OrganizationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Highlight",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highlight", x => new { x.OrganizationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Highlight_Job_OrganizationId_JobId",
                        columns: x => new { x.OrganizationId, x.JobId },
                        principalTable: "Job",
                        principalColumns: new[] { "OrganizationId", "Id" });
                    table.ForeignKey(
                        name: "FK_Highlight_Project_ProjectId_JobId",
                        columns: x => new { x.ProjectId, x.JobId },
                        principalTable: "Project",
                        principalColumns: new[] { "Id", "JobId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Iso2", "CapsName", "Iso3", "Name", "NumberCode", "PhoneCode" },
                values: new object[,]
                {
                    { "AD", "ANDORRA", "AND", "Andorra", 20, 376 },
                    { "AE", "UNITED ARAB EMIRATES", "ARE", "United Arab Emirates", 784, 971 },
                    { "AF", "AFGHANISTAN", "AFG", "Afghanistan", 4, 93 },
                    { "AG", "ANTIGUA AND BARBUDA", "ATG", "Antigua and Barbuda", 28, 1268 },
                    { "AI", "ANGUILLA", "AIA", "Anguilla", 660, 1264 },
                    { "AL", "ALBANIA", "ALB", "Albania", 8, 355 },
                    { "AM", "ARMENIA", "ARM", "Armenia", 51, 374 },
                    { "AN", "NETHERLANDS ANTILLES", "ANT", "Netherlands Antilles", 530, 599 },
                    { "AO", "ANGOLA", "AGO", "Angola", 24, 244 },
                    { "AQ", "ANTARCTICA", "", "Antarctica", null, 0 },
                    { "AR", "ARGENTINA", "ARG", "Argentina", 32, 54 },
                    { "AS", "AMERICAN SAMOA", "ASM", "American Samoa", 16, 1684 },
                    { "AT", "AUSTRIA", "AUT", "Austria", 40, 43 },
                    { "AU", "AUSTRALIA", "AUS", "Australia", 36, 61 },
                    { "AW", "ARUBA", "ABW", "Aruba", 533, 297 },
                    { "AZ", "AZERBAIJAN", "AZE", "Azerbaijan", 31, 994 },
                    { "BA", "BOSNIA AND HERZEGOVINA", "BIH", "Bosnia and Herzegovina", 70, 387 },
                    { "BB", "BARBADOS", "BRB", "Barbados", 52, 1246 },
                    { "BD", "BANGLADESH", "BGD", "Bangladesh", 50, 880 },
                    { "BE", "BELGIUM", "BEL", "Belgium", 56, 32 },
                    { "BF", "BURKINA FASO", "BFA", "Burkina Faso", 854, 226 },
                    { "BG", "BULGARIA", "BGR", "Bulgaria", 100, 359 },
                    { "BH", "BAHRAIN", "BHR", "Bahrain", 48, 973 },
                    { "BI", "BURUNDI", "BDI", "Burundi", 108, 257 },
                    { "BJ", "BENIN", "BEN", "Benin", 204, 229 },
                    { "BM", "BERMUDA", "BMU", "Bermuda", 60, 1441 },
                    { "BN", "BRUNEI DARUSSALAM", "BRN", "Brunei Darussalam", 96, 673 },
                    { "BO", "BOLIVIA", "BOL", "Bolivia", 68, 591 },
                    { "BR", "BRAZIL", "BRA", "Brazil", 76, 55 },
                    { "BS", "BAHAMAS", "BHS", "Bahamas", 44, 1242 },
                    { "BT", "BHUTAN", "BTN", "Bhutan", 64, 975 },
                    { "BV", "BOUVET ISLAND", "", "Bouvet Island", null, 0 },
                    { "BW", "BOTSWANA", "BWA", "Botswana", 72, 267 },
                    { "BY", "BELARUS", "BLR", "Belarus", 112, 375 },
                    { "BZ", "BELIZE", "BLZ", "Belize", 84, 501 },
                    { "CA", "CANADA", "CAN", "Canada", 124, 1 },
                    { "CC", "COCOS (KEELING) ISLANDS", "", "Cocos (Keeling) Islands", null, 672 },
                    { "CD", "CONGO, THE DEMOCRATIC REPUBLIC OF THE", "COD", "Congo, the Democratic Republic of the", 180, 242 },
                    { "CF", "CENTRAL AFRICAN REPUBLIC", "CAF", "Central African Republic", 140, 236 },
                    { "CG", "CONGO", "COG", "Congo", 178, 242 },
                    { "CH", "SWITZERLAND", "CHE", "Switzerland", 756, 41 },
                    { "CI", "COTE D'IVOIRE", "CIV", "Cote D'Ivoire", 384, 225 },
                    { "CK", "COOK ISLANDS", "COK", "Cook Islands", 184, 682 },
                    { "CL", "CHILE", "CHL", "Chile", 152, 56 },
                    { "CM", "CAMEROON", "CMR", "Cameroon", 120, 237 },
                    { "CN", "CHINA", "CHN", "China", 156, 86 },
                    { "CO", "COLOMBIA", "COL", "Colombia", 170, 57 },
                    { "CR", "COSTA RICA", "CRI", "Costa Rica", 188, 506 },
                    { "CS", "SERBIA AND MONTENEGRO", "", "Serbia and Montenegro", null, 381 },
                    { "CU", "CUBA", "CUB", "Cuba", 192, 53 },
                    { "CV", "CAPE VERDE", "CPV", "Cape Verde", 132, 238 },
                    { "CX", "CHRISTMAS ISLAND", "", "Christmas Island", null, 61 },
                    { "CY", "CYPRUS", "CYP", "Cyprus", 196, 357 },
                    { "CZ", "CZECH REPUBLIC", "CZE", "Czech Republic", 203, 420 },
                    { "DE", "GERMANY", "DEU", "Germany", 276, 49 },
                    { "DJ", "DJIBOUTI", "DJI", "Djibouti", 262, 253 },
                    { "DK", "DENMARK", "DNK", "Denmark", 208, 45 },
                    { "DM", "DOMINICA", "DMA", "Dominica", 212, 1767 },
                    { "DO", "DOMINICAN REPUBLIC", "DOM", "Dominican Republic", 214, 1809 },
                    { "DZ", "ALGERIA", "DZA", "Algeria", 12, 213 },
                    { "EC", "ECUADOR", "ECU", "Ecuador", 218, 593 },
                    { "EE", "ESTONIA", "EST", "Estonia", 233, 372 },
                    { "EG", "EGYPT", "EGY", "Egypt", 818, 20 },
                    { "EH", "WESTERN SAHARA", "ESH", "Western Sahara", 732, 212 },
                    { "ER", "ERITREA", "ERI", "Eritrea", 232, 291 },
                    { "ES", "SPAIN", "ESP", "Spain", 724, 34 },
                    { "ET", "ETHIOPIA", "ETH", "Ethiopia", 231, 251 },
                    { "FI", "FINLAND", "FIN", "Finland", 246, 358 },
                    { "FJ", "FIJI", "FJI", "Fiji", 242, 679 },
                    { "FK", "FALKLAND ISLANDS (MALVINAS)", "FLK", "Falkland Islands (Malvinas)", 238, 500 },
                    { "FM", "MICRONESIA, FEDERATED STATES OF", "FSM", "Micronesia, Federated States of", 583, 691 },
                    { "FO", "FAROE ISLANDS", "FRO", "Faroe Islands", 234, 298 },
                    { "FR", "FRANCE", "FRA", "France", 250, 33 },
                    { "GA", "GABON", "GAB", "Gabon", 266, 241 },
                    { "GB", "UNITED KINGDOM", "GBR", "United Kingdom", 826, 44 },
                    { "GD", "GRENADA", "GRD", "Grenada", 308, 1473 },
                    { "GE", "GEORGIA", "GEO", "Georgia", 268, 995 },
                    { "GF", "FRENCH GUIANA", "GUF", "French Guiana", 254, 594 },
                    { "GH", "GHANA", "GHA", "Ghana", 288, 233 },
                    { "GI", "GIBRALTAR", "GIB", "Gibraltar", 292, 350 },
                    { "GL", "GREENLAND", "GRL", "Greenland", 304, 299 },
                    { "GM", "GAMBIA", "GMB", "Gambia", 270, 220 },
                    { "GN", "GUINEA", "GIN", "Guinea", 324, 224 },
                    { "GP", "GUADELOUPE", "GLP", "Guadeloupe", 312, 590 },
                    { "GQ", "EQUATORIAL GUINEA", "GNQ", "Equatorial Guinea", 226, 240 },
                    { "GR", "GREECE", "GRC", "Greece", 300, 30 },
                    { "GS", "SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS", "", "South Georgia and the South Sandwich Islands", null, 0 },
                    { "GT", "GUATEMALA", "GTM", "Guatemala", 320, 502 },
                    { "GU", "GUAM", "GUM", "Guam", 316, 1671 },
                    { "GW", "GUINEA-BISSAU", "GNB", "Guinea-Bissau", 624, 245 },
                    { "GY", "GUYANA", "GUY", "Guyana", 328, 592 },
                    { "HK", "HONG KONG", "HKG", "Hong Kong", 344, 852 },
                    { "HM", "HEARD ISLAND AND MCDONALD ISLANDS", "", "Heard Island and Mcdonald Islands", null, 0 },
                    { "HN", "HONDURAS", "HND", "Honduras", 340, 504 },
                    { "HR", "CROATIA", "HRV", "Croatia", 191, 385 },
                    { "HT", "HAITI", "HTI", "Haiti", 332, 509 },
                    { "HU", "HUNGARY", "HUN", "Hungary", 348, 36 },
                    { "ID", "INDONESIA", "IDN", "Indonesia", 360, 62 },
                    { "IE", "IRELAND", "IRL", "Ireland", 372, 353 },
                    { "IL", "ISRAEL", "ISR", "Israel", 376, 972 },
                    { "IN", "INDIA", "IND", "India", 356, 91 },
                    { "IO", "BRITISH INDIAN OCEAN TERRITORY", "", "British Indian Ocean Territory", null, 246 },
                    { "IQ", "IRAQ", "IRQ", "Iraq", 368, 964 },
                    { "IR", "IRAN, ISLAMIC REPUBLIC OF", "IRN", "Iran, Islamic Republic of", 364, 98 },
                    { "IS", "ICELAND", "ISL", "Iceland", 352, 354 },
                    { "IT", "ITALY", "ITA", "Italy", 380, 39 },
                    { "JM", "JAMAICA", "JAM", "Jamaica", 388, 1876 },
                    { "JO", "JORDAN", "JOR", "Jordan", 400, 962 },
                    { "JP", "JAPAN", "JPN", "Japan", 392, 81 },
                    { "KE", "KENYA", "KEN", "Kenya", 404, 254 },
                    { "KG", "KYRGYZSTAN", "KGZ", "Kyrgyzstan", 417, 996 },
                    { "KH", "CAMBODIA", "KHM", "Cambodia", 116, 855 },
                    { "KI", "KIRIBATI", "KIR", "Kiribati", 296, 686 },
                    { "KM", "COMOROS", "COM", "Comoros", 174, 269 },
                    { "KN", "SAINT KITTS AND NEVIS", "KNA", "Saint Kitts and Nevis", 659, 1869 },
                    { "KP", "KOREA, DEMOCRATIC PEOPLE'S REPUBLIC OF", "PRK", "Korea, Democratic People's Republic of", 408, 850 },
                    { "KR", "KOREA, REPUBLIC OF", "KOR", "Korea, Republic of", 410, 82 },
                    { "KW", "KUWAIT", "KWT", "Kuwait", 414, 965 },
                    { "KY", "CAYMAN ISLANDS", "CYM", "Cayman Islands", 136, 1345 },
                    { "KZ", "KAZAKHSTAN", "KAZ", "Kazakhstan", 398, 7 },
                    { "LA", "LAO PEOPLE'S DEMOCRATIC REPUBLIC", "LAO", "Lao People's Democratic Republic", 418, 856 },
                    { "LB", "LEBANON", "LBN", "Lebanon", 422, 961 },
                    { "LC", "SAINT LUCIA", "LCA", "Saint Lucia", 662, 1758 },
                    { "LI", "LIECHTENSTEIN", "LIE", "Liechtenstein", 438, 423 },
                    { "LK", "SRI LANKA", "LKA", "Sri Lanka", 144, 94 },
                    { "LR", "LIBERIA", "LBR", "Liberia", 430, 231 },
                    { "LS", "LESOTHO", "LSO", "Lesotho", 426, 266 },
                    { "LT", "LITHUANIA", "LTU", "Lithuania", 440, 370 },
                    { "LU", "LUXEMBOURG", "LUX", "Luxembourg", 442, 352 },
                    { "LV", "LATVIA", "LVA", "Latvia", 428, 371 },
                    { "LY", "LIBYAN ARAB JAMAHIRIYA", "LBY", "Libyan Arab Jamahiriya", 434, 218 },
                    { "MA", "MOROCCO", "MAR", "Morocco", 504, 212 },
                    { "MC", "MONACO", "MCO", "Monaco", 492, 377 },
                    { "MD", "MOLDOVA, REPUBLIC OF", "MDA", "Moldova, Republic of", 498, 373 },
                    { "MG", "MADAGASCAR", "MDG", "Madagascar", 450, 261 },
                    { "MH", "MARSHALL ISLANDS", "MHL", "Marshall Islands", 584, 692 },
                    { "MK", "MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF", "MKD", "Macedonia, the Former Yugoslav Republic of", 807, 389 },
                    { "ML", "MALI", "MLI", "Mali", 466, 223 },
                    { "MM", "MYANMAR", "MMR", "Myanmar", 104, 95 },
                    { "MN", "MONGOLIA", "MNG", "Mongolia", 496, 976 },
                    { "MO", "MACAO", "MAC", "Macao", 446, 853 },
                    { "MP", "NORTHERN MARIANA ISLANDS", "MNP", "Northern Mariana Islands", 580, 1670 },
                    { "MQ", "MARTINIQUE", "MTQ", "Martinique", 474, 596 },
                    { "MR", "MAURITANIA", "MRT", "Mauritania", 478, 222 },
                    { "MS", "MONTSERRAT", "MSR", "Montserrat", 500, 1664 },
                    { "MT", "MALTA", "MLT", "Malta", 470, 356 },
                    { "MU", "MAURITIUS", "MUS", "Mauritius", 480, 230 },
                    { "MV", "MALDIVES", "MDV", "Maldives", 462, 960 },
                    { "MW", "MALAWI", "MWI", "Malawi", 454, 265 },
                    { "MX", "MEXICO", "MEX", "Mexico", 484, 52 },
                    { "MY", "MALAYSIA", "MYS", "Malaysia", 458, 60 },
                    { "MZ", "MOZAMBIQUE", "MOZ", "Mozambique", 508, 258 },
                    { "NA", "NAMIBIA", "NAM", "Namibia", 516, 264 },
                    { "NC", "NEW CALEDONIA", "NCL", "New Caledonia", 540, 687 },
                    { "NE", "NIGER", "NER", "Niger", 562, 227 },
                    { "NF", "NORFOLK ISLAND", "NFK", "Norfolk Island", 574, 672 },
                    { "NG", "NIGERIA", "NGA", "Nigeria", 566, 234 },
                    { "NI", "NICARAGUA", "NIC", "Nicaragua", 558, 505 },
                    { "NL", "NETHERLANDS", "NLD", "Netherlands", 528, 31 },
                    { "NO", "NORWAY", "NOR", "Norway", 578, 47 },
                    { "NP", "NEPAL", "NPL", "Nepal", 524, 977 },
                    { "NR", "NAURU", "NRU", "Nauru", 520, 674 },
                    { "NU", "NIUE", "NIU", "Niue", 570, 683 },
                    { "NZ", "NEW ZEALAND", "NZL", "New Zealand", 554, 64 },
                    { "OM", "OMAN", "OMN", "Oman", 512, 968 },
                    { "PA", "PANAMA", "PAN", "Panama", 591, 507 },
                    { "PE", "PERU", "PER", "Peru", 604, 51 },
                    { "PF", "FRENCH POLYNESIA", "PYF", "French Polynesia", 258, 689 },
                    { "PG", "PAPUA NEW GUINEA", "PNG", "Papua New Guinea", 598, 675 },
                    { "PH", "PHILIPPINES", "PHL", "Philippines", 608, 63 },
                    { "PK", "PAKISTAN", "PAK", "Pakistan", 586, 92 },
                    { "PL", "POLAND", "POL", "Poland", 616, 48 },
                    { "PM", "SAINT PIERRE AND MIQUELON", "SPM", "Saint Pierre and Miquelon", 666, 508 },
                    { "PN", "PITCAIRN", "PCN", "Pitcairn", 612, 0 },
                    { "PR", "PUERTO RICO", "PRI", "Puerto Rico", 630, 1787 },
                    { "PS", "PALESTINIAN TERRITORY, OCCUPIED", "", "Palestinian Territory, Occupied", null, 970 },
                    { "PT", "PORTUGAL", "PRT", "Portugal", 620, 351 },
                    { "PW", "PALAU", "PLW", "Palau", 585, 680 },
                    { "PY", "PARAGUAY", "PRY", "Paraguay", 600, 595 },
                    { "QA", "QATAR", "QAT", "Qatar", 634, 974 },
                    { "RE", "REUNION", "REU", "Reunion", 638, 262 },
                    { "RO", "ROMANIA", "ROM", "Romania", 642, 40 },
                    { "RU", "RUSSIAN FEDERATION", "RUS", "Russian Federation", 643, 70 },
                    { "RW", "RWANDA", "RWA", "Rwanda", 646, 250 },
                    { "SA", "SAUDI ARABIA", "SAU", "Saudi Arabia", 682, 966 },
                    { "SB", "SOLOMON ISLANDS", "SLB", "Solomon Islands", 90, 677 },
                    { "SC", "SEYCHELLES", "SYC", "Seychelles", 690, 248 },
                    { "SD", "SUDAN", "SDN", "Sudan", 736, 249 },
                    { "SE", "SWEDEN", "SWE", "Sweden", 752, 46 },
                    { "SG", "SINGAPORE", "SGP", "Singapore", 702, 65 },
                    { "SH", "SAINT HELENA", "SHN", "Saint Helena", 654, 290 },
                    { "SI", "SLOVENIA", "SVN", "Slovenia", 705, 386 },
                    { "SJ", "SVALBARD AND JAN MAYEN", "SJM", "Svalbard and Jan Mayen", 744, 47 },
                    { "SK", "SLOVAKIA", "SVK", "Slovakia", 703, 421 },
                    { "SL", "SIERRA LEONE", "SLE", "Sierra Leone", 694, 232 },
                    { "SM", "SAN MARINO", "SMR", "San Marino", 674, 378 },
                    { "SN", "SENEGAL", "SEN", "Senegal", 686, 221 },
                    { "SO", "SOMALIA", "SOM", "Somalia", 706, 252 },
                    { "SR", "SURINAME", "SUR", "Suriname", 740, 597 },
                    { "ST", "SAO TOME AND PRINCIPE", "STP", "Sao Tome and Principe", 678, 239 },
                    { "SV", "EL SALVADOR", "SLV", "El Salvador", 222, 503 },
                    { "SY", "SYRIAN ARAB REPUBLIC", "SYR", "Syrian Arab Republic", 760, 963 },
                    { "SZ", "SWAZILAND", "SWZ", "Swaziland", 748, 268 },
                    { "TC", "TURKS AND CAICOS ISLANDS", "TCA", "Turks and Caicos Islands", 796, 1649 },
                    { "TD", "CHAD", "TCD", "Chad", 148, 235 },
                    { "TF", "FRENCH SOUTHERN TERRITORIES", "", "French Southern Territories", null, 0 },
                    { "TG", "TOGO", "TGO", "Togo", 768, 228 },
                    { "TH", "THAILAND", "THA", "Thailand", 764, 66 },
                    { "TJ", "TAJIKISTAN", "TJK", "Tajikistan", 762, 992 },
                    { "TK", "TOKELAU", "TKL", "Tokelau", 772, 690 },
                    { "TL", "TIMOR-LESTE", "", "Timor-Leste", null, 670 },
                    { "TM", "TURKMENISTAN", "TKM", "Turkmenistan", 795, 7370 },
                    { "TN", "TUNISIA", "TUN", "Tunisia", 788, 216 },
                    { "TO", "TONGA", "TON", "Tonga", 776, 676 },
                    { "TR", "TURKEY", "TUR", "Turkey", 792, 90 },
                    { "TT", "TRINIDAD AND TOBAGO", "TTO", "Trinidad and Tobago", 780, 1868 },
                    { "TV", "TUVALU", "TUV", "Tuvalu", 798, 688 },
                    { "TW", "TAIWAN, PROVINCE OF CHINA", "TWN", "Taiwan, Province of China", 158, 886 },
                    { "TZ", "TANZANIA, UNITED REPUBLIC OF", "TZA", "Tanzania, United Republic of", 834, 255 },
                    { "UA", "UKRAINE", "UKR", "Ukraine", 804, 380 },
                    { "UG", "UGANDA", "UGA", "Uganda", 800, 256 },
                    { "UM", "UNITED STATES MINOR OUTLYING ISLANDS", "", "United States Minor Outlying Islands", null, 1 },
                    { "US", "UNITED STATES", "USA", "United States", 840, 1 },
                    { "UY", "URUGUAY", "URY", "Uruguay", 858, 598 },
                    { "UZ", "UZBEKISTAN", "UZB", "Uzbekistan", 860, 998 },
                    { "VA", "HOLY SEE (VATICAN CITY STATE)", "VAT", "Holy See (Vatican City State)", 336, 39 },
                    { "VC", "SAINT VINCENT AND THE GRENADINES", "VCT", "Saint Vincent and the Grenadines", 670, 1784 },
                    { "VE", "VENEZUELA", "VEN", "Venezuela", 862, 58 },
                    { "VG", "VIRGIN ISLANDS, BRITISH", "VGB", "Virgin Islands, British", 92, 1284 },
                    { "VI", "VIRGIN ISLANDS, U.S.", "VIR", "Virgin Islands, U.s.", 850, 1340 },
                    { "VN", "VIET NAM", "VNM", "Viet Nam", 704, 84 },
                    { "VU", "VANUATU", "VUT", "Vanuatu", 548, 678 },
                    { "WF", "WALLIS AND FUTUNA", "WLF", "Wallis and Futuna", 876, 681 },
                    { "WS", "SAMOA", "WSM", "Samoa", 882, 684 },
                    { "YE", "YEMEN", "YEM", "Yemen", 887, 967 },
                    { "YT", "MAYOTTE", "", "Mayotte", null, 269 },
                    { "ZA", "SOUTH AFRICA", "ZAF", "South Africa", 710, 27 },
                    { "ZM", "ZAMBIA", "ZMB", "Zambia", 894, 260 },
                    { "ZW", "ZIMBABWE", "ZWE", "Zimbabwe", 716, 263 }
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Code3", "Code2", "Name", "NativeName" },
                values: new object[,]
                {
                    { "aar", "aa", "Afar", "Afaraf" },
                    { "abk", "ab", "Abkhaz", "аҧсуа бызшәа, аҧсшәа" },
                    { "afr", "af", "Afrikaans", "Afrikaans" },
                    { "aka", "ak", "Akan", "Akan" },
                    { "amh", "am", "Amharic", "አማርኛ" },
                    { "ara", "ar", "Arabic", "العربية" },
                    { "arg", "an", "Aragonese", "aragonés" },
                    { "asm", "as", "Assamese", "অসমীয়া" },
                    { "ava", "av", "Avaric", "авар мацӀ, магӀарул мацӀ" },
                    { "ave", "ae", "Avestan", "avesta" },
                    { "aym", "ay", "Aymara", "aymar aru" },
                    { "azb", "az", "South Azerbaijani", "تورکجه‎" },
                    { "aze", "az", "Azerbaijani", "azərbaycan dili" },
                    { "bak", "ba", "Bashkir", "башҡорт теле" },
                    { "bam", "bm", "Bambara", "bamanankan" },
                    { "bel", "be", "Belarusian", "беларуская мова" },
                    { "ben", "bn", "Bengali; Bangla", "বাংলা" },
                    { "bih", "bh", "Bihari", "भोजपुरी" },
                    { "bis", "bi", "Bislama", "Bislama" },
                    { "bod", "bo", "Tibetan Standard, Tibetan, Central", "བོད་ཡིག" },
                    { "bos", "bs", "Bosnian", "bosanski jezik" },
                    { "bre", "br", "Breton", "brezhoneg" },
                    { "bul", "bg", "Bulgarian", "български език" },
                    { "cat", "ca", "Catalan; Valencian", "català, valencià" },
                    { "ces", "cs", "Czech", "čeština, český jazyk" },
                    { "cha", "ch", "Chamorro", "Chamoru" },
                    { "che", "ce", "Chechen", "нохчийн мотт" },
                    { "chu", "cu", "Old Church Slavonic, Church Slavonic, Old Bulgarian", "ѩзыкъ словѣньскъ" },
                    { "chv", "cv", "Chuvash", "чӑваш чӗлхи" },
                    { "cor", "kw", "Cornish", "Kernewek" },
                    { "cos", "co", "Corsican", "corsu, lingua corsa" },
                    { "cre", "cr", "Cree", "ᓀᐦᐃᔭᐍᐏᐣ" },
                    { "cym", "cy", "Welsh", "Cymraeg" },
                    { "dan", "da", "Danish", "dansk" },
                    { "deu", "de", "German", "Deutsch" },
                    { "div", "dv", "Divehi; Dhivehi; Maldivian;", "ދިވެހި" },
                    { "dzo", "dz", "Dzongkha", "རྫོང་ཁ" },
                    { "ell", "el", "Greek, Modern", "ελληνικά" },
                    { "eng", "en", "English", "English" },
                    { "epo", "eo", "Esperanto", "Esperanto" },
                    { "est", "et", "Estonian", "eesti, eesti keel" },
                    { "eus", "eu", "Basque", "euskara, euskera" },
                    { "ewe", "ee", "Ewe", "Eʋegbe" },
                    { "fao", "fo", "Faroese", "føroyskt" },
                    { "fas", "fa", "Persian (Farsi)", "فارسی" },
                    { "fij", "fj", "Fijian", "vosa Vakaviti" },
                    { "fin", "fi", "Finnish", "suomi, suomen kieli" },
                    { "fra", "fr", "French", "français, langue française" },
                    { "fry", "fy", "Western Frisian", "Frysk" },
                    { "ful", "ff", "Fula; Fulah; Pulaar; Pular", "Fulfulde, Pulaar, Pular" },
                    { "gla", "gd", "Scottish Gaelic; Gaelic", "Gàidhlig" },
                    { "gle", "ga", "Irish", "Gaeilge" },
                    { "glg", "gl", "Galician", "galego" },
                    { "glv", "gv", "Manx", "Gaelg, Gailck" },
                    { "grn", "gn", "Guaraní", "Avañe'ẽ" },
                    { "guj", "gu", "Gujarati", "ગુજરાતી" },
                    { "hat", "ht", "Haitian; Haitian Creole", "Kreyòl ayisyen" },
                    { "hau", "ha", "Hausa", "Hausa, هَوُسَ" },
                    { "heb", "he", "Hebrew (modern)", "עברית" },
                    { "her", "hz", "Herero", "Otjiherero" },
                    { "hin", "hi", "Hindi", "हिन्दी, हिंदी" },
                    { "hmo", "ho", "Hiri Motu", "Hiri Motu" },
                    { "hrv", "hr", "Croatian", "hrvatski jezik" },
                    { "hun", "hu", "Hungarian", "magyar" },
                    { "hye", "hy", "Armenian", "Հայերեն" },
                    { "ibo", "ig", "Igbo", "Asụsụ Igbo" },
                    { "ido", "io", "Ido", "Ido" },
                    { "iii", "ii", "Nuosu", "ꆈꌠ꒿ Nuosuhxop" },
                    { "iku", "iu", "Inuktitut", "ᐃᓄᒃᑎᑐᑦ" },
                    { "ile", "ie", "Interlingue", "Originally called Occidental; then Interlingue after WWII" },
                    { "ina", "ia", "Interlingua", "Interlingua" },
                    { "ind", "id", "Indonesian", "Bahasa Indonesia" },
                    { "ipk", "ik", "Inupiaq", "Iñupiaq, Iñupiatun" },
                    { "isl", "is", "Icelandic", "Íslenska" },
                    { "ita", "it", "Italian", "italiano" },
                    { "jav", "jv", "Javanese", "basa Jawa" },
                    { "jpn", "ja", "Japanese", "日本語 (にほんご)" },
                    { "kal", "kl", "Kalaallisut, Greenlandic", "kalaallisut, kalaallit oqaasii" },
                    { "kan", "kn", "Kannada", "ಕನ್ನಡ" },
                    { "kas", "ks", "Kashmiri", "कश्मीरी, كشميري‎" },
                    { "kat", "ka", "Georgian", "ქართული" },
                    { "kau", "kr", "Kanuri", "Kanuri" },
                    { "kaz", "kk", "Kazakh", "қазақ тілі" },
                    { "khm", "km", "Khmer", "ខ្មែរ, ខេមរភាសា, ភាសាខ្មែរ" },
                    { "kik", "ki", "Kikuyu, Gikuyu", "Gĩkũyũ" },
                    { "kin", "rw", "Kinyarwanda", "Ikinyarwanda" },
                    { "kir", "ky", "Kyrgyz", "Кыргызча, Кыргыз тили" },
                    { "kom", "kv", "Komi", "коми кыв" },
                    { "kon", "kg", "Kongo", "KiKongo" },
                    { "kor", "ko", "Korean", "한국어 (韓國語), 조선어 (朝鮮語)" },
                    { "kua", "kj", "Kwanyama, Kuanyama", "Kuanyama" },
                    { "kur", "ku", "Kurdish", "Kurdî, كوردی‎" },
                    { "lao", "lo", "Lao", "ພາສາລາວ" },
                    { "lat", "la", "Latin", "latine, lingua latina" },
                    { "lav", "lv", "Latvian", "latviešu valoda" },
                    { "lim", "li", "Limburgish, Limburgan, Limburger", "Limburgs" },
                    { "lin", "ln", "Lingala", "Lingála" },
                    { "lit", "lt", "Lithuanian", "lietuvių kalba" },
                    { "ltz", "lb", "Luxembourgish, Letzeburgesch", "Lëtzebuergesch" },
                    { "lub", "lu", "Luba-Katanga", "Tshiluba" },
                    { "lug", "lg", "Ganda", "Luganda" },
                    { "mah", "mh", "Marshallese", "Kajin M̧ajeļ" },
                    { "mal", "ml", "Malayalam", "മലയാളം" },
                    { "mar", "mr", "Marathi (Marāṭhī)", "मराठी" },
                    { "mkd", "mk", "Macedonian", "македонски јазик" },
                    { "mlg", "mg", "Malagasy", "fiteny malagasy" },
                    { "mlt", "mt", "Maltese", "Malti" },
                    { "mon", "mn", "Mongolian", "монгол" },
                    { "mri", "mi", "Māori", "te reo Māori" },
                    { "msa", "ms", "Malay", "bahasa Melayu, بهاس ملايو‎" },
                    { "mya", "my", "Burmese", "ဗမာစာ" },
                    { "nau", "na", "Nauru", "Ekakairũ Naoero" },
                    { "nav", "nv", "Navajo, Navaho", "Diné bizaad, Dinékʼehǰí" },
                    { "nbl", "nr", "South Ndebele", "isiNdebele" },
                    { "nde", "nd", "North Ndebele", "isiNdebele" },
                    { "ndo", "ng", "Ndonga", "Owambo" },
                    { "nep", "ne", "Nepali", "नेपाली" },
                    { "nld", "nl", "Dutch", "Nederlands, Vlaams" },
                    { "nno", "nn", "Norwegian Nynorsk", "Norsk nynorsk" },
                    { "nob", "nb", "Norwegian Bokmål", "Norsk bokmål" },
                    { "nor", "no", "Norwegian", "Norsk" },
                    { "nya", "ny", "Chichewa; Chewa; Nyanja", "chiCheŵa, chinyanja" },
                    { "oci", "oc", "Occitan", "occitan, lenga d'òc" },
                    { "oji", "oj", "Ojibwe, Ojibwa", "ᐊᓂᔑᓈᐯᒧᐎᓐ" },
                    { "ori", "or", "Oriya", "ଓଡ଼ିଆ" },
                    { "orm", "om", "Oromo", "Afaan Oromoo" },
                    { "oss", "os", "Ossetian, Ossetic", "ирон æвзаг" },
                    { "pan", "pa", "Panjabi, Punjabi", "ਪੰਜਾਬੀ, پنجابی‎" },
                    { "pli", "pi", "Pāli", "पाऴि" },
                    { "pol", "pl", "Polish", "język polski, polszczyzna" },
                    { "por", "pt", "Portuguese", "português" },
                    { "pus", "ps", "Pashto, Pushto", "پښتو" },
                    { "que", "qu", "Quechua", "Runa Simi, Kichwa" },
                    { "roh", "rm", "Romansh", "rumantsch grischun" },
                    { "ron", "ro", "Romanian", "limba română" },
                    { "run", "rn", "Kirundi", "Ikirundi" },
                    { "rus", "ru", "Russian", "русский язык" },
                    { "sag", "sg", "Sango", "yângâ tî sängö" },
                    { "san", "sa", "Sanskrit (Saṁskṛta)", "संस्कृतम्" },
                    { "sin", "si", "Sinhala, Sinhalese", "සිංහල" },
                    { "slk", "sk", "Slovak", "slovenčina, slovenský jazyk" },
                    { "slv", "sl", "Slovene", "slovenski jezik, slovenščina" },
                    { "sme", "se", "Northern Sami", "Davvisámegiella" },
                    { "smo", "sm", "Samoan", "gagana fa'a Samoa" },
                    { "sna", "sn", "Shona", "chiShona" },
                    { "snd", "sd", "Sindhi", "सिन्धी, سنڌي، سندھی‎" },
                    { "som", "so", "Somali", "Soomaaliga, af Soomaali" },
                    { "sot", "st", "Southern Sotho", "Sesotho" },
                    { "spa", "es", "Spanish", "español" },
                    { "sqi", "sq", "Albanian", "gjuha shqipe" },
                    { "srd", "sc", "Sardinian", "sardu" },
                    { "srp", "sr", "Serbian", "српски језик" },
                    { "ssw", "ss", "Swati", "SiSwati" },
                    { "sun", "su", "Sundanese", "Basa Sunda" },
                    { "swa", "sw", "Swahili", "Kiswahili" },
                    { "swe", "sv", "Swedish", "Svenska" },
                    { "tah", "ty", "Tahitian", "Reo Tahiti" },
                    { "tam", "ta", "Tamil", "தமிழ்" },
                    { "tat", "tt", "Tatar", "татар теле, tatar tele" },
                    { "tel", "te", "Telugu", "తెలుగు" },
                    { "tgk", "tg", "Tajik", "тоҷикӣ, toğikī, تاجیکی‎" },
                    { "tgl", "tl", "Tagalog", "Wikang Tagalog, ᜏᜒᜃᜅ᜔ ᜆᜄᜎᜓᜄ᜔" },
                    { "tha", "th", "Thai", "ไทย" },
                    { "tir", "ti", "Tigrinya", "ትግርኛ" },
                    { "ton", "to", "Tonga (Tonga Islands)", "faka Tonga" },
                    { "tsn", "tn", "Tswana", "Setswana" },
                    { "tso", "ts", "Tsonga", "Xitsonga" },
                    { "tuk", "tk", "Turkmen", "Türkmen, Түркмен" },
                    { "tur", "tr", "Turkish", "Türkçe" },
                    { "twi", "tw", "Twi", "Twi" },
                    { "uig", "ug", "Uyghur, Uighur", "Uyƣurqə, ئۇيغۇرچە‎" },
                    { "ukr", "uk", "Ukrainian", "українська мова" },
                    { "urd", "ur", "Urdu", "اردو" },
                    { "uzb", "uz", "Uzbek", "O‘zbek, Ўзбек, أۇزبېك‎" },
                    { "ven", "ve", "Venda", "Tshivenḓa" },
                    { "vie", "vi", "Vietnamese", "Tiếng Việt" },
                    { "vol", "vo", "Volapük", "Volapük" },
                    { "wln", "wa", "Walloon", "walon" },
                    { "wol", "wo", "Wolof", "Wollof" },
                    { "xho", "xh", "Xhosa", "isiXhosa" },
                    { "yid", "yi", "Yiddish", "ייִדיש" },
                    { "yor", "yo", "Yoruba", "Yorùbá" },
                    { "zha", "za", "Zhuang, Chuang", "Saɯ cueŋƅ, Saw cuengh" },
                    { "zho", "zh", "Chinese", "中文 (Zhōngwén), 汉语, 漢語" },
                    { "zul", "zu", "Zulu", "isiZulu" }
                });

            migrationBuilder.InsertData(
                table: "OrganizationSettings",
                columns: new[] { "OrganizationId", "AttachAllJobs", "AttachAllSkills", "DefaultTemplateId", "ResumeYearHistory", "ShowContactInfo", "ShowDuration", "ShowRatings", "ShowTechnologyPerJob", "SkillView" },
                values: new object[] { 1, true, true, 2, 10, true, true, false, true, 1 });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "Azure" },
                    { 3, "Sql Server" },
                    { 4, "Angular" },
                    { 5, "Entity Framework" },
                    { 6, "TypeScript" },
                    { 7, "CI/CD" },
                    { 8, "oAuth2" },
                    { 9, "Visual Studio" },
                    { 10, "Identity Server" },
                    { 11, "Postman" },
                    { 12, "SignalR" },
                    { 13, "Material Design" },
                    { 14, "Bootstrap" },
                    { 15, "Figma" },
                    { 16, "gRPC" },
                    { 17, "Blazor" },
                    { 18, "Geneva" },
                    { 19, "Data Explorer" },
                    { 20, "Synapse" },
                    { 21, "Function Apps" },
                    { 22, "AppInsights" },
                    { 23, "Kusto" },
                    { 24, "Bicep" },
                    { 25, "Api Integrations" },
                    { 26, "Orchard" },
                    { 27, "Objective-C" },
                    { 29, "WCF" },
                    { 30, "IIS" },
                    { 31, ".NET Framework" },
                    { 32, "WPF" },
                    { 33, "Classic ASP" },
                    { 34, "ASP.NET" },
                    { 35, "Big Data" },
                    { 36, "Html/CSS" },
                    { 37, "Artificial Intelligence (AI)" },
                    { 38, ".Net Core" },
                    { 39, "Stripe" },
                    { 40, "WebHooks" },
                    { 41, "Microsoft Teams" },
                    { 42, "Jira" },
                    { 43, "jQuery" },
                    { 44, "JavaScript" },
                    { 45, "Powershell" },
                    { 46, "Python" },
                    { 47, "XAML" },
                    { 48, "Java" },
                    { 49, "C++" },
                    { 50, "PHP" },
                    { 51, "Swift" },
                    { 52, "Ruby" },
                    { 53, "C" },
                    { 54, "Kotlin" },
                    { 55, "R" },
                    { 56, "Go" },
                    { 57, "Scala" },
                    { 58, "Perl" },
                    { 59, "Rust" },
                    { 60, "Dart" },
                    { 61, "Elixir" },
                    { 62, "Haskell" },
                    { 63, "MySQL" },
                    { 64, "PostgreSQL" },
                    { 65, "MongoDB" },
                    { 66, "Oracle Database" },
                    { 67, "Spring" },
                    { 68, "Django" },
                    { 69, "Ruby on Rails" },
                    { 70, "React" },
                    { 71, "Vue.js" },
                    { 72, "Laravel" },
                    { 73, "Express.js" },
                    { 74, "Flask" },
                    { 75, "Symfony" },
                    { 76, "Meteor" },
                    { 77, "Svelte" },
                    { 78, "Next.js" },
                    { 79, "Gatsby" },
                    { 80, "Play Framework" },
                    { 81, "CodeIgniter" },
                    { 82, "CakePHP" },
                    { 83, "Phoenix" },
                    { 84, "NestJS" },
                    { 85, "Jenkins" },
                    { 86, "Docker" },
                    { 87, "Kubernetes" },
                    { 88, "Ansible" },
                    { 89, "Terraform" },
                    { 90, "GitLab" },
                    { 91, "Prometheus" },
                    { 92, "Nagios" },
                    { 93, "Puppet" },
                    { 94, "Chef" },
                    { 95, "Azure Devops" },
                    { 96, "Amazon Web Services (AWS)" },
                    { 97, "Google Cloud Platform" },
                    { 98, "Oracle Cloud" },
                    { 99, "IBM Cloud" },
                    { 100, "Google APIs" },
                    { 101, "Twitter API" },
                    { 102, "Facebook API" },
                    { 103, "Amazon AWS API" },
                    { 104, "Twilio API" },
                    { 105, "Spotify API" }
                });

            migrationBuilder.InsertData(
                table: "SkillCategory",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Programming Languages" },
                    { 2, "Databases" },
                    { 3, "Web Frameworks" },
                    { 4, "DevOps/Infrastructure" },
                    { 5, "Cloud Platforms" },
                    { 6, "APIs/Integration Tools" },
                    { 7, "Software Development Tools" },
                    { 8, "UI/UX Design" },
                    { 9, "Data Management/Analytics" },
                    { 10, "Server-Side Technologies" },
                    { 11, "Markup Languages" },
                    { 12, "Payment and Commerce" },
                    { 13, "Legacy Technologies" }
                });

            migrationBuilder.InsertData(
                table: "Template",
                columns: new[] { "Id", "Engine", "Format", "Name", "OrganizationId", "Source" },
                values: new object[,]
                {
                    { 1, "hb", "htm", "html", null, "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>{{FirstName}} {{LastName}} - Resume</title>\r\n    <style>\r\n        body { font-family: Arial, sans-serif; margin: 20px; }\r\n        h1 { font-size: 24px; }\r\n        h2 { font-size: 20px; margin-top: 20px; }\r\n        p { margin: 5px 0; }\r\n        ul { list-style-type: none; padding: 0; }\r\n        ul li { margin: 5px 0; }\r\n        .contact-info { margin-bottom: 20px; }\r\n        .section { margin-bottom: 20px; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <h1>{{FirstName}} {{LastName}}</h1>\r\n    <div class=\"contact-info\">\r\n        <p>Email: <a href=\"mailto:{{Email}}\">{{Email}}</a></p>\r\n        <p>Phone: {{PhoneNumber}}</p>\r\n        <p>LinkedIn: <a href=\"{{LinkedIn}}\">{{LinkedIn}}</a></p>\r\n        <p>GitHub: <a href=\"{{GitHub}}\">{{GitHub}}</a></p>\r\n        <p>Location: {{City}}, {{State}}, {{Country}}</p>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Job Title</h2>\r\n        <p>{{JobTitle}}</p>\r\n        <p>{{Description}}</p>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Skills</h2>\r\n        <ul>\r\n            {{#each Skills}}\r\n            <li>{{Title}} - {{Rating}}</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Experience</h2>\r\n        {{#each Jobs}}\r\n        <div class=\"job\">\r\n            <h3>{{Title}} at {{Company}}</h3>\r\n            <p>{{Location}} | {{StartDate}} - {{EndDate}}</p>\r\n            <p>{{Description}}</p>\r\n            <ul>\r\n                {{#each Highlights}}\r\n                <li>{{Text}}</li>\r\n                {{/each}}\r\n            </ul>\r\n            <ul>\r\n                {{#each Skills}}\r\n                <li>{{Name}}</li>\r\n                {{/each}}\r\n            </ul>\r\n            <div>\r\n                <h4>Projects:</h4>\r\n                {{#each Projects}}\r\n                <div class=\"project\">\r\n                    <h5>{{Name}}</h5>\r\n                    <p>{{Description}}</p>\r\n                    <ul>\r\n                        {{#each Highlights}}\r\n                        <li>{{Text}}</li>\r\n                        {{/each}}\r\n                    </ul>\r\n                </div>\r\n                {{/each}}\r\n            </div>\r\n        </div>\r\n        {{/each}}\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Education</h2>\r\n        {{#each Education}}\r\n        <div class=\"education\">\r\n            <h3>{{Name}}</h3>\r\n            {{#each Degrees}}\r\n            <p>{{Degree}} | {{StartDate}} - {{EndDate}}</p>\r\n            {{/each}}\r\n        </div>\r\n        {{/each}}\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Languages</h2>\r\n        <ul>\r\n            {{#each Languages}}\r\n            <li>{{LanguageName}} - {{Proficiency}}</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Certifications</h2>\r\n        <ul>\r\n            {{#each Certifications}}\r\n            <li>{{Name}} - {{OrganizationId}} ({{Date}})</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>References</h2>\r\n        <ul>\r\n            {{#each References}}\r\n            <li>{{Name}} - {{PhoneNumber}} | {{Text}}</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n</body>\r\n</html>\r\n" },
                    { 2, "hb", "md", "markdown", null, "# {{firstName}} {{lastName}}, {{jobTitle}}\r\n\r\n{{#if settings.showContactInfo}}\r\n- **Email:** {{email}}\r\n- **Phone:** {{phoneNumber}}\r\n- **LinkedIn:** {{linkedIn}}\r\n- **GitHub:** {{gitHub}}\r\n{{/if}}\r\n- **Languages:** {{languageString}}\r\n\r\n## Description\r\n{{description}}\r\n\r\n{{#eq settings.skillView 'Grouped'}}\r\n## Skills\r\n| Category               | Skills & Ratings                                       |\r\n|------------------------|--------------------------------------------------------|\r\n{{#each skillDictionary}}\r\n| **{{category}}**       | {{#each skills}}{{title}}{{#if ../settings.showRatings}}({{rating}}){{/if}}{{#unless @last}}, {{/unless}}{{/each}} |\r\n{{/each}}\r\n{{/eq}}\r\n\r\n{{#eq settings.skillView 'List'}}\r\n## Skills\r\n{{#each skills}} \r\n- {{title}} {{#if ../settings.showRatings}}(Rating: {{rating}}){{/if}}\r\n{{/each}}\r\n{{/eq}}\r\n\r\n## Experience\r\n{{#each jobs}}\r\n### {{title}} - {{company}}\r\n*{{location}} - {{formatDate startDate}}-{{displayEndDate}} {{#if ../settings.showDuration}}({{duration}}){{/if}}*\r\n{{#each highlights}}\r\n- {{text}}\r\n{{/each}}\r\n{{#each projects}}\r\n#### Project: {{name}}\r\n{{description}}\r\n{{#each highlights}}\r\n- {{text}}\r\n{{/each}}\r\n{{/each}}\r\n\r\n{{#if Skills}}\r\n**Technology Used:** {{#each Skills}}{{Name}}{{#unless @last}}, {{/unless}}{{/each}}\r\n{{/if}}\r\n{{/each}}\r\n\r\n## Education\r\n{{#each education}}\r\n### {{name}}\r\n*{{formatDate startDate}}-{{displayEndDate}}*\r\n{{#each degrees}}\r\n- Degree: {{name}}\r\n{{/each}}\r\n{{/each}}\r\n\r\n## References\r\n{{#each references}}\r\n### {{name}}\r\n{{text}}\r\n{{/each}}" }
                });

            migrationBuilder.InsertData(
                table: "SkillCategorySkill",
                columns: new[] { "SkillCategoryId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 6 },
                    { 1, 27 },
                    { 1, 44 },
                    { 1, 46 },
                    { 1, 48 },
                    { 1, 49 },
                    { 1, 50 },
                    { 1, 51 },
                    { 1, 52 },
                    { 1, 53 },
                    { 1, 54 },
                    { 1, 55 },
                    { 1, 56 },
                    { 1, 57 },
                    { 1, 58 },
                    { 1, 59 },
                    { 1, 60 },
                    { 1, 61 },
                    { 1, 62 },
                    { 2, 3 },
                    { 2, 63 },
                    { 2, 64 },
                    { 2, 65 },
                    { 2, 66 },
                    { 3, 4 },
                    { 3, 17 },
                    { 3, 31 },
                    { 3, 34 },
                    { 3, 38 },
                    { 3, 43 },
                    { 3, 67 },
                    { 3, 68 },
                    { 3, 69 },
                    { 3, 70 },
                    { 3, 71 },
                    { 3, 72 },
                    { 3, 73 },
                    { 3, 74 },
                    { 3, 75 },
                    { 3, 76 },
                    { 3, 77 },
                    { 3, 78 },
                    { 3, 79 },
                    { 3, 80 },
                    { 3, 81 },
                    { 3, 82 },
                    { 3, 83 },
                    { 3, 84 },
                    { 4, 7 },
                    { 4, 24 },
                    { 4, 85 },
                    { 4, 86 },
                    { 4, 87 },
                    { 4, 88 },
                    { 4, 89 },
                    { 4, 90 },
                    { 4, 91 },
                    { 4, 92 },
                    { 4, 93 },
                    { 4, 94 },
                    { 4, 95 },
                    { 5, 2 },
                    { 5, 96 },
                    { 5, 97 },
                    { 5, 98 },
                    { 5, 99 },
                    { 6, 8 },
                    { 6, 10 },
                    { 6, 11 },
                    { 6, 25 },
                    { 6, 100 },
                    { 6, 101 },
                    { 6, 102 },
                    { 6, 103 },
                    { 6, 104 },
                    { 6, 105 },
                    { 7, 9 },
                    { 7, 41 },
                    { 7, 42 },
                    { 7, 45 },
                    { 8, 13 },
                    { 8, 14 },
                    { 8, 15 },
                    { 9, 19 },
                    { 9, 20 },
                    { 9, 23 },
                    { 9, 35 },
                    { 9, 37 },
                    { 10, 5 },
                    { 10, 12 },
                    { 10, 16 },
                    { 10, 18 },
                    { 10, 21 },
                    { 10, 22 },
                    { 10, 26 },
                    { 10, 29 },
                    { 10, 30 },
                    { 11, 36 },
                    { 11, 47 },
                    { 12, 39 },
                    { 12, 40 },
                    { 13, 31 },
                    { 13, 32 },
                    { 13, 33 }
                });

            migrationBuilder.InsertData(
                table: "StateProvince",
                columns: new[] { "Id", "Abbrev", "Code", "Iso2", "Name" },
                values: new object[,]
                {
                    { 1, "Ala.", "AL", "US", "Alabama" },
                    { 2, "Alaska", "AK", "US", "Alaska" },
                    { 3, "Ariz.", "AZ", "US", "Arizona" },
                    { 4, "Ark.", "AR", "US", "Arkansas" },
                    { 5, "Calif.", "CA", "US", "California" },
                    { 6, "Colo.", "CO", "US", "Colorado" },
                    { 7, "Conn.", "CT", "US", "Connecticut" },
                    { 8, "Del.", "DE", "US", "Delaware" },
                    { 9, "D.C.", "DC", "US", "District of Columbia" },
                    { 10, "Fla.", "FL", "US", "Florida" },
                    { 11, "Ga.", "GA", "US", "Georgia" },
                    { 12, "Hawaii", "HI", "US", "Hawaii" },
                    { 13, "Idaho", "ID", "US", "Idaho" },
                    { 14, "Ill.", "IL", "US", "Illinois" },
                    { 15, "Ind.", "IN", "US", "Indiana" },
                    { 16, "Iowa", "IA", "US", "Iowa" },
                    { 17, "Kans.", "KS", "US", "Kansas" },
                    { 18, "Ky.", "KY", "US", "Kentucky" },
                    { 19, "La.", "LA", "US", "Louisiana" },
                    { 20, "Maine", "ME", "US", "Maine" },
                    { 21, "Md.", "MD", "US", "Maryland" },
                    { 22, "Mass.", "MA", "US", "Massachusetts" },
                    { 23, "Mich.", "MI", "US", "Michigan" },
                    { 24, "Minn.", "MN", "US", "Minnesota" },
                    { 25, "Miss.", "MS", "US", "Mississippi" },
                    { 26, "Mo.", "MO", "US", "Missouri" },
                    { 27, "Mont.", "MT", "US", "Montana" },
                    { 28, "Nebr.", "NE", "US", "Nebraska" },
                    { 29, "Nev.", "NV", "US", "Nevada" },
                    { 30, "N.H.", "NH", "US", "New Hampshire" },
                    { 31, "N.J.", "NJ", "US", "New Jersey" },
                    { 32, "N.M.", "NM", "US", "New Mexico" },
                    { 33, "N.Y.", "NY", "US", "New York" },
                    { 34, "N.C.", "NC", "US", "North Carolina" },
                    { 35, "N.D.", "ND", "US", "North Dakota" },
                    { 36, "Ohio", "OH", "US", "Ohio" },
                    { 37, "Okla.", "OK", "US", "Oklahoma" },
                    { 38, "Ore.", "OR", "US", "Oregon" },
                    { 39, "Pa.", "PA", "US", "Pennsylvania" },
                    { 40, "R.I.", "RI", "US", "Rhode Island" },
                    { 41, "S.C.", "SC", "US", "South Carolina" },
                    { 42, "S.D.", "SD", "US", "South Dakota" },
                    { 43, "Tenn.", "TN", "US", "Tennessee" },
                    { 44, "Tex.", "TX", "US", "Texas" },
                    { 45, "Utah", "UT", "US", "Utah" },
                    { 46, "Vt.", "VT", "US", "Vermont" },
                    { 47, "Va.", "VA", "US", "Virginia" },
                    { 48, "Wash.", "WA", "US", "Washington" },
                    { 49, "W.Va.", "WV", "US", "West Virginia" },
                    { 50, "Wis.", "WI", "US", "Wisconsin" },
                    { 51, "Wyo.", "WY", "US", "Wyoming" }
                });

            migrationBuilder.InsertData(
                table: "Persona",
                columns: new[] { "Id", "OrganizationId", "City", "Email", "FirstName", "GitHub", "IsDeleted", "LastName", "LinkedIn", "PhoneNumber", "StateId" },
                values: new object[] { 1, 1, "Salt Lake City", "rodmjay@gmail.com", "Rod", "https://www.github.com/rodmjay", false, "Johnson", "https://www.linkedin.com/in/rodmjay", "(385) 352-6026", 45 });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "Id", "OrganizationId", "Company", "Description", "EndDate", "JobTitle", "Location", "PersonaId", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, "Infosys", null, null, "Technical Architect", "Salt Lake City, UT", 1, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "Solution Stream", null, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sr. Software Architect", "American Fork, UT", 1, new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, "IdeaFortune", null, new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Founder/Architect", "American Fork, UT", 1, new DateTime(2017, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, "Agile Software and Marketing", null, new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect", "Cameron Park, CA", 1, new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, "Access Softek", null, new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sr. Engineer Dev Lead", "West Jordan, UT", 1, new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, "Netchex", null, new DateTime(2013, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect Consultant", "Mandeville, LA", 1, new DateTime(2012, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, "Ancestry.com", null, new DateTime(2012, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sr. Engineer", "Provo, UT", 1, new DateTime(2010, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 1, "Cathexis", null, new DateTime(2010, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect/Dev Manager", "Provo, UT", 1, new DateTime(2008, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 1, "Motorola Public Safety", null, new DateTime(2008, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineer", "Salt Lake City, UT", 1, new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PersonaLanguage",
                columns: new[] { "Code3", "OrganizationId", "PersonaId", "Proficiency" },
                values: new object[,]
                {
                    { "eng", 1, 1, 5 },
                    { "spa", 1, 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "PersonaSkill",
                columns: new[] { "OrganizationId", "PersonaId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 1, 1, 2 },
                    { 1, 1, 3 },
                    { 1, 1, 4 },
                    { 1, 1, 5 },
                    { 1, 1, 6 },
                    { 1, 1, 7 },
                    { 1, 1, 8 },
                    { 1, 1, 9 },
                    { 1, 1, 10 },
                    { 1, 1, 11 },
                    { 1, 1, 12 },
                    { 1, 1, 13 },
                    { 1, 1, 14 },
                    { 1, 1, 15 },
                    { 1, 1, 16 },
                    { 1, 1, 17 },
                    { 1, 1, 18 },
                    { 1, 1, 19 },
                    { 1, 1, 20 },
                    { 1, 1, 21 },
                    { 1, 1, 22 },
                    { 1, 1, 23 },
                    { 1, 1, 24 },
                    { 1, 1, 25 },
                    { 1, 1, 26 },
                    { 1, 1, 27 },
                    { 1, 1, 29 },
                    { 1, 1, 30 },
                    { 1, 1, 31 },
                    { 1, 1, 32 },
                    { 1, 1, 33 },
                    { 1, 1, 34 },
                    { 1, 1, 35 },
                    { 1, 1, 36 },
                    { 1, 1, 37 },
                    { 1, 1, 38 },
                    { 1, 1, 39 },
                    { 1, 1, 40 },
                    { 1, 1, 41 },
                    { 1, 1, 42 },
                    { 1, 1, 43 },
                    { 1, 1, 44 },
                    { 1, 1, 45 },
                    { 1, 1, 46 },
                    { 1, 1, 47 },
                    { 1, 1, 95 },
                    { 1, 1, 96 }
                });

            migrationBuilder.InsertData(
                table: "Reference",
                columns: new[] { "Id", "OrganizationId", "PersonaId", "Name", "Order", "PhoneNumber", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, "Joseph Cotton", 1, null, "I had the opportunity to work with Rod as a fellow solutions architect at Kahoa. Rod was leading a team of developers to meet client software needs. During my time there, Rod was not only an outstanding project architect and team lead, but an outstanding individual contributor as well. He was still able to contribute just as much as the rest of his team did despite his additional leadership responsibilities.  Rod was also a central contributing figure to company architectural principles as a whole. He was was a senior member of Kahoa's architecture community, and helped guide discussions around software standards and best practices for the company as a whole. Rod is an excellent architect and software engineer. He has the ability to lead teams and projects, and has the grit to get them over the line when it's needed. I very highly recommend him to anyone seeking an outstanding software architect or team lead." },
                    { 2, 1, 1, "Cameo Doran", 2, null, "I worked with Rod when he was recruited as the lead architect for a complicated financial SaaS product for an important client.\\Rod quickly impressed me with his ability to put himself in the clients shoes and build creative solutions that were focused on bringing the most value for the smallest cost.\\He also understands how to leverage the chosen technology for great results. Many a time I was impressed with recommendations he made that were far beyond what anyone else had considered.\\Rod’s experience and skill as an architect and engineering leader allowed us to place him on critical client projects and trust that he would delight the client and lead the team successfully.\\It was a pleasure to work with such a talented mind. Rod will add experience and technical leadership to any company." },
                    { 5, 1, 1, "Rob Atlas", 3, null, "Recently, my startup worked with Rod on the development of our MVP (Minimal Viable Product) offering. I have worked with 100's of software developers over my career. Rod is one of the most talented and efficient architects/developers with whom I have been associated. I highly recommend him as a designer and implementer of complex or sophisticated software." },
                    { 6, 1, 1, "Robert Clymer", 4, null, "If you want someone who can have high bandwidth conversations about the best way to design something, and then have that person accurately implement the agreed upon ideas as 5X the speed of a typical developer, Rod is your guy." },
                    { 7, 1, 1, "Daniel Schulz", 5, null, "Rod is a brilliant developer and a hard worker. He literally saved our project as I added him in the last hours as his expertise directed us to deliver. He certainly says up with the latest technologies, is a very fast learner, and was able to lead us in them. I highly recommend him." },
                    { 8, 1, 1, "Ryan Done", 6, null, "I worked with Rod on a complex web project at Ancestry.com. Rod made a big difference in the success of our project by finding great solutions and sharing different ways of looking at problems. He is sharp, knowledgeable and a great team player." },
                    { 9, 1, 1, "Gregg B. Jensen", 7, null, "Rod is a very skilled, and well rounded professional in web development. He is committed to success in all of his projects, and makes sure to deliver more than whats expected. His is a great asset to any team, and is an excellent team member." }
                });

            migrationBuilder.InsertData(
                table: "Resume",
                columns: new[] { "Id", "OrganizationId", "Description", "JobTitle", "PersonaId" },
                values: new object[] { 1, 1, "Rod is an enterprise architect with deep expertise in the latest .NET and web technologies. With 19 years of experience as a professional developer and architect, he has mastered the complete software development lifecycle, from ideation to implementation. Rod is frequently praised as a 10x developer, consistently delivering high-end software solutions from the ground up.", "Enterprise Application Architect", 1 });

            migrationBuilder.InsertData(
                table: "School",
                columns: new[] { "Id", "OrganizationId", "EndDate", "Location", "Name", "PersonaId", "StartDate" },
                values: new object[] { 1, 1, new DateTime(2005, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portland, OR", "Portland Community College", 1, new DateTime(2004, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Degree",
                columns: new[] { "Id", "OrganizationId", "Name", "Order", "SchoolId" },
                values: new object[] { 1, 1, "AAS Computer and Information Systems", 1, 1 });

            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "OrganizationId", "JobId", "Order", "ProjectId", "Text" },
                values: new object[,]
                {
                    { 6, 1, 2, 1, null, "Emerged as the company-wide leader for .NET initiatives, steering pivotal technology decisions and strategies." },
                    { 7, 1, 2, 2, null, "Revolutionized technical screenings and led numerous interviews, enhancing recruitment processes and team quality." },
                    { 8, 1, 2, 3, null, "Created a shared .NET codebase, significantly reducing development redundancy and boosting project efficiency." },
                    { 9, 1, 2, 4, null, "Awarded 'Top Developer of 2021' for exceptional productivity and innovation." },
                    { 17, 1, 5, 1, null, "Managed four developers directly using Agile Scrum methodologies." },
                    { 18, 1, 5, 2, null, "Responsible for the architecture and development of components within a banking platform." },
                    { 19, 1, 5, 3, null, "Developed Fingerprint Login and Friends and Family Shared Banking components." },
                    { 28, 1, 9, 1, null, "Developed tools using C#, WCF, and XAML to support the Miami-Dade Police Department." },
                    { 29, 1, 9, 2, null, "Received extensive training in .NET, JavaScript, and WCF directly from Microsoft." },
                    { 31, 1, 3, 1, null, "Responsible for the ideation, design, and delivery of a comprehensive professional services platform." },
                    { 32, 1, 3, 2, null, "Raised startup capital to fund company operations for three years." },
                    { 33, 1, 3, 3, null, "Managed a team of five developers to implement the flagship product." },
                    { 37, 1, 4, 1, null, "Reconstructed the flagship product, transitioning it from ASP to the .NET Framework to modernize infrastructure and improve scalability." },
                    { 38, 1, 4, 2, null, "Developed a comprehensive order and scheduling system with full functionality." },
                    { 39, 1, 4, 3, null, "Recruited, interviewed, and mentored developers at various levels, fostering a skilled and cohesive team." },
                    { 40, 1, 4, 4, null, "Developed a comprehensive DevOps strategy to ensure compliance with international regulations." },
                    { 44, 1, 6, 1, null, "Redesigned the flagship product by integrating advanced technology to enhance performance and functionality." },
                    { 45, 1, 6, 2, null, "Collaborated directly with the onsite team, providing mentorship to enhance skills and foster professional growth." },
                    { 46, 1, 6, 3, null, "Engineered numerous innovative features into the existing platform, significantly enhancing its capabilities and user experience." },
                    { 47, 1, 6, 4, null, "Specialized expertise in PCI Compliance and payment processing systems." },
                    { 49, 1, 7, 2, null, "Spearheaded companywide adoption and training of jQuery." }
                });

            migrationBuilder.InsertData(
                table: "JobSkill",
                columns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 1, 1, 1, 3 },
                    { 1, 1, 1, 4 },
                    { 1, 1, 1, 5 },
                    { 1, 1, 1, 6 },
                    { 1, 1, 1, 7 },
                    { 1, 1, 1, 9 },
                    { 1, 1, 1, 18 },
                    { 1, 1, 1, 19 },
                    { 1, 1, 1, 20 },
                    { 1, 1, 1, 21 },
                    { 1, 1, 1, 22 },
                    { 1, 1, 1, 23 },
                    { 1, 1, 1, 24 },
                    { 1, 1, 1, 31 },
                    { 1, 1, 1, 34 },
                    { 1, 1, 1, 36 },
                    { 1, 1, 1, 37 },
                    { 1, 1, 1, 38 },
                    { 2, 1, 1, 1 },
                    { 2, 1, 1, 2 },
                    { 2, 1, 1, 3 },
                    { 2, 1, 1, 4 },
                    { 2, 1, 1, 5 },
                    { 2, 1, 1, 6 },
                    { 2, 1, 1, 7 },
                    { 2, 1, 1, 8 },
                    { 2, 1, 1, 9 },
                    { 2, 1, 1, 10 },
                    { 2, 1, 1, 11 },
                    { 2, 1, 1, 12 },
                    { 2, 1, 1, 13 },
                    { 2, 1, 1, 14 },
                    { 2, 1, 1, 16 },
                    { 2, 1, 1, 17 },
                    { 2, 1, 1, 22 },
                    { 2, 1, 1, 34 },
                    { 2, 1, 1, 36 },
                    { 2, 1, 1, 38 },
                    { 2, 1, 1, 43 },
                    { 2, 1, 1, 44 },
                    { 3, 1, 1, 1 },
                    { 3, 1, 1, 2 },
                    { 3, 1, 1, 3 },
                    { 3, 1, 1, 4 },
                    { 3, 1, 1, 5 },
                    { 3, 1, 1, 6 },
                    { 3, 1, 1, 7 },
                    { 3, 1, 1, 8 },
                    { 3, 1, 1, 9 },
                    { 3, 1, 1, 10 },
                    { 3, 1, 1, 11 },
                    { 3, 1, 1, 12 },
                    { 3, 1, 1, 13 },
                    { 3, 1, 1, 22 },
                    { 3, 1, 1, 34 },
                    { 3, 1, 1, 36 },
                    { 3, 1, 1, 39 },
                    { 3, 1, 1, 40 },
                    { 3, 1, 1, 41 },
                    { 3, 1, 1, 42 },
                    { 3, 1, 1, 44 },
                    { 4, 1, 1, 1 },
                    { 4, 1, 1, 3 },
                    { 4, 1, 1, 4 },
                    { 4, 1, 1, 8 },
                    { 4, 1, 1, 10 },
                    { 4, 1, 1, 25 },
                    { 4, 1, 1, 31 },
                    { 4, 1, 1, 36 },
                    { 4, 1, 1, 42 },
                    { 5, 1, 1, 1 },
                    { 5, 1, 1, 3 },
                    { 5, 1, 1, 8 },
                    { 5, 1, 1, 25 },
                    { 5, 1, 1, 26 },
                    { 5, 1, 1, 27 },
                    { 5, 1, 1, 36 },
                    { 6, 1, 1, 1 },
                    { 6, 1, 1, 3 },
                    { 6, 1, 1, 34 },
                    { 6, 1, 1, 36 },
                    { 6, 1, 1, 43 },
                    { 6, 1, 1, 44 },
                    { 7, 1, 1, 1 },
                    { 7, 1, 1, 34 },
                    { 7, 1, 1, 36 },
                    { 7, 1, 1, 43 },
                    { 7, 1, 1, 44 },
                    { 8, 1, 1, 1 },
                    { 8, 1, 1, 3 },
                    { 8, 1, 1, 33 },
                    { 8, 1, 1, 34 },
                    { 8, 1, 1, 36 },
                    { 8, 1, 1, 46 },
                    { 9, 1, 1, 1 },
                    { 9, 1, 1, 29 },
                    { 9, 1, 1, 30 },
                    { 9, 1, 1, 32 },
                    { 9, 1, 1, 33 },
                    { 9, 1, 1, 34 },
                    { 9, 1, 1, 36 },
                    { 9, 1, 1, 44 },
                    { 9, 1, 1, 47 }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "JobId", "OrganizationId", "Budget", "Description", "Name", "Order" },
                values: new object[,]
                {
                    { 1, 1, 1, null, "Refinity is a digital platform that manages inventory, color, learning, and business management for body shops", "BASF-Refinity", 1 },
                    { 2, 1, 1, null, "The SRE Team oversees the quality assurance and monitoring of Microsoft's internal systems.", "Microsoft", 2 },
                    { 3, 2, 1, 500000m, "ElderKey is a sophisticated platform designed to manage the health and wellness of senior citizens.", "Elder Care Management Platform", 1 },
                    { 4, 2, 1, 500000m, "Cashflow Tactics is a platform designed to enhance the profitability of real estate investors through a diverse array of strategic approaches.", "Real Estate Accounting Platform", 2 },
                    { 5, 2, 1, 3000000m, "Associated Foods is a cooperative network that provides food distribution, warehousing, and retail support to independent grocery stores.", "Major Food Distribution System", 3 },
                    { 6, 3, 1, 500000m, null, "Professional Services Management Platform", 1 },
                    { 7, 4, 1, 500000m, "Party Center Software is the number 1 online booking and facility management tool for the family entertainment industry.", "Family Entertainment Center Platform", 1 },
                    { 8, 7, 1, 2000000m, null, "Small Collections Self-Management Platform", 1 },
                    { 9, 8, 1, 2000000m, "Cathexis is a platform that hosts extensive affiliate marketing campaigns, tracking clicks and conversions to inform business decisions.", "Affiliate Marketing Platform", 1 },
                    { 10, 8, 1, 500000m, "Monaco Classroom is a virtual learning environment that offers access to digital training materials purchased online.", "E-Learning Platform", 1 }
                });

            migrationBuilder.InsertData(
                table: "ResumeJob",
                columns: new[] { "JobId", "OrganizationId", "ResumeId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 },
                    { 3, 1, 1 },
                    { 4, 1, 1 },
                    { 5, 1, 1 },
                    { 6, 1, 1 },
                    { 7, 1, 1 },
                    { 8, 1, 1 },
                    { 9, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "ResumeSettings",
                columns: new[] { "OrganizationId", "ResumeId", "AttachAllJobs", "AttachAllSkills", "DefaultTemplateId", "ResumeYearHistory", "ShowContactInfo", "ShowDuration", "ShowRatings", "ShowTechnologyPerJob", "SkillView" },
                values: new object[] { 1, 1, null, null, null, null, null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 1, 1, 1, 2 },
                    { 1, 1, 1, 3 },
                    { 1, 1, 1, 4 },
                    { 1, 1, 1, 5 },
                    { 1, 1, 1, 6 },
                    { 1, 1, 1, 7 },
                    { 1, 1, 1, 10 },
                    { 1, 1, 1, 17 },
                    { 1, 1, 1, 34 },
                    { 1, 1, 1, 35 },
                    { 1, 1, 1, 36 },
                    { 1, 1, 1, 37 },
                    { 1, 1, 1, 38 },
                    { 1, 1, 1, 39 },
                    { 1, 1, 1, 44 },
                    { 1, 1, 1, 95 }
                });

            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "OrganizationId", "JobId", "Order", "ProjectId", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 2, "Pioneered the development and launch of mission-critical workflows, enhancing software reliability significantly." },
                    { 2, 1, 1, 2, 2, "Architected the Geneva logging library, becoming a foundational tool for the Site Reliability Engineering (SRE) department." },
                    { 3, 1, 1, 3, 2, "Crafted real-time dashboards using Geneva, handling data from millions of records for instant insights." },
                    { 4, 1, 1, 4, 2, "Engineered and deployed complex Synapse workspaces with Bicep across multiple environments, optimizing deployment processes." },
                    { 5, 1, 1, 5, 2, "Responsible for implementing integration testing patterns across multiple applications." },
                    { 10, 1, 1, 1, 1, "Spearheaded the architecture of a model MicroService, setting new standards for future developments." },
                    { 11, 1, 1, 2, 1, "Conducted hundreds of technical screenings for C# and Angular developers, refining the technical team’s capabilities." },
                    { 12, 1, 2, 1, 5, "Overhauled an antiquated system using the latest .NET technologies, greatly improving performance and scalability." },
                    { 13, 1, 2, 2, 5, "Seamlessly integrated an existing SQL Server database with EF Core, optimizing data operations and efficiency." },
                    { 14, 1, 2, 3, 5, "Developed a robust gRPC-based messaging system using C# WebAPI, enhancing inter-service communication." },
                    { 15, 1, 1, 5, 1, "Introduced advanced patterns for unit and integration testing, achieving 90% code coverage and setting a high standard for code reliability." },
                    { 20, 1, 2, 1, 3, "Responsible for the architecture and development of the .NET Core backend." },
                    { 21, 1, 2, 2, 3, "Developed advanced medication and activity scheduling systems to accommodate complex care requirements." },
                    { 22, 1, 2, 3, 3, "Made critical technology decisions that kept the project on track and within budgetary constraints." },
                    { 23, 1, 2, 4, 3, "Maintained a code coverage of 90%, ensuring high reliability and quality of the software." },
                    { 24, 1, 2, 1, 4, "Responsible for the entire backend and frontend architecture and development, including a .NET Core backend and Angular frontend." },
                    { 25, 1, 2, 2, 4, "Developed a comprehensive single-entry accounting platform that calculates cash flow, tax savings, appreciation, and capital inflow." },
                    { 26, 1, 2, 4, 4, "Maintained 75% code coverage." },
                    { 27, 1, 2, 3, 4, "Made key technology decisions to ensure the project remained on schedule and within budget." },
                    { 30, 1, 1, 2, 2, "Optimized ChatGPT training models to meet internal business needs." },
                    { 34, 1, 3, 1, 6, "Developed a role-based business model that facilitates complex, multi-partner payouts from hours billed." },
                    { 36, 1, 3, 3, 6, "Developed integrations with the Stripe payment system, PandaDoc document signing, and various other APIs." },
                    { 41, 1, 4, 1, 7, "Oversaw and managed the complete backend development process." },
                    { 42, 1, 4, 2, 7, "Integrated with multiple bowling and card-reading systems to enhance overall system functionality." },
                    { 43, 1, 4, 3, 7, "Developed a fully-featured order and scheduling system." },
                    { 48, 1, 7, 1, 8, "Developed tools to facilitate the indexing of library archives." },
                    { 50, 1, 7, 3, 8, "Collaborated as a member of an 8-person development team utilizing Agile/Scrum methodologies." },
                    { 51, 1, 7, 4, 8, "Maintained 100% code coverage." },
                    { 53, 1, 8, 1, 9, "Oversaw the architecture, development, and deployment of a comprehensive affiliate marketing platform." },
                    { 54, 1, 8, 1, 10, "Directly managed a team of up to 9 developers utilizing Agile Scrum methodologies." },
                    { 55, 1, 8, 2, 10, "Oversaw the architecture, development, and deployment of a comprehensive e-learning platform." },
                    { 56, 1, 8, 2, 9, "Ensured 99% uptime to maintain smooth business operations." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certification_OrganizationId_PersonaId",
                table: "Certification",
                columns: new[] { "OrganizationId", "PersonaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Country_Iso2",
                table: "Country",
                column: "Iso2");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Iso3",
                table: "Country",
                column: "Iso3");

            migrationBuilder.CreateIndex(
                name: "IX_Degree_OrganizationId_SchoolId",
                table: "Degree",
                columns: new[] { "OrganizationId", "SchoolId" });

            migrationBuilder.CreateIndex(
                name: "IX_Highlight_OrganizationId_JobId",
                table: "Highlight",
                columns: new[] { "OrganizationId", "JobId" });

            migrationBuilder.CreateIndex(
                name: "IX_Highlight_ProjectId_JobId",
                table: "Highlight",
                columns: new[] { "ProjectId", "JobId" });

            migrationBuilder.CreateIndex(
                name: "IX_Job_OrganizationId_PersonaId",
                table: "Job",
                columns: new[] { "OrganizationId", "PersonaId" });

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_OrganizationId_JobId",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "JobId" });

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_OrganizationId_PersonaId_SkillId",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "PersonaId", "SkillId" });

            migrationBuilder.CreateIndex(
                name: "IX_Persona_StateId",
                table: "Persona",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaLanguage_Code3",
                table: "PersonaLanguage",
                column: "Code3");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaSkill_SkillId",
                table: "PersonaSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_OrganizationId_JobId",
                table: "Project",
                columns: new[] { "OrganizationId", "JobId" });

            migrationBuilder.CreateIndex(
                name: "IX_Rendering_TemplateId",
                table: "Rendering",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeJob_OrganizationId_JobId",
                table: "ResumeJob",
                columns: new[] { "OrganizationId", "JobId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSettings_DefaultTemplateId",
                table: "ResumeSettings",
                column: "DefaultTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkill_OrganizationId_PersonaId_SkillId",
                table: "ResumeSkill",
                columns: new[] { "OrganizationId", "PersonaId", "SkillId" });

            migrationBuilder.CreateIndex(
                name: "IX_School_OrganizationId_PersonaId",
                table: "School",
                columns: new[] { "OrganizationId", "PersonaId" });

            migrationBuilder.CreateIndex(
                name: "IX_SkillCategorySkill_SkillId",
                table: "SkillCategorySkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_StateProvince_Iso2",
                table: "StateProvince",
                column: "Iso2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropTable(
                name: "Degree");

            migrationBuilder.DropTable(
                name: "Highlight");

            migrationBuilder.DropTable(
                name: "JobSkill");

            migrationBuilder.DropTable(
                name: "PersonaLanguage");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropTable(
                name: "Rendering");

            migrationBuilder.DropTable(
                name: "ResumeJob");

            migrationBuilder.DropTable(
                name: "ResumeSettings");

            migrationBuilder.DropTable(
                name: "ResumeSkill");

            migrationBuilder.DropTable(
                name: "SkillCategorySkill");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "OrganizationSettings");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "PersonaSkill");

            migrationBuilder.DropTable(
                name: "Resume");

            migrationBuilder.DropTable(
                name: "SkillCategory");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "StateProvince");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}

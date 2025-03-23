using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Infrastructure.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
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
                    CapsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iso3 = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
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
                    NativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code2 = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Sequence",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequence", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "varchar(64)", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    Format = table.Column<string>(type: "varchar(64)", nullable: false),
                    Engine = table.Column<string>(type: "varchar(64)", nullable: false)
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
                    Iso2 = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbrev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillCategorySkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(64)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(64)", nullable: false),
                    Email = table.Column<string>(type: "varchar(64)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(64)", nullable: false),
                    LinkedIn = table.Column<string>(type: "varchar(64)", nullable: true),
                    GitHub = table.Column<string>(type: "varchar(64)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Body = table.Column<string>(type: "varchar(255)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => new { x.OrganizationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Certification_Persona_OrganizationId_PersonId",
                        columns: x => new { x.OrganizationId, x.PersonId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(255)", nullable: false),
                    Location = table.Column<string>(type: "varchar(255)", nullable: true),
                    Description = table.Column<string>(type: "varchar(1024)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => new { x.OrganizationId, x.PersonId, x.Id });
                    table.UniqueConstraint("AK_Company_OrganizationId_Id", x => new { x.OrganizationId, x.Id });
                    table.UniqueConstraint("AK_Company_OrganizationId_Id_PersonId", x => new { x.OrganizationId, x.Id, x.PersonId });
                    table.ForeignKey(
                        name: "FK_Company_Persona_OrganizationId_PersonId",
                        columns: x => new { x.OrganizationId, x.PersonId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "PersonaLanguage",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Code3 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Proficiency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaLanguage", x => new { x.OrganizationId, x.PersonId, x.Code3 });
                    table.ForeignKey(
                        name: "FK_PersonaLanguage_Language_Code3",
                        column: x => x.Code3,
                        principalTable: "Language",
                        principalColumn: "Code3");
                    table.ForeignKey(
                        name: "FK_PersonaLanguage_Persona_OrganizationId_PersonId",
                        columns: x => new { x.OrganizationId, x.PersonId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "PersonaSkill",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaSkill", x => new { x.OrganizationId, x.PersonId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_PersonaSkill_Persona_OrganizationId_PersonId",
                        columns: x => new { x.OrganizationId, x.PersonId },
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
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "varchar(1024)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference", x => new { x.OrganizationId, x.PersonId, x.Id });
                    table.ForeignKey(
                        name: "FK_Reference_Persona_OrganizationId_PersonId",
                        columns: x => new { x.OrganizationId, x.PersonId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "Resume",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resume", x => new { x.OrganizationId, x.PersonId, x.Id });
                    table.UniqueConstraint("AK_Resume_OrganizationId_Id", x => new { x.OrganizationId, x.Id });
                    table.UniqueConstraint("AK_Resume_OrganizationId_Id_PersonId", x => new { x.OrganizationId, x.Id, x.PersonId });
                    table.ForeignKey(
                        name: "FK_Resume_Persona_OrganizationId_PersonId",
                        columns: x => new { x.OrganizationId, x.PersonId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => new { x.OrganizationId, x.Id });
                    table.ForeignKey(
                        name: "FK_School_Persona_OrganizationId_PersonId",
                        columns: x => new { x.OrganizationId, x.PersonId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JobTitle = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.Id });
                    table.ForeignKey(
                        name: "FK_Position_Company_OrganizationId_PersonId_CompanyId",
                        columns: x => new { x.OrganizationId, x.PersonId, x.CompanyId },
                        principalTable: "Company",
                        principalColumns: new[] { "OrganizationId", "PersonId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "CompanySkill",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySkill", x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CompanySkill_Company_OrganizationId_CompanyId",
                        columns: x => new { x.OrganizationId, x.CompanyId },
                        principalTable: "Company",
                        principalColumns: new[] { "OrganizationId", "Id" });
                    table.ForeignKey(
                        name: "FK_CompanySkill_PersonaSkill_OrganizationId_PersonId_SkillId",
                        columns: x => new { x.OrganizationId, x.PersonId, x.SkillId },
                        principalTable: "PersonaSkill",
                        principalColumns: new[] { "OrganizationId", "PersonId", "SkillId" });
                });

            migrationBuilder.CreateTable(
                name: "Rendering",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    RenderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rendering", x => new { x.OrganizationId, x.ResumeId, x.TemplateId });
                    table.ForeignKey(
                        name: "FK_Rendering_Resume_OrganizationId_ResumeId",
                        columns: x => new { x.OrganizationId, x.ResumeId },
                        principalTable: "Resume",
                        principalColumns: new[] { "OrganizationId", "Id" });
                    table.ForeignKey(
                        name: "FK_Rendering_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResumeCompany",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeCompany", x => new { x.OrganizationId, x.ResumeId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_ResumeCompany_Company_OrganizationId_CompanyId_PersonId",
                        columns: x => new { x.OrganizationId, x.CompanyId, x.PersonId },
                        principalTable: "Company",
                        principalColumns: new[] { "OrganizationId", "Id", "PersonId" });
                    table.ForeignKey(
                        name: "FK_ResumeCompany_Resume_OrganizationId_ResumeId_PersonId",
                        columns: x => new { x.OrganizationId, x.ResumeId, x.PersonId },
                        principalTable: "Resume",
                        principalColumns: new[] { "OrganizationId", "Id", "PersonId" });
                });

            migrationBuilder.CreateTable(
                name: "ResumeSettings",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ResumeSettings", x => new { x.OrganizationId, x.ResumeId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_ResumeSettings_OrganizationSettings_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationSettings",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_ResumeSettings_Resume_OrganizationId_ResumeId_PersonId",
                        columns: x => new { x.OrganizationId, x.ResumeId, x.PersonId },
                        principalTable: "Resume",
                        principalColumns: new[] { "OrganizationId", "Id", "PersonId" });
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
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSkill", x => new { x.OrganizationId, x.PersonId, x.ResumeId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_ResumeSkill_PersonaSkill_OrganizationId_PersonId_SkillId",
                        columns: x => new { x.OrganizationId, x.PersonId, x.SkillId },
                        principalTable: "PersonaSkill",
                        principalColumns: new[] { "OrganizationId", "PersonId", "SkillId" });
                    table.ForeignKey(
                        name: "FK_ResumeSkill_Resume_OrganizationId_PersonId_ResumeId",
                        columns: x => new { x.OrganizationId, x.PersonId, x.ResumeId },
                        principalTable: "Resume",
                        principalColumns: new[] { "OrganizationId", "PersonId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "Degree",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degree", x => new { x.OrganizationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Degree_School_OrganizationId_SchoolId",
                        columns: x => new { x.OrganizationId, x.SchoolId },
                        principalTable: "School",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "Highlight",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "varchar(512)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highlight", x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Highlight_Company_OrganizationId_PersonId_CompanyId",
                        columns: x => new { x.OrganizationId, x.PersonId, x.CompanyId },
                        principalTable: "Company",
                        principalColumns: new[] { "OrganizationId", "PersonId", "Id" });
                    table.ForeignKey(
                        name: "FK_Highlight_Persona_OrganizationId_PersonId",
                        columns: x => new { x.OrganizationId, x.PersonId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                    table.ForeignKey(
                        name: "FK_Highlight_Position_OrganizationId_PersonId_CompanyId_PositionId",
                        columns: x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId },
                        principalTable: "Position",
                        principalColumns: new[] { "OrganizationId", "PersonId", "CompanyId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(512)", nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Project_Company_OrganizationId_PersonId_CompanyId",
                        columns: x => new { x.OrganizationId, x.PersonId, x.CompanyId },
                        principalTable: "Company",
                        principalColumns: new[] { "OrganizationId", "PersonId", "Id" });
                    table.ForeignKey(
                        name: "FK_Project_Position_OrganizationId_PersonId_CompanyId_PositionId",
                        columns: x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId },
                        principalTable: "Position",
                        principalColumns: new[] { "OrganizationId", "PersonId", "CompanyId", "Id" });
                });

            migrationBuilder.CreateTable(
                name: "ProjectHighlight",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "varchar(512)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectHighlight", x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId, x.ProjectId, x.Id });
                    table.ForeignKey(
                        name: "FK_ProjectHighlight_Project_OrganizationId_PersonId_CompanyId_PositionId_ProjectId",
                        columns: x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId, x.ProjectId },
                        principalTable: "Project",
                        principalColumns: new[] { "OrganizationId", "PersonId", "CompanyId", "PositionId", "Id" });
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
                values: new object[] { 1, true, true, 1, 10, true, true, false, true, 1 });

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
                    { 1, "handlebars", "html", "Modern Resume", null, "<html><body>Modern Template</body></html>" },
                    { 2, "handlebars", "html", "Classic Resume", null, "<html><body>Classic Template</body></html>" }
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
                values: new object[] { 1, 1, "New York", "alice.smith@example.com", "Alice", "https://www.github.com/alicesmith", false, "Smith", "https://www.linkedin.com/in/alicesmith", "(123) 456-7890", 33 });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "OrganizationId", "PersonId", "CompanyName", "Description", "EndDate", "Location", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, 1, "Tech Innovations Inc.", "Leading software development company specializing in cloud solutions", new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seattle WA", new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, 1, "Digital Solutions LLC", "Startup focused on mobile application development", new DateTime(2017, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "San Francisco CA", new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 1, 1, "Enterprise Systems Corp.", "Enterprise software solutions provider", new DateTime(2015, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "New York NY", new DateTime(2012, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "PersonaLanguage",
                columns: new[] { "Code3", "OrganizationId", "PersonId", "Proficiency" },
                values: new object[,]
                {
                    { "eng", 1, 1, 5 },
                    { "fra", 1, 1, 2 },
                    { "spa", 1, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "PersonaSkill",
                columns: new[] { "OrganizationId", "PersonId", "SkillId" },
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
                columns: new[] { "Id", "OrganizationId", "PersonId", "Name", "Order", "PhoneNumber", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, "John Smith", 1, "(555) 123-4567", "Excellent team player with strong technical skills" },
                    { 2, 1, 1, "Sarah Johnson", 2, "(555) 987-6543", "Innovative problem solver who consistently delivers high-quality work" },
                    { 3, 1, 1, "Michael Brown", 3, "(555) 456-7890", "Dedicated professional with exceptional leadership abilities" }
                });

            migrationBuilder.InsertData(
                table: "Resume",
                columns: new[] { "Id", "OrganizationId", "PersonId", "Description", "JobTitle" },
                values: new object[,]
                {
                    { 1, 1, 1, "Experienced software engineer with expertise in cloud technologies and distributed systems", "Senior Software Engineer" },
                    { 2, 1, 1, "Versatile developer with skills in both frontend and backend technologies", "Full Stack Developer" },
                    { 3, 1, 1, "Specialist in automation and infrastructure as code", "DevOps Engineer" }
                });

            migrationBuilder.InsertData(
                table: "School",
                columns: new[] { "Id", "OrganizationId", "EndDate", "Location", "Name", "PersonId", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2014, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seattle", "University of Washington", 1, new DateTime(2010, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, new DateTime(2016, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Palo Alto", "Stanford University", 1, new DateTime(2014, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "CompanySkill",
                columns: new[] { "CompanyId", "OrganizationId", "PersonId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 1, 1, 1, 2 },
                    { 1, 1, 1, 3 },
                    { 1, 1, 1, 4 },
                    { 1, 1, 1, 5 },
                    { 2, 1, 1, 1 },
                    { 2, 1, 1, 6 },
                    { 2, 1, 1, 7 },
                    { 3, 1, 1, 1 },
                    { 3, 1, 1, 8 },
                    { 3, 1, 1, 9 }
                });

            migrationBuilder.InsertData(
                table: "Degree",
                columns: new[] { "Id", "OrganizationId", "Name", "Order", "SchoolId" },
                values: new object[,]
                {
                    { 1, 1, "Bachelor of Science in Computer Science", 1, 1 },
                    { 2, 1, "Master of Science in Computer Science", 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "CompanyId", "Id", "OrganizationId", "PersonId", "EndDate", "JobTitle", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Senior Software Engineer", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 1, 2, 1, 1, new DateTime(2019, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Software Engineer", new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, 1, 1, new DateTime(2017, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Full Stack Developer", new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 1, 1, 1, new DateTime(2015, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Junior Developer", new DateTime(2012, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "ResumeCompany",
                columns: new[] { "CompanyId", "OrganizationId", "ResumeId", "PersonId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 1, 1 },
                    { 1, 1, 2, 1 },
                    { 3, 1, 2, 1 },
                    { 2, 1, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "ResumeSettings",
                columns: new[] { "OrganizationId", "PersonId", "ResumeId", "AttachAllJobs", "AttachAllSkills", "DefaultTemplateId", "ResumeYearHistory", "ShowContactInfo", "ShowDuration", "ShowRatings", "ShowTechnologyPerJob", "SkillView" },
                values: new object[,]
                {
                    { 1, 1, 1, true, true, 1, 10, true, true, false, true, 1 },
                    { 1, 1, 2, true, true, 1, 5, true, true, true, false, 2 },
                    { 1, 1, 3, false, true, 1, 7, true, false, false, true, 1 }
                });

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "OrganizationId", "PersonId", "ResumeId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 1, 1, 1, 2 },
                    { 1, 1, 1, 3 },
                    { 1, 1, 1, 4 },
                    { 1, 1, 1, 5 },
                    { 1, 1, 2, 1 },
                    { 1, 1, 2, 6 },
                    { 1, 1, 2, 7 },
                    { 1, 1, 3, 2 },
                    { 1, 1, 3, 8 },
                    { 1, 1, 3, 9 }
                });

            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "CompanyId", "Id", "OrganizationId", "PersonId", "PositionId", "Order", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1, 1, "Led team of 5 developers implementing microservices architecture" },
                    { 1, 2, 1, 1, 1, 2, "Implemented automated testing increasing code coverage to 85%" },
                    { 1, 1, 1, 1, 2, 1, "Optimized database queries reducing response time by 50%" },
                    { 2, 1, 1, 1, 1, 1, "Developed mobile-first responsive design framework" },
                    { 3, 1, 1, 1, 1, 1, "Implemented logging and monitoring system for production environment" }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "CompanyId", "Id", "OrganizationId", "PersonId", "PositionId", "Budget", "Description", "Name", "Order" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1, 250000m, "Migrated legacy systems to AWS cloud infrastructure", "Cloud Migration", 1 },
                    { 1, 2, 1, 1, 1, 150000m, "Redesigned and implemented RESTful APIs", "API Modernization", 2 },
                    { 1, 1, 1, 1, 2, 100000m, "Developed cross-platform mobile application", "Mobile App Development", 1 },
                    { 2, 1, 1, 1, 1, 200000m, "Built scalable e-commerce solution", "E-commerce Platform", 1 },
                    { 3, 1, 1, 1, 1, 75000m, "Integrated Salesforce with internal systems", "CRM Integration", 1 }
                });

            migrationBuilder.InsertData(
                table: "ProjectHighlight",
                columns: new[] { "CompanyId", "Id", "OrganizationId", "PersonId", "PositionId", "ProjectId", "Order", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1, 1, 1, "Reduced infrastructure costs by 40% through efficient cloud architecture" },
                    { 1, 2, 1, 1, 1, 1, 2, "Implemented automated CI/CD pipeline reducing deployment time by 70%" },
                    { 1, 1, 1, 1, 1, 2, 1, "Increased API performance by 60% through caching and optimization" },
                    { 1, 2, 1, 1, 1, 2, 2, "Implemented comprehensive API documentation with Swagger" },
                    { 1, 1, 1, 1, 2, 1, 1, "Developed UI components library used across multiple projects" },
                    { 2, 1, 1, 1, 1, 1, 1, "Implemented payment processing system supporting multiple gateways" },
                    { 3, 1, 1, 1, 1, 1, 1, "Reduced data synchronization errors by 90% through robust error handling" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certification_OrganizationId_PersonId",
                table: "Certification",
                columns: new[] { "OrganizationId", "PersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanySkill_OrganizationId_CompanyId",
                table: "CompanySkill",
                columns: new[] { "OrganizationId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanySkill_OrganizationId_PersonId_SkillId",
                table: "CompanySkill",
                columns: new[] { "OrganizationId", "PersonId", "SkillId" });

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
                name: "IX_Rendering_TemplateId",
                table: "Rendering",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeCompany_OrganizationId_CompanyId_PersonId",
                table: "ResumeCompany",
                columns: new[] { "OrganizationId", "CompanyId", "PersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResumeCompany_OrganizationId_ResumeId_PersonId",
                table: "ResumeCompany",
                columns: new[] { "OrganizationId", "ResumeId", "PersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSettings_DefaultTemplateId",
                table: "ResumeSettings",
                column: "DefaultTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkill_OrganizationId_PersonId_SkillId",
                table: "ResumeSkill",
                columns: new[] { "OrganizationId", "PersonId", "SkillId" });

            migrationBuilder.CreateIndex(
                name: "IX_School_OrganizationId_PersonId",
                table: "School",
                columns: new[] { "OrganizationId", "PersonId" });

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
                name: "CompanySkill");

            migrationBuilder.DropTable(
                name: "Degree");

            migrationBuilder.DropTable(
                name: "Highlight");

            migrationBuilder.DropTable(
                name: "PersonaLanguage");

            migrationBuilder.DropTable(
                name: "ProjectHighlight");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropTable(
                name: "Rendering");

            migrationBuilder.DropTable(
                name: "ResumeCompany");

            migrationBuilder.DropTable(
                name: "ResumeSettings");

            migrationBuilder.DropTable(
                name: "ResumeSkill");

            migrationBuilder.DropTable(
                name: "Sequence");

            migrationBuilder.DropTable(
                name: "SkillCategorySkill");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Project");

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
                name: "Position");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "StateProvince");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}

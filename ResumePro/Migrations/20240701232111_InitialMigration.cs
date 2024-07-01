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
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "School",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.Id);
                    table.ForeignKey(
                        name: "FK_School_Persona_PersonaId",
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
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
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
                    JobId = table.Column<int>(type: "int", nullable: false)
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
                name: "Degree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degree", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Degree_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeSkill",
                columns: table => new
                {
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    ShowInSummary = table.Column<bool>(type: "bit", nullable: false)
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
                name: "Highlight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Highlight_Project_ProjectId_JobId",
                        columns: x => new { x.ProjectId, x.JobId },
                        principalTable: "Project",
                        principalColumns: new[] { "Id", "JobId" });
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
                values: new object[] { 1, "Salt Lake City", "rodmjay@gmail.com", "Rod", "https://www.github.com/rodmjay", "Johnson", "https://www.linkedin.com/in/rodmjay", "(385) 352-6026", "UT" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "C# Development" },
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
                    { 28, "Bluetooth Low Energy (BLE)" },
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
                    { 47, "XAML" }
                });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "Id", "Company", "Description", "EndDate", "Location", "PersonaId", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, "InfoSys", null, null, "Salt Lake City,UT", 1, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Technical Architect" },
                    { 2, "Solution Stream", null, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "American Fork,UT", 1, new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sr. Software Architect" },
                    { 3, "IdeaFortune", null, new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "American Fork,UT", 1, new DateTime(2017, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Founder/Architect" },
                    { 4, "Agile Software", null, new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sacramento,CA", 1, new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect" },
                    { 5, "Access Softek", null, new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "West Jordan,UT", 1, new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sr. Engineer Dev Lead" },
                    { 6, "NETCHEX", null, new DateTime(2013, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Louisiana", 1, new DateTime(2012, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect Consultant" },
                    { 7, "Ancestry.com", null, new DateTime(2012, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Provo,UT", 1, new DateTime(2010, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sr. Engineer" },
                    { 8, "Cathexis", null, new DateTime(2010, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Provo,UT", 1, new DateTime(2008, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect/Dev Manager" },
                    { 9, "Motorola Public Safety", null, new DateTime(2008, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salt Lake City,UT", 1, new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineer" }
                });

            migrationBuilder.InsertData(
                table: "PersonaSkill",
                columns: new[] { "PersonaId", "SkillId", "Rating" },
                values: new object[,]
                {
                    { 1, 1, 10 },
                    { 1, 2, 9 },
                    { 1, 3, 9 },
                    { 1, 4, 9 },
                    { 1, 5, 10 },
                    { 1, 6, 8 },
                    { 1, 7, 9 },
                    { 1, 8, 8 },
                    { 1, 9, 10 },
                    { 1, 10, 8 },
                    { 1, 11, 8 },
                    { 1, 12, 8 },
                    { 1, 13, 8 },
                    { 1, 14, 8 },
                    { 1, 15, 8 },
                    { 1, 16, 8 },
                    { 1, 17, 8 },
                    { 1, 18, 8 },
                    { 1, 19, 8 },
                    { 1, 20, 8 },
                    { 1, 21, 8 },
                    { 1, 22, 10 },
                    { 1, 23, 8 },
                    { 1, 24, 8 },
                    { 1, 25, 8 },
                    { 1, 26, 8 },
                    { 1, 27, 8 },
                    { 1, 28, 8 },
                    { 1, 29, 8 },
                    { 1, 30, 8 },
                    { 1, 31, 10 },
                    { 1, 32, 8 },
                    { 1, 33, 8 },
                    { 1, 34, 10 },
                    { 1, 35, 8 },
                    { 1, 36, 9 },
                    { 1, 37, 9 },
                    { 1, 38, 10 },
                    { 1, 39, 10 },
                    { 1, 40, 10 },
                    { 1, 41, 10 },
                    { 1, 42, 10 },
                    { 1, 43, 10 },
                    { 1, 44, 10 },
                    { 1, 45, 8 },
                    { 1, 46, 5 },
                    { 1, 47, 5 }
                });

            migrationBuilder.InsertData(
                table: "Resume",
                columns: new[] { "Id", "Description", "JobTitle", "PersonaId" },
                values: new object[] { 1, "Rod is an enterprise architect with deep expertise in the latest .NET and web technologies. With 19 years of experience as a professional developer and architect, he has mastered the complete software development lifecycle, from ideation to implementation. Rod is frequently praised as a 10x developer, consistently delivering high-end software solutions from the ground up.", "Enterprise Application Architect", 1 });

            migrationBuilder.InsertData(
                table: "School",
                columns: new[] { "Id", "EndDate", "Name", "PersonaId", "StartDate" },
                values: new object[] { 1, new DateTime(2005, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portland Community College", 1, new DateTime(2004, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Degree",
                columns: new[] { "Id", "Name", "SchoolId" },
                values: new object[] { 1, "AAS Computer and Information Systems", 1 });

            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "JobId", "Order", "ProjectId", "Text" },
                values: new object[,]
                {
                    { 6, 2, 1, null, "Company-wide leader for .NET initiatives." },
                    { 7, 2, 2, null, "Developed the technical screening process and conducted numerous technical interviews." },
                    { 8, 2, 3, null, "Developed the common codebase utilized for all .NET projects." },
                    { 9, 2, 4, null, "Received the award for highest-producing developer in 2021." },
                    { 17, 5, 1, null, "Managed four developers directly using Agile Scrum methodologies." },
                    { 18, 5, 2, null, "Responsible for the architecture and development of components within a banking platform." },
                    { 19, 5, 3, null, "Developed Fingerprint Login and Friends and Family Shared Banking components." },
                    { 28, 9, 1, null, "Developed tools using C#, WCF, and XAML to support the Miami-Dade Police Department." },
                    { 29, 9, 2, null, "Received extensive training in .NET, JavaScript, and WCF directly from Microsoft." },
                    { 31, 3, 1, null, "Responsible for the ideation, design, and delivery of a comprehensive professional services platform." },
                    { 32, 3, 2, null, "Raised startup capital to fund company operations for three years." },
                    { 33, 3, 3, null, "Managed a team of five developers to implement the flagship product." },
                    { 37, 4, 1, null, "Reconstructed the flagship product, transitioning it from ASP to the .NET Framework to modernize infrastructure and improve scalability." },
                    { 38, 4, 2, null, "Developed a comprehensive order and scheduling system with full functionality." },
                    { 39, 4, 3, null, "Recruited, interviewed, and mentored developers at various levels, fostering a skilled and cohesive team." },
                    { 40, 4, 4, null, "Developed a comprehensive DevOps strategy to ensure compliance with international regulations." },
                    { 44, 6, 1, null, "Redesigned the flagship product by integrating advanced technology to enhance performance and functionality." },
                    { 45, 6, 2, null, "Collaborated directly with the onsite team, providing mentorship to enhance skills and foster professional growth." },
                    { 46, 6, 3, null, "Engineered numerous innovative features into the existing platform, significantly enhancing its capabilities and user experience." },
                    { 47, 6, 4, null, "Specialized expertise in PCI Compliance and payment processing systems." },
                    { 49, 7, 2, null, "Spearheaded companywide adoption and training of jQuery." }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "JobId", "Budget", "Description", "Name", "Order" },
                values: new object[,]
                {
                    { 1, 1, null, "Refinity is a digital platform that manages inventory, color, learning, and business management for body shops", "BASF-Refinity", 1 },
                    { 2, 1, null, "SRE Team manages the overal qualtiy and monitorying of internal microsoft systems", "Microsoft", 2 },
                    { 3, 2, 500000m, "ElderCare is a platform that manages the health and wellness of senior citizens", "Elder Care Management Platform", 1 },
                    { 4, 2, 500000m, null, "Real Estate Accounting Platform", 2 },
                    { 5, 2, 3000000m, null, "Major Food Distribution System", 3 },
                    { 6, 3, 500000m, null, "Professional Services Management Platform", 1 },
                    { 7, 4, 500000m, null, "Family Entertainment Center Platform", 1 },
                    { 8, 7, 2000000m, null, "Small Collections Self-Management Platform", 1 },
                    { 9, 8, 2000000m, null, "Affiliate Marketing Platform", 1 },
                    { 10, 8, 500000m, null, "E-Learning Platform", 1 }
                });

            migrationBuilder.InsertData(
                table: "Reference",
                columns: new[] { "Id", "JobId", "Name", "PhoneNumber", "Text" },
                values: new object[,]
                {
                    { 1, 1, "Joseph Cotton", null, "I had the opportunity to work with Rod as a fellow solutions architect at Kahoa. Rod was leading a team of developers to meet client software needs. During my time there, Rod was not only an outstanding project architect and team lead, but an outstanding individual contributor as well. He was still able to contribute just as much as the rest of his team did despite his additional leadership responsibilities.  Rod was also a central contributing figure to company architectural principles as a whole. He was was a senior member of Kahoa's architecture community, and helped guide discussions around software standards and best practices for the company as a whole. Rod is an excellent architect and software engineer. He has the ability to lead teams and projects, and has the grit to get them over the line when it's needed. I very highly recommend him to anyone seeking an outstanding software architect or team lead." },
                    { 2, 2, "Cameo Doran", null, "I worked with Rod when he was recruited as the lead architect for a complicated financial SaaS product for an important client.\\Rod quickly impressed me with his ability to put himself in the clients shoes and build creative solutions that were focused on bringing the most value for the smallest cost.\\He also understands how to leverage the chosen technology for great results. Many a time I was impressed with recommendations he made that were far beyond what anyone else had considered.\\Rod’s experience and skill as an architect and engineering leader allowed us to place him on critical client projects and trust that he would delight the client and lead the team successfully.\\It was a pleasure to work with such a talented mind. Rod will add experience and technical leadership to any company." },
                    { 5, 2, "Rob Atlas", null, "Recently, my startup worked with Rod on the development of our MVP (Minimal Viable Product) offering. I have worked with 100's of software developers over my career. Rod is one of the most talented and efficient architects/developers with whom I have been associated. I highly recommend him as a designer and implementer of complex or sophisticated software." },
                    { 6, 2, "Robert Clymer", null, "If you want someone who can have high bandwidth conversations about the best way to design something, and then have that person accurately implement the agreed upon ideas as 5X the speed of a typical developer, Rod is your guy." },
                    { 7, 4, "Daniel Schulz", null, "Rod is a brilliant developer and a hard worker. He literally saved our project as I added him in the last hours as his expertise directed us to deliver. He certainly says up with the latest technologies, is a very fast learner, and was able to lead us in them. I highly recommend him." },
                    { 8, 7, "Ryan Done", null, "I worked with Rod on a complex web project at Ancestry.com. Rod made a big difference in the success of our project by finding great solutions and sharing different ways of looking at problems. He is sharp, knowledgeable and a great team player." },
                    { 9, 7, "Gregg B. Jensen", null, "Rod is a very skilled, and well rounded professional in web development. He is committed to success in all of his projects, and makes sure to deliver more than whats expected. His is a great asset to any team, and is an excellent team member." }
                });

            migrationBuilder.InsertData(
                table: "ResumeJob",
                columns: new[] { "JobId", "PersonaId", "ResumeId" },
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
                table: "ResumeSkill",
                columns: new[] { "PersonaId", "ResumeId", "SkillId", "ShowInSummary" },
                values: new object[,]
                {
                    { 1, 1, 1, true },
                    { 1, 1, 2, true },
                    { 1, 1, 3, true },
                    { 1, 1, 4, true },
                    { 1, 1, 5, true },
                    { 1, 1, 6, false },
                    { 1, 1, 7, false },
                    { 1, 1, 8, false },
                    { 1, 1, 9, false },
                    { 1, 1, 10, false },
                    { 1, 1, 11, false },
                    { 1, 1, 12, false },
                    { 1, 1, 13, false },
                    { 1, 1, 14, false },
                    { 1, 1, 15, false },
                    { 1, 1, 16, false },
                    { 1, 1, 17, false },
                    { 1, 1, 18, false },
                    { 1, 1, 19, false },
                    { 1, 1, 20, false },
                    { 1, 1, 21, false },
                    { 1, 1, 22, false },
                    { 1, 1, 23, false },
                    { 1, 1, 24, false },
                    { 1, 1, 25, false },
                    { 1, 1, 26, false },
                    { 1, 1, 27, false },
                    { 1, 1, 28, false },
                    { 1, 1, 29, false },
                    { 1, 1, 30, false },
                    { 1, 1, 31, false },
                    { 1, 1, 32, false },
                    { 1, 1, 33, false },
                    { 1, 1, 34, true },
                    { 1, 1, 35, true },
                    { 1, 1, 36, true },
                    { 1, 1, 37, true },
                    { 1, 1, 38, true },
                    { 1, 1, 39, false },
                    { 1, 1, 40, false },
                    { 1, 1, 41, false },
                    { 1, 1, 42, false },
                    { 1, 1, 43, false },
                    { 1, 1, 44, true },
                    { 1, 1, 45, false },
                    { 1, 1, 46, false },
                    { 1, 1, 47, false }
                });

            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "JobId", "Order", "ProjectId", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, 2, "Oversaw the development and deployment of mission-critical workflows to enhance software reliability" },
                    { 2, 1, 2, 2, "Developed the Geneva logging library utilized by the Site Reliability Engineering (SRE) department." },
                    { 3, 1, 3, 2, "Developed dashboards utilizing Geneva to display real-time data from millions of records." },
                    { 4, 1, 4, 2, "Utilized Bicep to deploy complex Synapse workspaces across multiple environments." },
                    { 5, 1, 5, 2, "Responsible for implementing integration testing patterns across multiple applications." },
                    { 10, 1, 1, 1, "Responsible for the development and architecture of a new MicroService that served as a model for subsequent microservices." },
                    { 11, 1, 2, 1, "Conducted technical screenings for hundreds of C# and Angular developers." },
                    { 12, 2, 1, 5, "Responsible for the re-architecture and development of an antiquated system using the latest .NET technologies." },
                    { 13, 2, 2, 5, "Integrated an existing SQL Server database with EF Core." },
                    { 14, 2, 3, 5, "Developed a gRPC-based messaging system using C# WebAPI." },
                    { 15, 1, 5, 1, "Introduced enhanced patterns for unit and integration testing frameworks." },
                    { 16, 1, 6, 1, "Maintaned 90% code coverage." },
                    { 20, 2, 4, 3, "Responsible for the architecture and development of the .NET Core backend." },
                    { 21, 2, 2, 3, "Developed complex medication and recurring activity scheduling systems." },
                    { 22, 2, 3, 3, "Made key technology decisions to ensure the project remained on schedule and within budget." },
                    { 23, 2, 4, 3, "Maintained 90% code coverage." },
                    { 24, 2, 1, 4, "Responsible for the entire backend and frontend architecture and development, including a .NET Core backend and Angular frontend." },
                    { 25, 2, 2, 4, "Developed a comprehensive single-entry accounting platform that calculates cash flow, tax savings, appreciation, and capital inflow." },
                    { 26, 2, 4, 4, "Maintained 75% code coverage." },
                    { 27, 2, 3, 4, "Made key technology decisions to ensure the project remained on schedule and within budget." },
                    { 30, 1, 2, 2, "Optimized ChatGPT training models to meet internal business needs." },
                    { 34, 3, 1, 6, "Developed a role-based business model that facilitates complex, multi-partner payouts from hours billed." },
                    { 36, 3, 3, 6, "Developed integrations with the Stripe payment system, PandaDoc document signing, and various other APIs." },
                    { 41, 4, 1, 7, "Oversaw and managed the complete backend development process." },
                    { 42, 4, 2, 7, "Integrated with multiple bowling and card-reading systems to enhance overall system functionality." },
                    { 43, 4, 3, 7, "Developed a fully-featured order and scheduling system." },
                    { 48, 7, 1, 8, "Developed tools to facilitate the indexing of library archives." },
                    { 50, 7, 3, 8, "Collaborated as a member of an 8-person development team utilizing Agile/Scrum methodologies." },
                    { 51, 7, 4, 8, "Maintained 100% code coverage." },
                    { 53, 8, 1, 9, "Oversaw the architecture, development, and deployment of a comprehensive affiliate marketing platform." },
                    { 54, 8, 1, 10, "Directly managed a team of up to 9 developers utilizing Agile Scrum methodologies." },
                    { 55, 8, 2, 10, "Oversaw the architecture, development, and deployment of a comprehensive e-learning platform." },
                    { 56, 8, 2, 9, "Ensured 99% uptime to maintain smooth business operations." }
                });

            migrationBuilder.InsertData(
                table: "JobSkill",
                columns: new[] { "JobId", "SkillId", "ResumeId" },
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
                    { 9, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 2, 1 },
                    { 1, 3, 1 },
                    { 2, 3, 1 },
                    { 3, 3, 1 },
                    { 4, 3, 1 },
                    { 5, 3, 1 },
                    { 6, 3, 1 },
                    { 8, 3, 1 },
                    { 1, 4, 1 },
                    { 2, 4, 1 },
                    { 3, 4, 1 },
                    { 4, 4, 1 },
                    { 1, 5, 1 },
                    { 2, 5, 1 },
                    { 3, 5, 1 },
                    { 1, 6, 1 },
                    { 2, 6, 1 },
                    { 3, 6, 1 },
                    { 1, 7, 1 },
                    { 2, 7, 1 },
                    { 3, 7, 1 },
                    { 2, 8, 1 },
                    { 3, 8, 1 },
                    { 4, 8, 1 },
                    { 5, 8, 1 },
                    { 1, 9, 1 },
                    { 2, 9, 1 },
                    { 3, 9, 1 },
                    { 2, 10, 1 },
                    { 3, 10, 1 },
                    { 4, 10, 1 },
                    { 2, 11, 1 },
                    { 3, 11, 1 },
                    { 2, 12, 1 },
                    { 3, 12, 1 },
                    { 2, 13, 1 },
                    { 3, 13, 1 },
                    { 2, 14, 1 },
                    { 2, 16, 1 },
                    { 2, 17, 1 },
                    { 1, 18, 1 },
                    { 1, 19, 1 },
                    { 1, 20, 1 },
                    { 1, 21, 1 },
                    { 1, 22, 1 },
                    { 2, 22, 1 },
                    { 3, 22, 1 },
                    { 1, 23, 1 },
                    { 1, 24, 1 },
                    { 4, 25, 1 },
                    { 5, 25, 1 },
                    { 5, 26, 1 },
                    { 5, 27, 1 },
                    { 5, 28, 1 },
                    { 9, 29, 1 },
                    { 9, 30, 1 },
                    { 1, 31, 1 },
                    { 4, 31, 1 },
                    { 9, 32, 1 },
                    { 8, 33, 1 },
                    { 9, 33, 1 },
                    { 1, 34, 1 },
                    { 2, 34, 1 },
                    { 3, 34, 1 },
                    { 6, 34, 1 },
                    { 7, 34, 1 },
                    { 8, 34, 1 },
                    { 9, 34, 1 },
                    { 1, 36, 1 },
                    { 2, 36, 1 },
                    { 3, 36, 1 },
                    { 4, 36, 1 },
                    { 5, 36, 1 },
                    { 6, 36, 1 },
                    { 7, 36, 1 },
                    { 8, 36, 1 },
                    { 9, 36, 1 },
                    { 1, 37, 1 },
                    { 1, 38, 1 },
                    { 2, 38, 1 },
                    { 3, 39, 1 },
                    { 3, 40, 1 },
                    { 3, 41, 1 },
                    { 3, 42, 1 },
                    { 4, 42, 1 },
                    { 2, 43, 1 },
                    { 6, 43, 1 },
                    { 7, 43, 1 },
                    { 2, 44, 1 },
                    { 3, 44, 1 },
                    { 6, 44, 1 },
                    { 7, 44, 1 },
                    { 9, 44, 1 },
                    { 8, 46, 1 },
                    { 9, 47, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Degree_SchoolId",
                table: "Degree",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Highlight_JobId",
                table: "Highlight",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Highlight_ProjectId_JobId",
                table: "Highlight",
                columns: new[] { "ProjectId", "JobId" });

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
                name: "IX_Project_JobId",
                table: "Project",
                column: "JobId");

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

            migrationBuilder.CreateIndex(
                name: "IX_School_PersonaId",
                table: "School",
                column: "PersonaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Degree");

            migrationBuilder.DropTable(
                name: "Highlight");

            migrationBuilder.DropTable(
                name: "JobSkill");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "Project");

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

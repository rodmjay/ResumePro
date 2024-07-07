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
            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 1, 1 },
                column: "Text",
                value: "Pioneered the development and launch of mission-critical workflows, enhancing software reliability significantly.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 2, 1 },
                column: "Text",
                value: "Architected the Geneva logging library, becoming a foundational tool for the Site Reliability Engineering (SRE) department.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 3, 1 },
                column: "Text",
                value: "Crafted real-time dashboards using Geneva, handling data from millions of records for instant insights.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 4, 1 },
                column: "Text",
                value: "Engineered and deployed complex Synapse workspaces with Bicep across multiple environments, optimizing deployment processes.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 6, 1 },
                column: "Text",
                value: "Emerged as the company-wide leader for .NET initiatives, steering pivotal technology decisions and strategies.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 7, 1 },
                column: "Text",
                value: "Revolutionized technical screenings and led numerous interviews, enhancing recruitment processes and team quality.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 8, 1 },
                column: "Text",
                value: "Created a shared .NET codebase, significantly reducing development redundancy and boosting project efficiency.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 9, 1 },
                column: "Text",
                value: "Awarded 'Top Developer of 2021' for exceptional productivity and innovation.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 10, 1 },
                column: "Text",
                value: "Spearheaded the architecture of a model MicroService, setting new standards for future developments.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 11, 1 },
                column: "Text",
                value: "Conducted hundreds of technical screenings for C# and Angular developers, refining the technical team’s capabilities.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 12, 1 },
                column: "Text",
                value: "Overhauled an antiquated system using the latest .NET technologies, greatly improving performance and scalability.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 13, 1 },
                column: "Text",
                value: "Seamlessly integrated an existing SQL Server database with EF Core, optimizing data operations and efficiency.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 14, 1 },
                column: "Text",
                value: "Developed a robust gRPC-based messaging system using C# WebAPI, enhancing inter-service communication.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 15, 1 },
                column: "Text",
                value: "Introduced advanced patterns for unit and integration testing, achieving 90% code coverage and setting a high standard for code reliability.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 1, 1 },
                column: "Text",
                value: "Oversaw the development and deployment of mission-critical workflows to enhance software reliability");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 2, 1 },
                column: "Text",
                value: "Developed the Geneva logging library utilized by the Site Reliability Engineering (SRE) department.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 3, 1 },
                column: "Text",
                value: "Developed dashboards utilizing Geneva to display real-time data from millions of records.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 4, 1 },
                column: "Text",
                value: "Utilized Bicep to deploy complex Synapse workspaces across multiple environments.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 6, 1 },
                column: "Text",
                value: "Company-wide leader for .NET initiatives.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 7, 1 },
                column: "Text",
                value: "Developed the technical screening process and conducted numerous technical interviews.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 8, 1 },
                column: "Text",
                value: "Developed the common codebase utilized for all .NET projects.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 9, 1 },
                column: "Text",
                value: "Received the award for highest-producing developer in 2021.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 10, 1 },
                column: "Text",
                value: "Responsible for the development and architecture of a new MicroService that served as a model for subsequent microservices.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 11, 1 },
                column: "Text",
                value: "Conducted technical screenings for hundreds of C# and Angular developers.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 12, 1 },
                column: "Text",
                value: "Responsible for the re-architecture and development of an antiquated system using the latest .NET technologies.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 13, 1 },
                column: "Text",
                value: "Integrated an existing SQL Server database with EF Core.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 14, 1 },
                column: "Text",
                value: "Developed a gRPC-based messaging system using C# WebAPI.");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 15, 1 },
                column: "Text",
                value: "Introduced enhanced patterns for unit and integration testing frameworks.");

            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "OrganizationId", "JobId", "Order", "ProjectId", "Text" },
                values: new object[] { 16, 1, 1, 6, 1, "Maintaned 90% code coverage." });
        }
    }
}

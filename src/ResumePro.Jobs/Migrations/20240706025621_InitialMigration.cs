using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Jobs.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategory", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "JobCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Jobs related to developing and maintaining software applications.", "Software Development" },
                    { 2, "Positions focused on promoting products and services.", "Marketing" },
                    { 3, "Roles involved in selling products and services to customers.", "Sales" },
                    { 4, "Jobs related to recruiting", "Human Resources" },
                    { 5, "Positions focused on managing company finances and accounts.", "Finance" },
                    { 6, "Roles involved in supporting and assisting customers.", "Customer Service" },
                    { 7, "Jobs related to designing and building products or infrastructure.", "Engineering" },
                    { 8, "Positions focused on overseeing product development and strategy.", "Product Management" },
                    { 9, "Roles involved in managing day-to-day business operations.", "Operations" },
                    { 10, "Jobs related to legal compliance and providing legal advice.", "Legal" },
                    { 11, "Positions focused on administrative and clerical tasks.", "Administrative" },
                    { 12, "Jobs related to providing technical support and managing IT systems.", "IT Support" },
                    { 13, "Positions focused on innovating and developing new products.", "Research and Development" },
                    { 14, "Roles involved in teaching and educational administration.", "Education" },
                    { 15, "Jobs related to providing medical and healthcare services.", "Healthcare" },
                    { 16, "Positions focused on managing supply chain and distribution.", "Logistics" },
                    { 17, "Roles related to graphic design", "Creative" },
                    { 18, "Jobs focused on ensuring product or service quality.", "Quality Assurance" },
                    { 19, "Positions involved in the production of goods.", "Manufacturing" },
                    { 20, "Roles related to planning and overseeing projects.", "Project Management" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobCategory");
        }
    }
}

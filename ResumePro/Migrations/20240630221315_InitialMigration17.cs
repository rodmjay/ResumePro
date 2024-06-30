using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reference",
                columns: new[] { "Id", "JobId", "Name", "PhoneNumber", "Text" },
                values: new object[] { 2, 2, "", "", "I worked with Rod when he was recruited as the lead architect for a complicated financial SaaS product for an important client.\\Rod quickly impressed me with his ability to put himself in the clients shoes and build creative solutions that were focused on bringing the most value for the smallest cost.\\He also understands how to leverage the chosen technology for great results. Many a time I was impressed with recommendations he made that were far beyond what anyone else had considered.\\Rod’s experience and skill as an architect and engineering leader allowed us to place him on critical client projects and trust that he would delight the client and lead the team successfully.\\It was a pleasure to work with such a talented mind. Rod will add experience and technical leadership to any company." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

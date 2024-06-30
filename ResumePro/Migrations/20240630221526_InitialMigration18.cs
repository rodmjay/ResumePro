using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "PhoneNumber" },
                values: new object[] { "Test1", "Test2" });

            migrationBuilder.InsertData(
                table: "Reference",
                columns: new[] { "Id", "JobId", "Name", "PhoneNumber", "Text" },
                values: new object[,]
                {
                    { 3, 2, "Test1", "Test2", "We asked people to step up and share their ideas and opinions on how to make SolutionStream more successful, Rod was one of the first people to come forward with an idea and plan and he executed it flawlessly. He is consistently bringing forward new ideas and ways to innovate some of the processes here, from interviewing to development" },
                    { 4, 2, "Test1", "Test2", "He's the best in .NET, he knows a lot of things and is a great employee. Also, he tries that everybody is update to the latest technologies" },
                    { 5, 2, "Test1", "Test2", "Recently, my startup worked with Rod on the development of our MVP (Minimal Viable Product) offering. I have worked with 100's of software developers over my career. Rod is one of the most talented and efficient architects/developers with whom I have been associated. I highly recommend him as a designer and implementer of complex or sophisticated software." },
                    { 6, 2, "Test1", "Test2", "If you want someone who can have high bandwidth conversations about the best way to design something, and then have that person accurately implement the agreed upon ideas as 5X the speed of a typical developer, Rod is your guy." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "PhoneNumber" },
                values: new object[] { "", "" });
        }
    }
}

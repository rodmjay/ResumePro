using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "PhoneNumber", "Text" },
                values: new object[] { "Joseph Cotton", null, "I had the opportunity to work with Rod as a fellow solutions architect at Kahoa. Rod was leading a team of developers to meet client software needs. During my time there, Rod was not only an outstanding project architect and team lead, but an outstanding individual contributor as well. He was still able to contribute just as much as the rest of his team did despite his additional leadership responsibilities.  Rod was also a central contributing figure to company architectural principles as a whole. He was was a senior member of Kahoa's architecture community, and helped guide discussions around software standards and best practices for the company as a whole. Rod is an excellent architect and software engineer. He has the ability to lead teams and projects, and has the grit to get them over the line when it's needed. I very highly recommend him to anyone seeking an outstanding software architect or team lead." });

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "PhoneNumber" },
                values: new object[] { "Cameo Doran", null });

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "PhoneNumber" },
                values: new object[] { "Rob Atlas", null });

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "PhoneNumber" },
                values: new object[] { "Rob Atlas", null });

            migrationBuilder.InsertData(
                table: "Reference",
                columns: new[] { "Id", "JobId", "Name", "PhoneNumber", "Text" },
                values: new object[,]
                {
                    { 7, 4, "Daniel Schulz", null, "Rod is a brilliant developer and a hard worker. He literally saved our project as I added him in the last hours as his expertise directed us to deliver. He certainly says up with the latest technologies, is a very fast learner, and was able to lead us in them. I highly recommend him." },
                    { 8, 7, "Ryan Done", null, "I worked with Rod on a complex web project at Ancestry.com. Rod made a big difference in the success of our project by finding great solutions and sharing different ways of looking at problems. He is sharp, knowledgeable and a great team player." },
                    { 9, 7, "Gregg B. Jensen", null, "Rod is a very skilled, and well rounded professional in web development. He is committed to success in all of his projects, and makes sure to deliver more than whats expected. His is a great asset to any team, and is an excellent team member." }
                });

            migrationBuilder.UpdateData(
                table: "Resume",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ShortName" },
                values: new object[] { "Rod is an enterprise architect with deep expertise in the latest .NET and web technologies. With 19 years of experience as a professional developer and architect, he has mastered the complete software development lifecycle, from ideation to implementation. Rod is frequently praised as a 10x developer, consistently delivering high-end software solutions from the ground up.", "Enterprise Application Architect" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "PhoneNumber", "Text" },
                values: new object[] { "Test", "123-123-1234", "test" });

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "PhoneNumber" },
                values: new object[] { "Test1", "Test2" });

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "PhoneNumber" },
                values: new object[] { "Test1", "Test2" });

            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "PhoneNumber" },
                values: new object[] { "Test1", "Test2" });

            migrationBuilder.InsertData(
                table: "Reference",
                columns: new[] { "Id", "JobId", "Name", "PhoneNumber", "Text" },
                values: new object[] { 4, 2, "Test1", "Test2", "He's the best in .NET, he knows a lot of things and is a great employee. Also, he tries that everybody is update to the latest technologies" });

            migrationBuilder.UpdateData(
                table: "Resume",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ShortName" },
                values: new object[] { "Rod is an enterprise architect with expertise in the latest .NET and web technologies.  Rod has 19 years of experience as a professional developer and architect, with expertise in the complete software development lifecycle from ideation to implementation.  Rod is consistently described as a 10x developer who delivers high-end software from the ground up.", "Enterprice Application Architect" });
        }
    }
}

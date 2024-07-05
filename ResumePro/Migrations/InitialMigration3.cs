using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reference",
                table: "Reference");

            migrationBuilder.DropIndex(
                name: "IX_Reference_OrganizationId_PersonaId",
                table: "Reference");

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reference",
                table: "Reference",
                columns: new[] { "OrganizationId", "PersonaId", "Id" });

            migrationBuilder.InsertData(
                table: "Reference",
                columns: new[] { "Id", "OrganizationId", "PersonaId", "Name", "PhoneNumber", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, "Joseph Cotton", null, "I had the opportunity to work with Rod as a fellow solutions architect at Kahoa. Rod was leading a team of developers to meet client software needs. During my time there, Rod was not only an outstanding project architect and team lead, but an outstanding individual contributor as well. He was still able to contribute just as much as the rest of his team did despite his additional leadership responsibilities.  Rod was also a central contributing figure to company architectural principles as a whole. He was was a senior member of Kahoa's architecture community, and helped guide discussions around software standards and best practices for the company as a whole. Rod is an excellent architect and software engineer. He has the ability to lead teams and projects, and has the grit to get them over the line when it's needed. I very highly recommend him to anyone seeking an outstanding software architect or team lead." },
                    { 2, 1, 1, "Cameo Doran", null, "I worked with Rod when he was recruited as the lead architect for a complicated financial SaaS product for an important client.\\Rod quickly impressed me with his ability to put himself in the clients shoes and build creative solutions that were focused on bringing the most value for the smallest cost.\\He also understands how to leverage the chosen technology for great results. Many a time I was impressed with recommendations he made that were far beyond what anyone else had considered.\\Rod’s experience and skill as an architect and engineering leader allowed us to place him on critical client projects and trust that he would delight the client and lead the team successfully.\\It was a pleasure to work with such a talented mind. Rod will add experience and technical leadership to any company." },
                    { 5, 1, 1, "Rob Atlas", null, "Recently, my startup worked with Rod on the development of our MVP (Minimal Viable Product) offering. I have worked with 100's of software developers over my career. Rod is one of the most talented and efficient architects/developers with whom I have been associated. I highly recommend him as a designer and implementer of complex or sophisticated software." },
                    { 6, 1, 1, "Robert Clymer", null, "If you want someone who can have high bandwidth conversations about the best way to design something, and then have that person accurately implement the agreed upon ideas as 5X the speed of a typical developer, Rod is your guy." },
                    { 7, 1, 1, "Daniel Schulz", null, "Rod is a brilliant developer and a hard worker. He literally saved our project as I added him in the last hours as his expertise directed us to deliver. He certainly says up with the latest technologies, is a very fast learner, and was able to lead us in them. I highly recommend him." },
                    { 8, 1, 1, "Ryan Done", null, "I worked with Rod on a complex web project at Ancestry.com. Rod made a big difference in the success of our project by finding great solutions and sharing different ways of looking at problems. He is sharp, knowledgeable and a great team player." },
                    { 9, 1, 1, "Gregg B. Jensen", null, "Rod is a very skilled, and well rounded professional in web development. He is committed to success in all of his projects, and makes sure to deliver more than whats expected. His is a great asset to any team, and is an excellent team member." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reference",
                table: "Reference");

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId", "PersonaId" },
                keyValues: new object[] { 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId", "PersonaId" },
                keyValues: new object[] { 2, 1, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId", "PersonaId" },
                keyValues: new object[] { 5, 1, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId", "PersonaId" },
                keyValues: new object[] { 6, 1, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId", "PersonaId" },
                keyValues: new object[] { 7, 1, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId", "PersonaId" },
                keyValues: new object[] { 8, 1, 1 });

            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumns: new[] { "Id", "OrganizationId", "PersonaId" },
                keyValues: new object[] { 9, 1, 1 });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reference",
                table: "Reference",
                columns: new[] { "OrganizationId", "Id" });

            migrationBuilder.InsertData(
                table: "Reference",
                columns: new[] { "Id", "OrganizationId", "Name", "PersonaId", "PhoneNumber", "Text" },
                values: new object[,]
                {
                    { 1, 1, "Joseph Cotton", 1, null, "I had the opportunity to work with Rod as a fellow solutions architect at Kahoa. Rod was leading a team of developers to meet client software needs. During my time there, Rod was not only an outstanding project architect and team lead, but an outstanding individual contributor as well. He was still able to contribute just as much as the rest of his team did despite his additional leadership responsibilities.  Rod was also a central contributing figure to company architectural principles as a whole. He was was a senior member of Kahoa's architecture community, and helped guide discussions around software standards and best practices for the company as a whole. Rod is an excellent architect and software engineer. He has the ability to lead teams and projects, and has the grit to get them over the line when it's needed. I very highly recommend him to anyone seeking an outstanding software architect or team lead." },
                    { 2, 1, "Cameo Doran", 1, null, "I worked with Rod when he was recruited as the lead architect for a complicated financial SaaS product for an important client.\\Rod quickly impressed me with his ability to put himself in the clients shoes and build creative solutions that were focused on bringing the most value for the smallest cost.\\He also understands how to leverage the chosen technology for great results. Many a time I was impressed with recommendations he made that were far beyond what anyone else had considered.\\Rod’s experience and skill as an architect and engineering leader allowed us to place him on critical client projects and trust that he would delight the client and lead the team successfully.\\It was a pleasure to work with such a talented mind. Rod will add experience and technical leadership to any company." },
                    { 5, 1, "Rob Atlas", 1, null, "Recently, my startup worked with Rod on the development of our MVP (Minimal Viable Product) offering. I have worked with 100's of software developers over my career. Rod is one of the most talented and efficient architects/developers with whom I have been associated. I highly recommend him as a designer and implementer of complex or sophisticated software." },
                    { 6, 1, "Robert Clymer", 1, null, "If you want someone who can have high bandwidth conversations about the best way to design something, and then have that person accurately implement the agreed upon ideas as 5X the speed of a typical developer, Rod is your guy." },
                    { 7, 1, "Daniel Schulz", 1, null, "Rod is a brilliant developer and a hard worker. He literally saved our project as I added him in the last hours as his expertise directed us to deliver. He certainly says up with the latest technologies, is a very fast learner, and was able to lead us in them. I highly recommend him." },
                    { 8, 1, "Ryan Done", 1, null, "I worked with Rod on a complex web project at Ancestry.com. Rod made a big difference in the success of our project by finding great solutions and sharing different ways of looking at problems. He is sharp, knowledgeable and a great team player." },
                    { 9, 1, "Gregg B. Jensen", 1, null, "Rod is a very skilled, and well rounded professional in web development. He is committed to success in all of his projects, and makes sure to deliver more than whats expected. His is a great asset to any team, and is an excellent team member." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reference_OrganizationId_PersonaId",
                table: "Reference",
                columns: new[] { "OrganizationId", "PersonaId" });
        }
    }
}

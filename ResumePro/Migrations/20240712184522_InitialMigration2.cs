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
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeSettings_Template_DefaultTemplateId",
                table: "ResumeSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Template",
                table: "Template");

            migrationBuilder.DeleteData(
                table: "Template",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Template",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Template");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Template",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DefaultTemplateId",
                table: "ResumeSettings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Template",
                table: "Template",
                column: "Name");

            migrationBuilder.UpdateData(
                table: "ResumeSettings",
                keyColumns: new[] { "OrganizationId", "ResumeId" },
                keyValues: new object[] { 1, 1 },
                column: "DefaultTemplateId",
                value: "markdown");

            migrationBuilder.InsertData(
                table: "Template",
                columns: new[] { "Name", "Format", "Source" },
                values: new object[,]
                {
                    { "html", ".hb", "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>{{FirstName}} {{LastName}} - Resume</title>\r\n    <style>\r\n        body { font-family: Arial, sans-serif; margin: 20px; }\r\n        h1 { font-size: 24px; }\r\n        h2 { font-size: 20px; margin-top: 20px; }\r\n        p { margin: 5px 0; }\r\n        ul { list-style-type: none; padding: 0; }\r\n        ul li { margin: 5px 0; }\r\n        .contact-info { margin-bottom: 20px; }\r\n        .section { margin-bottom: 20px; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <h1>{{FirstName}} {{LastName}}</h1>\r\n    <div class=\"contact-info\">\r\n        <p>Email: <a href=\"mailto:{{Email}}\">{{Email}}</a></p>\r\n        <p>Phone: {{PhoneNumber}}</p>\r\n        <p>LinkedIn: <a href=\"{{LinkedIn}}\">{{LinkedIn}}</a></p>\r\n        <p>GitHub: <a href=\"{{GitHub}}\">{{GitHub}}</a></p>\r\n        <p>Location: {{City}}, {{State}}, {{Country}}</p>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Job Title</h2>\r\n        <p>{{JobTitle}}</p>\r\n        <p>{{Description}}</p>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Skills</h2>\r\n        <ul>\r\n            {{#each Skills}}\r\n            <li>{{Title}} - {{Rating}}</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Experience</h2>\r\n        {{#each Jobs}}\r\n        <div class=\"job\">\r\n            <h3>{{Title}} at {{Company}}</h3>\r\n            <p>{{Location}} | {{StartDate}} - {{EndDate}}</p>\r\n            <p>{{Description}}</p>\r\n            <ul>\r\n                {{#each Highlights}}\r\n                <li>{{Text}}</li>\r\n                {{/each}}\r\n            </ul>\r\n            <ul>\r\n                {{#each Skills}}\r\n                <li>{{Name}}</li>\r\n                {{/each}}\r\n            </ul>\r\n            <div>\r\n                <h4>Projects:</h4>\r\n                {{#each Projects}}\r\n                <div class=\"project\">\r\n                    <h5>{{Name}}</h5>\r\n                    <p>{{Description}}</p>\r\n                    <ul>\r\n                        {{#each Highlights}}\r\n                        <li>{{Text}}</li>\r\n                        {{/each}}\r\n                    </ul>\r\n                </div>\r\n                {{/each}}\r\n            </div>\r\n        </div>\r\n        {{/each}}\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Education</h2>\r\n        {{#each Education}}\r\n        <div class=\"education\">\r\n            <h3>{{Name}}</h3>\r\n            {{#each Degrees}}\r\n            <p>{{Degree}} | {{StartDate}} - {{EndDate}}</p>\r\n            {{/each}}\r\n        </div>\r\n        {{/each}}\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Languages</h2>\r\n        <ul>\r\n            {{#each Languages}}\r\n            <li>{{LanguageName}} - {{Proficiency}}</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Certifications</h2>\r\n        <ul>\r\n            {{#each Certifications}}\r\n            <li>{{Name}} - {{OrganizationId}} ({{Date}})</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>References</h2>\r\n        <ul>\r\n            {{#each References}}\r\n            <li>{{Name}} - {{PhoneNumber}} | {{Text}}</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n</body>\r\n</html>\r\n" },
                    { "markdown", ".hb", "# {{firstName}} {{lastName}}, {{jobTitle}}\r\n\r\n- **Email:** {{email}}\r\n- **Phone:** {{phoneNumber}}\r\n- **LinkedIn:** {{linkedIn}}\r\n- **GitHub:** {{gitHub}}\r\n- **Languages:** {{languageString}}\r\n\r\n## Description\r\n{{description}}\r\n\r\n\r\n\r\n## Skills\r\n| Category               | Skills & Ratings                                       |\r\n|------------------------|--------------------------------------------------------|\r\n{{#each skillDictionary}}\r\n| **{{category}}**       | {{#each skills}}{{title}}{{#if settings.showRatings}}({{rating}}){{/if}}{{#unless @last}}, {{/unless}}{{/each}} |\r\n{{/each}}\r\n\r\n## Experience\r\n{{#each jobs}}\r\n### {{title}} - {{company}}\r\n*{{location}} - {{formatDate startDate}}-{{displayEndDate}}*\r\n{{#each highlights}}\r\n- {{text}}\r\n{{/each}}\r\n{{#each projects}}\r\n#### Project: {{name}}\r\n{{description}}\r\n{{#each highlights}}\r\n- {{text}}\r\n{{/each}}\r\n{{/each}}\r\n\r\n\r\n{{#if Skills}}\r\n**Technology Used:** {{#each Skills}}{{Name}}{{#unless @last}}, {{/unless}}{{/each}}\r\n{{/if}}\r\n{{/each}}\r\n\r\n## Education\r\n{{#each education}}\r\n### {{name}}\r\n*{{formatDate startDate}}-{{displayEndDate}}*\r\n{{#each degrees}}\r\n- Degree: {{name}}\r\n{{/each}}\r\n{{/each}}\r\n\r\n## References\r\n{{#each references}}\r\n### {{name}}\r\n{{text}}\r\n{{/each}}" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeSettings_Template_DefaultTemplateId",
                table: "ResumeSettings",
                column: "DefaultTemplateId",
                principalTable: "Template",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeSettings_Template_DefaultTemplateId",
                table: "ResumeSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Template",
                table: "Template");

            migrationBuilder.DeleteData(
                table: "Template",
                keyColumn: "Name",
                keyValue: "html");

            migrationBuilder.DeleteData(
                table: "Template",
                keyColumn: "Name",
                keyValue: "markdown");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Template",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Template",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultTemplateId",
                table: "ResumeSettings",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Template",
                table: "Template",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ResumeSettings",
                keyColumns: new[] { "OrganizationId", "ResumeId" },
                keyValues: new object[] { 1, 1 },
                column: "DefaultTemplateId",
                value: null);

            migrationBuilder.InsertData(
                table: "Template",
                columns: new[] { "Id", "Format", "Name", "Source" },
                values: new object[,]
                {
                    { 1, ".hb", "html", "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>{{FirstName}} {{LastName}} - Resume</title>\r\n    <style>\r\n        body { font-family: Arial, sans-serif; margin: 20px; }\r\n        h1 { font-size: 24px; }\r\n        h2 { font-size: 20px; margin-top: 20px; }\r\n        p { margin: 5px 0; }\r\n        ul { list-style-type: none; padding: 0; }\r\n        ul li { margin: 5px 0; }\r\n        .contact-info { margin-bottom: 20px; }\r\n        .section { margin-bottom: 20px; }\r\n    </style>\r\n</head>\r\n<body>\r\n    <h1>{{FirstName}} {{LastName}}</h1>\r\n    <div class=\"contact-info\">\r\n        <p>Email: <a href=\"mailto:{{Email}}\">{{Email}}</a></p>\r\n        <p>Phone: {{PhoneNumber}}</p>\r\n        <p>LinkedIn: <a href=\"{{LinkedIn}}\">{{LinkedIn}}</a></p>\r\n        <p>GitHub: <a href=\"{{GitHub}}\">{{GitHub}}</a></p>\r\n        <p>Location: {{City}}, {{State}}, {{Country}}</p>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Job Title</h2>\r\n        <p>{{JobTitle}}</p>\r\n        <p>{{Description}}</p>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Skills</h2>\r\n        <ul>\r\n            {{#each Skills}}\r\n            <li>{{Title}} - {{Rating}}</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Experience</h2>\r\n        {{#each Jobs}}\r\n        <div class=\"job\">\r\n            <h3>{{Title}} at {{Company}}</h3>\r\n            <p>{{Location}} | {{StartDate}} - {{EndDate}}</p>\r\n            <p>{{Description}}</p>\r\n            <ul>\r\n                {{#each Highlights}}\r\n                <li>{{Text}}</li>\r\n                {{/each}}\r\n            </ul>\r\n            <ul>\r\n                {{#each Skills}}\r\n                <li>{{Name}}</li>\r\n                {{/each}}\r\n            </ul>\r\n            <div>\r\n                <h4>Projects:</h4>\r\n                {{#each Projects}}\r\n                <div class=\"project\">\r\n                    <h5>{{Name}}</h5>\r\n                    <p>{{Description}}</p>\r\n                    <ul>\r\n                        {{#each Highlights}}\r\n                        <li>{{Text}}</li>\r\n                        {{/each}}\r\n                    </ul>\r\n                </div>\r\n                {{/each}}\r\n            </div>\r\n        </div>\r\n        {{/each}}\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Education</h2>\r\n        {{#each Education}}\r\n        <div class=\"education\">\r\n            <h3>{{Name}}</h3>\r\n            {{#each Degrees}}\r\n            <p>{{Degree}} | {{StartDate}} - {{EndDate}}</p>\r\n            {{/each}}\r\n        </div>\r\n        {{/each}}\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Languages</h2>\r\n        <ul>\r\n            {{#each Languages}}\r\n            <li>{{LanguageName}} - {{Proficiency}}</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>Certifications</h2>\r\n        <ul>\r\n            {{#each Certifications}}\r\n            <li>{{Name}} - {{OrganizationId}} ({{Date}})</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n\r\n    <div class=\"section\">\r\n        <h2>References</h2>\r\n        <ul>\r\n            {{#each References}}\r\n            <li>{{Name}} - {{PhoneNumber}} | {{Text}}</li>\r\n            {{/each}}\r\n        </ul>\r\n    </div>\r\n</body>\r\n</html>\r\n" },
                    { 2, ".hb", "markdown", "# {{firstName}} {{lastName}}, {{jobTitle}}\r\n\r\n- **Email:** {{email}}\r\n- **Phone:** {{phoneNumber}}\r\n- **LinkedIn:** {{linkedIn}}\r\n- **GitHub:** {{gitHub}}\r\n- **Languages:** {{languageString}}\r\n\r\n## Description\r\n{{description}}\r\n\r\n\r\n\r\n## Skills\r\n| Category               | Skills & Ratings                                       |\r\n|------------------------|--------------------------------------------------------|\r\n{{#each skillDictionary}}\r\n| **{{category}}**       | {{#each skills}}{{title}}{{#if settings.showRatings}}({{rating}}){{/if}}{{#unless @last}}, {{/unless}}{{/each}} |\r\n{{/each}}\r\n\r\n## Experience\r\n{{#each jobs}}\r\n### {{title}} - {{company}}\r\n*{{location}} - {{formatDate startDate}}-{{displayEndDate}}*\r\n{{#each highlights}}\r\n- {{text}}\r\n{{/each}}\r\n{{#each projects}}\r\n#### Project: {{name}}\r\n{{description}}\r\n{{#each highlights}}\r\n- {{text}}\r\n{{/each}}\r\n{{/each}}\r\n\r\n\r\n{{#if Skills}}\r\n**Technology Used:** {{#each Skills}}{{Name}}{{#unless @last}}, {{/unless}}{{/each}}\r\n{{/if}}\r\n{{/each}}\r\n\r\n## Education\r\n{{#each education}}\r\n### {{name}}\r\n*{{formatDate startDate}}-{{displayEndDate}}*\r\n{{#each degrees}}\r\n- Degree: {{name}}\r\n{{/each}}\r\n{{/each}}\r\n\r\n## References\r\n{{#each references}}\r\n### {{name}}\r\n{{text}}\r\n{{/each}}" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeSettings_Template_DefaultTemplateId",
                table: "ResumeSettings",
                column: "DefaultTemplateId",
                principalTable: "Template",
                principalColumn: "Id");
        }
    }
}

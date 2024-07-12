using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Name",
                keyValue: "markdown",
                column: "Source",
                value: "# {{firstName}} {{lastName}}, {{jobTitle}}\r\n\r\n- **Email:** {{email}}\r\n- **Phone:** {{phoneNumber}}\r\n- **LinkedIn:** {{linkedIn}}\r\n- **GitHub:** {{gitHub}}\r\n- **Languages:** {{languageString}}\r\n\r\n## Description\r\n{{description}}\r\n\r\n\r\n\r\n## Skills\r\n| Category               | Skills & Ratings                                       |\r\n|------------------------|--------------------------------------------------------|\r\n{{#each skillDictionary}}\r\n| **{{category}}**       | {{#each skills}}{{title}}{{#if settings.showRatings}}({{rating}}){{/if}}{{#unless @last}}, {{/unless}}{{/each}} |\r\n{{/each}}\r\n\r\n## Experience\r\n{{#each jobs}}\r\n### {{title}} - {{company}}\r\n*{{location}} - {{formatDate startDate}}-{{displayEndDate}} {{#if settings.showDuration}}({{duration}}){{/if}}*\r\n{{#each highlights}}\r\n- {{text}}\r\n{{/each}}\r\n{{#each projects}}\r\n#### Project: {{name}}\r\n{{description}}\r\n{{#each highlights}}\r\n- {{text}}\r\n{{/each}}\r\n{{/each}}\r\n\r\n\r\n{{#if Skills}}\r\n**Technology Used:** {{#each Skills}}{{Name}}{{#unless @last}}, {{/unless}}{{/each}}\r\n{{/if}}\r\n{{/each}}\r\n\r\n## Education\r\n{{#each education}}\r\n### {{name}}\r\n*{{formatDate startDate}}-{{displayEndDate}}*\r\n{{#each degrees}}\r\n- Degree: {{name}}\r\n{{/each}}\r\n{{/each}}\r\n\r\n## References\r\n{{#each references}}\r\n### {{name}}\r\n{{text}}\r\n{{/each}}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Name",
                keyValue: "markdown",
                column: "Source",
                value: "# {{firstName}} {{lastName}}, {{jobTitle}}\r\n\r\n- **Email:** {{email}}\r\n- **Phone:** {{phoneNumber}}\r\n- **LinkedIn:** {{linkedIn}}\r\n- **GitHub:** {{gitHub}}\r\n- **Languages:** {{languageString}}\r\n\r\n## Description\r\n{{description}}\r\n\r\n\r\n\r\n## Skills\r\n| Category               | Skills & Ratings                                       |\r\n|------------------------|--------------------------------------------------------|\r\n{{#each skillDictionary}}\r\n| **{{category}}**       | {{#each skills}}{{title}}{{#if settings.showRatings}}({{rating}}){{/if}}{{#unless @last}}, {{/unless}}{{/each}} |\r\n{{/each}}\r\n\r\n## Experience\r\n{{#each jobs}}\r\n### {{title}} - {{company}}\r\n*{{location}} - {{formatDate startDate}}-{{displayEndDate}} {{#if settings.duration}}({{duration}}){{/if}}*\r\n{{#each highlights}}\r\n- {{text}}\r\n{{/each}}\r\n{{#each projects}}\r\n#### Project: {{name}}\r\n{{description}}\r\n{{#each highlights}}\r\n- {{text}}\r\n{{/each}}\r\n{{/each}}\r\n\r\n\r\n{{#if Skills}}\r\n**Technology Used:** {{#each Skills}}{{Name}}{{#unless @last}}, {{/unless}}{{/each}}\r\n{{/if}}\r\n{{/each}}\r\n\r\n## Education\r\n{{#each education}}\r\n### {{name}}\r\n*{{formatDate startDate}}-{{displayEndDate}}*\r\n{{#each degrees}}\r\n- Degree: {{name}}\r\n{{/each}}\r\n{{/each}}\r\n\r\n## References\r\n{{#each references}}\r\n### {{name}}\r\n{{text}}\r\n{{/each}}");
        }
    }
}

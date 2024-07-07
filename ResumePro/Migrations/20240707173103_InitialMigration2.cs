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
            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Id",
                keyValue: 1,
                column: "Source",
                value: "# {{firstName}} {{lastName}}, {{jobTitle}}\n## Contact Information\n- **Email:** {{email}}\n- **Phone:** {{phoneNumber}}\n- **LinkedIn:** {{linkedIn}}\n- **GitHub:** {{gitHub}}\n- **Languages:** {{languageString}}\n\n## Description\n{{description}}\n\n## Skills\n{{#each skills}} \n- {{title}} (Rating: {{rating}})\n{{/each}}\n\n## Experience\n{{#each jobs}}\n### {{company}} - {{title}}\n*{{formatDate startDate}} - {{displayEndDate}}*\n{{#each projects}}\n#### Project: {{name}}\n{{description}}\n{{#each highlights}}\n- {{text}}\n{{/each}}\n{{/each}}\n{{#each highlights}}\n- {{text}}\n{{/each}}\n\n{{#if Skills}}\n  **Technology Used:** {{#each Skills}}{{Name}}{{#unless @last}}, {{/unless}}{{/each}}\n{{/if}}\n{{/each}}\n\n## Education\n{{#each education}}\n### {{name}}\n*{{formatDate startDate}} - {{displayEndDate}}*\n{{#each degrees}}\n- Degree: {{this}}\n{{/each}}\n{{/each}}\n\n## References\n{{#each references}}\n### {{name}}\n{{text}}\n{{/each}}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Id",
                keyValue: 1,
                column: "Source",
                value: "# {{firstName}} {{lastName}}, {{jobTitle}}\n## Contact Information\n- **Email:** {{email}}\n- **Phone:** {{phoneNumber}}\n- **LinkedIn:** {{linkedIn}}\n- **GitHub:** {{gitHub}}\n\n## Description\n{{description}}\n\n## Skills\n{{#each skills}} \n- {{title}} (Rating: {{rating}})\n{{/each}}\n\n## Experience\n{{#each jobs}}\n### {{company}} - {{title}}\n*{{formatDate startDate}} - {{displayEndDate}}*\n{{#each projects}}\n#### Project: {{name}}\n{{description}}\n{{#each highlights}}\n- {{text}}\n{{/each}}\n{{/each}}\n{{#each highlights}}\n- {{text}}\n{{/each}}\n\n{{#if Skills}}\n  **Technology Used:** {{#each Skills}}{{Name}}{{#unless @last}}, {{/unless}}{{/each}}\n{{/if}}\n{{/each}}\n\n## Education\n{{#each education}}\n### {{name}}\n*{{formatDate startDate}} - {{displayEndDate}}*\n{{#each degrees}}\n- Degree: {{this}}\n{{/each}}\n{{/each}}\n\n## References\n{{#each references}}\n### {{name}}\n{{text}}\n{{/each}}");
        }
    }
}

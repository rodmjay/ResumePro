# {{firstName}} {{lastName}}, {{jobTitle}}
## Contact Information
- **Email:** {{email}}
- **Phone:** {{phoneNumber}}
- **LinkedIn:** {{linkedIn}}
- **GitHub:** {{gitHub}}
- **Languages:** {{languageString}}

## Description
{{description}}

## Skills
{{#each skills}} 
- {{title}} (Rating: {{rating}})
{{/each}}

## Experience
{{#each jobs}}
### {{company}} - {{title}}
*{{formatDate startDate}} - {{displayEndDate}}*
{{#each projects}}
#### Project: {{name}}
{{description}}
{{#each highlights}}
- {{text}}
{{/each}}
{{/each}}
{{#each highlights}}
- {{text}}
{{/each}}

{{#if Skills}}
  **Technology Used:** {{#each Skills}}{{Name}}{{#unless @last}}, {{/unless}}{{/each}}
{{/if}}
{{/each}}

## Education
{{#each education}}
### {{name}}
*{{formatDate startDate}} - {{displayEndDate}}*
{{#each degrees}}
- Degree: {{this}}
{{/each}}
{{/each}}

## References
{{#each references}}
### {{name}}
{{text}}
{{/each}}
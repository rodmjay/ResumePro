# {{firstName}} {{lastName}}, {{jobTitle}}

{{#if settings.showContactInfo}}
- **Email:** {{email}}
- **Phone:** {{phoneNumber}}
- **LinkedIn:** {{linkedIn}}
- **GitHub:** {{gitHub}}
{{/if}}
- **Languages:** {{languageString}}

## Description
{{description}}

{{#eq settings.skillView 'Grouped'}}
## Skills
| Category               | Skills & Ratings                                       |
|------------------------|--------------------------------------------------------|
{{#each skillDictionary}}
| **{{category}}**       | {{#each skills}}{{title}}{{#if ../settings.showRatings}}({{rating}}){{/if}}{{#unless @last}}, {{/unless}}{{/each}} |
{{/each}}
{{/eq}}

{{#eq settings.skillView 'List'}}
## Skills
{{#each skills}} 
- {{title}} {{#if ../settings.showRatings}}(Rating: {{rating}}){{/if}}
{{/each}}
{{/eq}}

## Experience
{{#each jobs}}
### {{title}} - {{company}}
*{{location}} - {{formatDate startDate}}-{{displayEndDate}} {{#if ../settings.showDuration}}({{duration}}){{/if}}*
{{#each highlights}}
- {{text}}
{{/each}}
{{#each projects}}
#### Project: {{name}}
{{description}}
{{#each highlights}}
- {{text}}
{{/each}}
{{/each}}

{{#if Skills}}
**Technology Used:** {{#each Skills}}{{Name}}{{#unless @last}}, {{/unless}}{{/each}}
{{/if}}
{{/each}}

## Education
{{#each education}}
### {{name}}
*{{formatDate startDate}}-{{displayEndDate}}*
{{#each degrees}}
- Degree: {{name}}
{{/each}}
{{/each}}

## References
{{#each references}}
### {{name}}
{{text}}
{{/each}}
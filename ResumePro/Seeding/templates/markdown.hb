﻿# {{firstName}} {{lastName}}, {{jobTitle}}

- **Email:** {{email}}
- **Phone:** {{phoneNumber}}
- **LinkedIn:** {{linkedIn}}
- **GitHub:** {{gitHub}}
- **Languages:** {{languageString}}

## Description
{{description}}

## Skills
| Category               | Skills & Ratings                                       |
|------------------------|--------------------------------------------------------|
{{#each skillDictionary}}
| **{{category}}**       | {{#each skills}}{{title}} ({{rating}}) {{#unless @last}}, {{/unless}}{{/each}} |
{{/each}}

## Experience
{{#each jobs}}
### {{title}} - {{company}}
*{{location}} - {{formatDate startDate}}-{{displayEndDate}}*
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
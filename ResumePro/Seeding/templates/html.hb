<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>{{FirstName}} {{LastName}} - Resume</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; }
        h1 { font-size: 24px; }
        h2 { font-size: 20px; margin-top: 20px; }
        p { margin: 5px 0; }
        ul { list-style-type: none; padding: 0; }
        ul li { margin: 5px 0; }
        .contact-info { margin-bottom: 20px; }
        .section { margin-bottom: 20px; }
    </style>
</head>
<body>
    <h1>{{FirstName}} {{LastName}}</h1>
    <div class="contact-info">
        <p>Email: <a href="mailto:{{Email}}">{{Email}}</a></p>
        <p>Phone: {{PhoneNumber}}</p>
        <p>LinkedIn: <a href="{{LinkedIn}}">{{LinkedIn}}</a></p>
        <p>GitHub: <a href="{{GitHub}}">{{GitHub}}</a></p>
        <p>Location: {{City}}, {{State}}, {{Country}}</p>
    </div>

    <div class="section">
        <h2>Job Title</h2>
        <p>{{JobTitle}}</p>
        <p>{{Description}}</p>
    </div>

    <div class="section">
        <h2>Skills</h2>
        <ul>
            {{#each Skills}}
            <li>{{Title}} - {{Rating}}</li>
            {{/each}}
        </ul>
    </div>

    <div class="section">
        <h2>Experience</h2>
        {{#each Jobs}}
        <div class="job">
            <h3>{{Title}} at {{Company}}</h3>
            <p>{{Location}} | {{StartDate}} - {{EndDate}}</p>
            <p>{{Description}}</p>
            <ul>
                {{#each Highlights}}
                <li>{{Text}}</li>
                {{/each}}
            </ul>
            <ul>
                {{#each Skills}}
                <li>{{Name}}</li>
                {{/each}}
            </ul>
            <div>
                <h4>Projects:</h4>
                {{#each Projects}}
                <div class="project">
                    <h5>{{Name}}</h5>
                    <p>{{Description}}</p>
                    <ul>
                        {{#each Highlights}}
                        <li>{{Text}}</li>
                        {{/each}}
                    </ul>
                </div>
                {{/each}}
            </div>
        </div>
        {{/each}}
    </div>

    <div class="section">
        <h2>Education</h2>
        {{#each Education}}
        <div class="education">
            <h3>{{Name}}</h3>
            {{#each Degrees}}
            <p>{{Degree}} | {{StartDate}} - {{EndDate}}</p>
            {{/each}}
        </div>
        {{/each}}
    </div>

    <div class="section">
        <h2>Languages</h2>
        <ul>
            {{#each Languages}}
            <li>{{LanguageName}} - {{Proficiency}}</li>
            {{/each}}
        </ul>
    </div>

    <div class="section">
        <h2>Certifications</h2>
        <ul>
            {{#each Certifications}}
            <li>{{Name}} - {{OrganizationId}} ({{Date}})</li>
            {{/each}}
        </ul>
    </div>

    <div class="section">
        <h2>References</h2>
        <ul>
            {{#each References}}
            <li>{{Name}} - {{PhoneNumber}} | {{Text}}</li>
            {{/each}}
        </ul>
    </div>
</body>
</html>

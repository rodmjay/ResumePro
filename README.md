# ResumePro

ResumePro is a comprehensive resume management application that allows users to create, manage, and export professional resumes. The application consists of a .NET API backend and an Angular frontend.

## Prerequisites

- .NET SDK 8.0
- Node.js 18+ and npm
- Angular CLI (`npm install -g @angular/cli`)

## Project Structure

- `ResumePro.Api`: Backend API built with .NET 8.0
- `ResumeProApp`: Frontend application built with Angular 19
- `ResumePro.IntegrationTests`: Integration tests for the API
- Other supporting projects for domain, data, services, etc.

## Domain Model and Entity Relationships

The ResumePro application is built around the following core entities and their relationships:

### Core Entities

| Entity | Description |
|--------|-------------|
| Person/Persona | The central entity representing an individual with a resume. Contains personal information like name, contact details, and location. |
| Company | Represents an organization where a person has worked. Includes company name, location, and description. |
| Position | Represents a job role at a company, including job title and employment dates. |
| Project | Represents work done within a position, including name, description, and budget. |
| Highlight | Represents achievements or notable points about a project. |
| Resume | A compiled resume document for a person, including selected skills, experiences, and formatting settings. |
| Skill | Represents professional skills that can be associated with people, companies, or specific positions. |
| Language | Represents languages spoken by a person with proficiency levels. |
| Reference | Represents professional references for a person. |
| Certification | Represents professional certifications obtained by a person. |
| School | Represents educational institutions attended by a person. |
| Degree | Represents academic degrees obtained at a school. |

### Key Relationships

| Relationship | Cardinality | Description |
|--------------|-------------|-------------|
| Person → Company | Many-to-Many | A person can work at multiple companies, and a company can have multiple people. This relationship is established through positions. |
| Company → Position | One-to-Many | A company can have multiple positions, but a position belongs to only one company. |
| Position → Project | One-to-Many | A position can have multiple projects, but a project belongs to only one position. |
| Project → Highlight | One-to-Many | A project can have multiple highlights, but a highlight belongs to only one project. |
| Person → Skill | Many-to-Many | A person can have multiple skills, and a skill can be associated with multiple people. This relationship is implemented through PersonaSkill. |
| Person → Language | Many-to-Many | A person can speak multiple languages, and a language can be spoken by multiple people. This relationship is implemented through PersonaLanguage. |
| Person → Resume | One-to-Many | A person can have multiple resumes, but a resume belongs to only one person. |
| Person → Reference | One-to-Many | A person can have multiple references, but a reference belongs to only one person. |
| Person → School | One-to-Many | A person can attend multiple schools, but a school entry in the system belongs to one person. |
| School → Degree | One-to-Many | A school can offer multiple degrees, but a degree entry belongs to one school. |
| Person → Certification | One-to-Many | A person can have multiple certifications, but a certification entry belongs to one person. |

### Entity Hierarchy

The application follows a hierarchical structure for organizing work experience:
- Person
  - Company (where the person worked)
    - Position (job roles at the company)
      - Project (work done in the position)
        - Highlight (achievements in the project)

This hierarchical relationship allows for organized representation of a person's work history in their resume.

## Getting Started with the API

### Building the API

```bash
cd ~/repos/Bespoke/demo/ResumePro/ResumePro.Api
dotnet build
```

### Running the API

```bash
cd ~/repos/Bespoke/demo/ResumePro/ResumePro.Api
dotnet run
```

By default, the API will be available at `https://localhost:5001` and `http://localhost:5000`.

> **Note:** When running the API in development mode, it will automatically start the Angular app if port 4200 is available. This feature is only enabled in development environment and won't affect build pipelines.

## Getting Started with the Angular App

### Installing Dependencies

```bash
cd ~/repos/Bespoke/demo/ResumePro/ResumeProApp
npm install
```

### Building the Angular App

```bash
cd ~/repos/Bespoke/demo/ResumePro/ResumeProApp
npm run build
```

This will create a production build in the `dist/resumepro` directory.

### Running the Angular App for Development

```bash
cd ~/repos/Bespoke/demo/ResumePro/ResumeProApp
npm start
```

The application will be available at `http://localhost:4200`.

## Running Tests

### Running Cypress Tests (Frontend)

Cypress tests are used for end-to-end testing of the Angular application.

#### Interactive Mode

```bash
cd ~/repos/Bespoke/demo/ResumePro/ResumeProApp
npx cypress open
```

This will open the Cypress Test Runner, where you can select and run tests interactively.

#### Headless Mode

```bash
cd ~/repos/Bespoke/demo/ResumePro/ResumeProApp
npx cypress run
```

This will run all Cypress tests in headless mode and output the results to the console.

### Running Integration Tests (Backend)

Integration tests are used to test the API controllers and their interactions with the database.

#### Running All Tests

```bash
cd ~/repos/Bespoke/demo/ResumePro/ResumePro.IntegrationTests
dotnet test
```

#### Running Specific Tests

```bash
cd ~/repos/Bespoke/demo/ResumePro/ResumePro.IntegrationTests
dotnet test --filter "FullyQualifiedName~ResumePro.IntegrationTests.Tests.Controllers.ControllerNameTests"
```

#### Generating Code Coverage Report

```bash
cd ~/repos/Bespoke/demo/ResumePro/ResumePro.IntegrationTests
./run-coverage.sh
```

The coverage report will be available at `./CoverageReport/index.html`.

## Docker Support

The project includes a Dockerfile for containerization. To build and run the Docker container:

```bash
cd ~/repos/Bespoke
docker build -t resumepro -f demo/ResumePro/Dockerfile .
docker run -p 80:80 resumepro
```

## Making a Pull Request to GitHub

1. Create a new branch for your changes:
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. Make your changes and commit them:
   ```bash
   git add .
   git commit -m "Description of your changes"
   ```

3. Push your branch to GitHub:
   ```bash
   git push -u origin feature/your-feature-name
   ```

4. Create a pull request using the GitHub CLI:
   ```bash
   gh pr create --title "Your PR title" --body "Description of your changes"
   ```

5. Squash commits when merging:
   All pull requests should have commits squashed into a single commit before merging to maintain a clean git history.

## Troubleshooting

### API Issues
- Ensure the database connection string is correctly configured
- Check that the required dependencies are installed
- Verify that the correct .NET SDK version is installed

### Angular App Issues
- Clear the npm cache: `npm cache clean --force`
- Delete the node_modules directory and reinstall: `rm -rf node_modules && npm install`
- Ensure you're using a compatible Node.js version

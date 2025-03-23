using System.Diagnostics.CodeAnalysis;
using Bespoke.Data;
using Bespoke.Data.Attributes;
using Bespoke.Data.Bases;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ResumePro.Domain.Entities;
using ResumePro.Domain.Extensions;
using ResumePro.Entities;

[assembly: SeedAssembly]

namespace ResumePro.Data.Contexts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ApplicationContext : BaseContext<ApplicationContext>
{
    public ApplicationContext(
        DbContextOptions<ApplicationContext> options, IOptions<DbSettings> settings) : base(options, settings)
    {
    }

    protected override void SeedDatabase(ModelBuilder builder)
    {
        // these should be placed in the Seeding/csv folder for it to work
        // make sure files are marked as "EmbeddedResource => Copy if newer"
        // Base reference data first
        builder.Entity<Country>().Seed("countries.csv");
        builder.Entity<StateProvince>().Seed("state_provinces.csv");
        builder.Entity<Language>().Seed("languages.csv");
        builder.Entity<Skill>().Seed("skills.csv");
        builder.Entity<SkillCategory>().Seed("skill_categories.csv");
        builder.Entity<Template>().Seed("templates.csv");
        
        // Organization settings before any entities that depend on it
        builder.Entity<OrganizationSettings>().Seed("organization_settings.csv");
        
        // Seed Persona after its dependencies (StateProvince)
        builder.Entity<Persona>().Seed("personas.csv");
        
        // Seed relationship entities for Persona
        builder.Entity<PersonaSkill>().Seed("persona_skills.csv");
        builder.Entity<PersonaLanguage>().Seed("persona_language.csv");
        
        // Seed entities that depend on Persona
        builder.Entity<School>().Seed("schools.csv");
        builder.Entity<Degree>().Seed("degrees.csv");
        builder.Entity<Company>().Seed("companies.csv");
        
        // Seed entities that depend on Company
        builder.Entity<CompanySkill>().Seed("company_skills.csv");
        builder.Entity<Position>().Seed("positions.csv");
        
        // Seed entities that depend on Position
        builder.Entity<Project>().Seed("projects.csv");
        builder.Entity<Highlight>().Seed("highlights.csv");
        builder.Entity<ProjectHighlight>().Seed("project_highlights.csv");
        
        // Seed resume-related entities
        builder.Entity<Resume>().Seed("resumes.csv");
        builder.Entity<ResumeSettings>().Seed("resume_settings.csv");
        
        // Seed entities that depend on Resume and ResumeSettings
        builder.Entity<ResumeSkill>().Seed("resume_skills.csv");
        builder.Entity<ResumeCompany>().Seed("resume_companies.csv");
        
        // Seed remaining entities
        builder.Entity<Reference>().Seed("references.csv");
        builder.Entity<SkillCategorySkill>().Seed("category_skills.csv");
    }

    protected override void PreModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(Resume).Assembly);
        
        // Configure all DateTime properties to use UTC for PostgreSQL compatibility
        builder.ConfigureUtcDateTimes();
    }
}

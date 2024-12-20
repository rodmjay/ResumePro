﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Logging;
using ResumePro.Core.Data.Bases;
using ResumePro.Languages.Entities;
using ResumePro.Seeding.Extensions;

namespace ResumePro.Context;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ApplicationContext(DbContextOptions<ApplicationContext> options, ILoggerFactory loggerFactory)
    : BaseContext<ApplicationContext>(options)
{
    public ApplicationContext(
        DbContextOptions<ApplicationContext> options) : this(options, null)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (loggerFactory != null) optionsBuilder.UseLoggerFactory(loggerFactory);
    }

    protected override void ConfigureDatabase(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        //builder.Ignore<Highlight>();
        //builder.Ignore<Project>();
    }

    private void SeedEntities(ModelBuilder builder)
    {
        // these should be placed in the Seeding/csv folder for it to work
        // make sure files are marked as "EmbeddedResource => Copy if newer"
        builder.Entity<Country>().Seed("countries.csv");
        builder.Entity<StateProvince>().Seed("state_provinces.csv");
        builder.Entity<Persona>().Seed("personas.csv");
        builder.Entity<Skill>().Seed("skills.csv");
        builder.Entity<PersonaSkill>().Seed("persona_skills.csv");
        builder.Entity<Resume>().Seed("resumes.csv");
        builder.Entity<ResumeSettings>().Seed("resume_settings.csv");
        builder.Entity<OrganizationSettings>().Seed("organization_settings.csv");
        builder.Entity<ResumeSkill>().Seed("resume_skills.csv");
        builder.Entity<Company>().Seed("companies.csv");
        builder.Entity<Position>().Seed("positions.csv");
        builder.Entity<Highlight>().Seed("highlights.csv");
        builder.Entity<ProjectHighlight>().Seed("project_highlights.csv");
        builder.Entity<CompanySkill>().Seed("company_skills.csv");
        builder.Entity<Project>().Seed("projects.csv");
        builder.Entity<Reference>().Seed("references.csv");
        builder.Entity<School>().Seed("schools.csv");
        builder.Entity<Degree>().Seed("degrees.csv");
        builder.Entity<ResumeCompany>().Seed("resume_companies.csv");
        builder.Entity<Language>().Seed("languages.csv");
        builder.Entity<PersonaLanguage>().Seed("persona_language.csv");
        builder.Entity<SkillCategory>().Seed("skill_categories.csv");
        builder.Entity<SkillCategorySkill>().Seed("category_skills.csv");
    }

    private void SeedTemplates(ModelBuilder builder)
    {
        builder.Entity<Template>().SeedTemplates("./seeding/templates");
    }

    protected override void SeedDatabase(ModelBuilder builder)
    {
        SeedEntities(builder);
        SeedTemplates(builder);
    }
}
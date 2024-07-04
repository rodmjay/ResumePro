#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Core.Data.Bases;
using ResumePro.Entities;
using ResumePro.Seeding.Extensions;

namespace ResumePro.Context;

public class ApplicationContext : BaseContext<ApplicationContext>
{
    private readonly ILoggerFactory _loggerFactory;

    public ApplicationContext(
        DbContextOptions<ApplicationContext> options, ILoggerFactory loggerFactory) :
        base(options)
    {
        _loggerFactory = loggerFactory;
    }


    public ApplicationContext(
        DbContextOptions<ApplicationContext> options) : this(options, null)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_loggerFactory != null) optionsBuilder.UseLoggerFactory(_loggerFactory);
    }

    protected override void ConfigureDatabase(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }


    protected override void SeedDatabase(ModelBuilder builder)
    {
        // these should be placed in the Seeding/csv folder for it to work
        // make sure files are marked as "EmbeddedResource => Copy if newer"

        builder.Entity<Persona>().Seed("personas.csv");
        builder.Entity<Skill>().Seed("skills.csv");
        builder.Entity<PersonaSkill>().Seed("persona_skills.csv");
        builder.Entity<Resume>().Seed("resumes.csv");
        builder.Entity<ResumeSkill>().Seed("resume_skills.csv");
        builder.Entity<Job>().Seed("jobs.csv");
        builder.Entity<Highlight>().Seed("highlights.csv");
        builder.Entity<JobSkill>().Seed("job_skills.csv");
        builder.Entity<Project>().Seed("projects.csv");
        builder.Entity<Reference>().Seed("references.csv");
        builder.Entity<School>().Seed("schools.csv");
        builder.Entity<Degree>().Seed("degrees.csv");

        builder.Entity<ResumeJob>().Seed("resume_jobs.csv");

    }
}
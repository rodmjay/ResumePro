#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.DependencyInjection;
using ResumePro.Core.Middleware.Builders;
using ResumePro.Interfaces;
using ResumePro.Services;

namespace ResumePro.Extensions;

public static class AppBuilderExtensions
{
    public static AppBuilder AddApplicationDependencies(this AppBuilder builder)
    {
        builder.Services.AddScoped<IResumeService, ResumeService>();
        builder.Services.AddScoped<IPeopleService, PeopleService>();
        builder.Services.AddScoped<ISkillService, SkillService>();
        builder.Services.AddScoped<IJobService, JobService>();
        builder.Services.AddScoped<IHighlightService, HighlightService>();
        builder.Services.AddScoped<IPersonalSkillsService, PersonaSkillService>();
        builder.Services.AddScoped<IDegreeService, DegreeService>();
        builder.Services.AddScoped<IJobSkillService, JobSkillService>();
        builder.Services.AddScoped<IReferenceService, ReferenceService>();
        builder.Services.AddScoped<ISchoolService, SchoolService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<IResumeSkillService, ResumeSkillService>();

        return builder;
    }
}
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ResumePro.App.MessageHandlers;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Proxies;

namespace ResumePro.App.Extensions;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddProxies(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

        builder.Services
            .AddTransient<ApiAuthorizationMessageHandler>();

        builder.Services
            .AddTransient<AiApiAuthorizationMessageHandler>();

        Uri resumeProApiUrl = new Uri(builder.Configuration["ApiBase"]);
        Uri resumeProUsersApiUrl = new Uri(builder.Configuration["UserApiBase"]);
        Uri aiApiUrl = new Uri(builder.Configuration["AIApiBase"]);

        builder.Services.AddHttpClient<ITextController, ChatGptProxy>(
                client => client.BaseAddress = aiApiUrl)
            .AddHttpMessageHandler<AiApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IPeopleController, PeopleProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<ITemplatesController, TemplatesProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IResumeController, ResumeProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IJobsController, JobsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<ICertificationsController, CertificationsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IDegreesController, DegreesProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IFiltersController, FiltersProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IHighlightsController, HighlightsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IOrganizationSettingsController, OrganizationSettingsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IPersonSkillsController, PersonSkillsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IProjectHighlightsController, ProjectHighlightsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IProjectsController, ProjectsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IReferencesController, ReferencesProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IResumeSkillsController, ResumeSkillsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<ISchoolsController, SchoolsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<ISkillsController, SkillsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IUserController, UserProxy>(
                client => client.BaseAddress = resumeProUsersApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        return builder;
    }
}
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ResumePro.Blazor.MessageHandlers;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Proxies;

namespace ResumeBuilder.App.Extensions;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddProxies(this WebAssemblyHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        
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

        builder.Services.AddHttpClient<IIndividualResumeSettingsController, IndividualResumeSettingsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IPersonController, PersonProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<ITemplatesController, TemplatesProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualResumeController, IndividualResumeProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualJobsController, IndividualJobsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualCertificationsController, IndividualCertificationsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualDegreesController, IndividualDegreesProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualHighlightsController, IndividualHighlightsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IOrganizationSettingsController, OrganizationSettingsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualSkillsController, IndividualSkillsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualProjectHighlightsController, IndividualProjectHighlightsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualProjectsController, IndividualProjectsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualReferencesController, IndividualReferencesProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualResumeSkillsController, IndividualResumeSkillsProxy>(
                client => client.BaseAddress = resumeProApiUrl)
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        builder.Services.AddHttpClient<IIndividualSchoolsController, IndividualSchoolsProxy>(
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
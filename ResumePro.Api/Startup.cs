﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Reflection;
using Microsoft.Extensions.Options;
using ResumePro.Context;
using ResumePro.Core.Extensions;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Core.Settings;
using ResumePro.Extensions;

namespace ResumePro.Api;

public sealed class Startup(
    IConfiguration configuration,
    IWebHostEnvironment environment,
    HttpMessageHandler identityServerMessageHandler = null)
{
    public IWebHostEnvironment Environment { get; } = environment;
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        var thisAssembly = Assembly.GetAssembly(GetType());
        var businessAssembly = typeof(ApplicationContext).Assembly;

        var builder = services.ConfigureApp(Configuration).AddDatabase<ApplicationContext>()
            .AddAutomapperProfilesFromAssemblies()
            .RegisterHandlebarsExtensions()
            .AddAppInsights()
            .RegisterPdfGeneration()
            .RegisterAllServices(businessAssembly);

        var webAppBuilder = builder.ConfigureWebApp(Environment);

        var restBuilder = webAppBuilder.ConfigureRest()
            .AddCors()
            .AddAuthorization()
            .AddBearerAuthentication(identityServerMessageHandler)
            .AddSwagger(thisAssembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context,
        IOptions<AppSettings> settings)
    {
        RestApiBuilderExtensions.Configure(app, env, settings);
    }
}
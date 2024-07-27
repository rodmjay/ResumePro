#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Reflection;
using Microsoft.Extensions.Options;
using ResumePro.AI.Contexts;
using ResumePro.AI.Services;
using ResumePro.Core.Extensions;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Core.Settings;

namespace ResumePro.OpenAI.Api;

public class Startup(
    IConfiguration configuration,
    IWebHostEnvironment environment,
    HttpMessageHandler identityServerMessageHandler = null)
{
    public IWebHostEnvironment Environment { get; } = environment;
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        var thisAssembly = Assembly.GetAssembly(GetType());
        var businessAssembly = typeof(ChatGptService).Assembly;

        var builder = services.ConfigureApp(Configuration)
            .AddDatabase<ApplicationContext>()
            .AddAutomapperProfilesFromAssemblies()
            .RegisterHandlebarsExtensions()
            .RegisterAllServices(businessAssembly);

        var webAppBuilder = builder.ConfigureWebApp(Environment);

        var restBuilder = webAppBuilder.ConfigureRest()
            .AddCors()
            .AddAuthorization()
            .AddBearerAuthentication(identityServerMessageHandler)
            .AddSwagger(thisAssembly!);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
        IOptions<AppSettings> settings)
    {
        RestApiBuilderExtensions.Configure(app, env, settings);
    }
}
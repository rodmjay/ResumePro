#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ResumePro.Context;
using ResumePro.Core.Extensions;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Core.Settings;

namespace ResumePro.Api;

public class Startup
{
    private readonly HttpMessageHandler _identityServerMessageHandler;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment,
        HttpMessageHandler identityServerMessageHandler = null)
    {
        _identityServerMessageHandler = identityServerMessageHandler;
        Configuration = configuration;
        Environment = environment;
    }

    public IWebHostEnvironment Environment { get; }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var thisAssembly = Assembly.GetAssembly(GetType());
        var businessAssembly = typeof(ApplicationContext).Assembly;

        var builder = services.ConfigureApp(Configuration).AddDatabase<ApplicationContext>()
            .AddAutomapperProfilesFromAssemblies()
            .RegisterHandlebarsExtensions()
            .RegisterAllServices(businessAssembly);

        var webAppBuilder = builder.ConfigureWebApp(Environment);

        var restBuilder = webAppBuilder.ConfigureRest()
            .AddCors()
            .AddAuthorization(policy =>
            {
                policy.RequireAuthenticatedUser();

                var scopes = builder.AppSettings.Audience.Split(" ");
                foreach (var scope in scopes) policy.RequireClaim("scope", scope);
            })
            .AddBearerAuthentication(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = builder.AppSettings.Authority;
                options.Audience = builder.AppSettings.Audience;

                if (_identityServerMessageHandler != null)
                    options.BackchannelHttpHandler = _identityServerMessageHandler;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidAudience = builder.AppSettings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = builder.AppSettings.Authority,
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = c =>
                    {
                        var x = 1;
                        return Task.FromResult(0);
                    },
                    OnAuthenticationFailed = c =>
                    {
                        var logger = c.HttpContext.RequestServices.GetRequiredService<ILogger<StartupBase>>();
                        logger.LogTrace("Authentication Failure");
                        return Task.FromResult(0);
                    },
                    OnTokenValidated = c =>
                    {
                        var logger = c.HttpContext.RequestServices.GetRequiredService<ILogger<StartupBase>>();
                        logger.LogTrace("Authentication Success");
                        return Task.FromResult(0);
                    }
                };
            })
            .AddSwagger(thisAssembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context,
        IOptions<AppSettings> settings)
    {
        RestApiBuilderExtensions.Configure(app, env, settings);
    }
}
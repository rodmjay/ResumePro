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
using ResumePro.Extensions;
using ResumePro.Geography.Extensions;
using ResumePro.Languages.Extensions;

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
        var builder = services.ConfigureApp(Configuration).AddDatabase<ApplicationContext>()
            .AddAutomapperProfilesFromAssemblies()
            .RegisterAllServices(typeof(ApplicationContext).Assembly);

        var webAppBuilder = builder.ConfigureWebApp(Environment);

        var restBuilder = webAppBuilder.ConfigureRest()
            .AddAuthorization(policy=>
            {
                policy.RequireAuthenticatedUser();

                var scopes = builder.AppSettings.Audience.Split(" ");
                foreach (var scope in scopes)
                {
                    policy.RequireClaim("scope", scope);
                }


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

                    NameClaimType = "name",
                    RoleClaimType = "role"
                };

                options.Events = new JwtBearerEvents
                {
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
            .AddSwagger(Assembly.GetAssembly(GetType()));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context,
        IOptions<AppSettings> settings)
    {
        RestApiBuilderExtensions.Configure(app, env, settings);
    }
}
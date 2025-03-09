#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Reflection;
using Bespoke.Azure.Extensions;
using Bespoke.Core.Extensions;
using Bespoke.Core.Settings;
using Bespoke.Data.Extensions;
using Bespoke.Data.SqlServer;
using Bespoke.Rest.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ResumePro.Data.Contexts;

namespace ResumePro.Api;

public sealed class Startup
{
    private readonly HttpMessageHandler _identityServerMessageHandler;

    public Startup(IConfiguration configuration,
        IWebHostEnvironment environment,
        HttpMessageHandler identityServerMessageHandler = null)
    {
        _identityServerMessageHandler = identityServerMessageHandler;
        Environment = environment;
        Configuration = configuration;
    }

    public IWebHostEnvironment Environment { get; }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddCompositeRoot(Configuration, builder =>
        {
            builder.AddAutomapper()
                .AddStorage(storageBuilder =>
                {
                    storageBuilder.UseSqlServer<ApplicationContext>();
                })
                .AddRest(restBuilder =>
                {
                    restBuilder.AddSwagger(options =>
                    {
                        options.AddSecurityDefinition("X-User-Id", new OpenApiSecurityScheme
                        {
                            Name = "X-User-Id", // Name of the header
                            In = ParameterLocation.Header, // Location of the header
                            Type = SecuritySchemeType.ApiKey, // Specify it's a header
                            Description = "Custom header to pass the User ID"
                        });

                        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "X-User-Id"
                                    }
                                },
                                Array.Empty<string>()
                            }
                        });
                    });
                });
        });
        
        //var thisAssembly = Assembly.GetAssembly(GetType());
        //var businessAssembly = typeof(ApplicationContext).Assembly;

        //var builder = services.ConfigureApp(Configuration).AddDatabase<ApplicationContext>()
        //    .AddAutomapperProfilesFromAssemblies()
        //    .RegisterHandlebarsExtensions()
        //    .AddAppInsights()
        //    .RegisterPdfGeneration()
        //    .RegisterAllServices(businessAssembly);

        //var webAppBuilder = builder.ConfigureWebApp(Environment);

        //var restBuilder = webAppBuilder.ConfigureRest()
        //    .AddCors()
        //    .AddAuthorization()
        //    .AddBearerAuthentication(_identityServerMessageHandler)
        //    .AddSwagger(thisAssembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context,
        IOptions<AppSettings> settings)
    {
        RestApiBuilderExtensions.Configure(app, env, settings);
    }
}
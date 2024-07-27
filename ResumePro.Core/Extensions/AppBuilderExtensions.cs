#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Globalization;
using System.Reflection;
using System.Security.Claims;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ResumePro.Core.Middleware.Builders;

namespace ResumePro.Core.Extensions;

public static class AppBuilderExtensions
{
    public static AppBuilder RegisterHandlebarsExtensions(this AppBuilder builder)
    {
        Handlebars.RegisterHelper("formatDate", (writer, context, parameters) =>
        {
            if (parameters[0] is DateTime dateTime)
                writer.WriteSafeString(dateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            else if (DateTime.TryParse(parameters[0]?.ToString(), out var parsedDate))
                writer.WriteSafeString(parsedDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            else
                writer.WriteSafeString(parameters[0]?.ToString());
        });

        Handlebars.RegisterHelper("eq", (output, options, context, arguments) =>
        {
            if (arguments.Length != 2) throw new HandlebarsException("eq helper must have exactly two arguments");

            var left = arguments[0];
            var right = arguments[1];

            var isEqual = left?.ToString() == right?.ToString();
            if (isEqual)
                options.Template(output, context); // Render the main block if true
            else
                options.Inverse(output, context); // Render the inverse block if false
        });

        return builder;
    }

    public static RestApiBuilder AddAuthorization(this RestApiBuilder builder)
    {
        builder.AddAuthorization(policy =>
        {
            policy.RequireAuthenticatedUser();

            var scopes = builder.AppSettings.Audience.Split(" ");
            foreach (var scope in scopes) policy.RequireClaim("scope", scope);
        });
        return builder;
    }
    
    public static RestApiBuilder AddAuthorization(this RestApiBuilder builder,
        Action<AuthorizationPolicyBuilder> action)
    {
        builder.Services.AddAuthorization(options => { options.AddPolicy("ApiScope", action); });

        return builder;
    }

    public static RestApiBuilder AddBearerAuthentication(this RestApiBuilder builder,
        Action<JwtBearerOptions> action)
    {
        //builder.Services.AddSingleton<IClaimsTransformation, ClaimsTransformer>();
        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", action);

        return builder;
    }

    public static RestApiBuilder AddBearerAuthentication(this RestApiBuilder builder, HttpMessageHandler? handler)
    {
        builder.AddBearerAuthentication(options =>
        {
            options.RequireHttpsMetadata = false;
            options.Authority = builder.AppSettings.Authority;
            options.Audience = builder.AppSettings.Audience;

            if (handler != null)
                options.BackchannelHttpHandler = handler;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidAudience = builder.AppSettings.Audience,
                ValidateIssuer = true,
                ValidIssuer = builder.AppSettings.Authority,
                NameClaimType = ClaimTypes.NameIdentifier,
                RoleClaimType = ClaimTypes.Role
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
        });
        return builder;
    }



    private static AppBuilder RegisterAllErrorDescribers(this AppBuilder builder, Assembly assembly)
    {
        var typesWithErrorDescriberName = assembly.GetTypes()
            .Where(x => x.Name.Contains("ErrorDescriber")).ToList();

        foreach (var type in typesWithErrorDescriberName) builder.Services.AddScoped(type);

        return builder;
    }


    private static AppBuilder RegisterServiceImplementations(this AppBuilder builder, Assembly assembly)
    {
        var typesWithInterfaces = assembly.GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Any())
            .Select(x => new
            {
                Implementation = x,
                Services = x.GetInterfaces().Where(i => i.Name == $"I{x.Name}")
            })
            .Where(x => x.Services.Any());


        foreach (var type in typesWithInterfaces)
        foreach (var service in type.Services)
            builder.Services.AddScoped(service, type.Implementation);
        return builder;
    }

    public static AppBuilder RegisterAllServices(this AppBuilder builder, Assembly assembly)
    {
        return builder
            .RegisterServiceImplementations(assembly)
            .RegisterAllErrorDescribers(assembly);
    }
}
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Globalization;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
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
            if (arguments.Length != 2)
            {
                throw new HandlebarsException("eq helper must have exactly two arguments");
            }

            var left = arguments[0];
            var right = arguments[1];

            var isEqual = left?.ToString() == right?.ToString();
            if (isEqual)
            {
                options.Template(output, context);  // Render the main block if true
            }
            else
            {
                options.Inverse(output, context);  // Render the inverse block if false
            }
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
}
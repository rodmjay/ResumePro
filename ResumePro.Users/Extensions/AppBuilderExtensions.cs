#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Core.Middleware.Builders;

namespace ResumePro.Users.Extensions;

public static class AppBuilderExtensions
{
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
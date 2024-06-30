#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Duende.IdentityServer.Services;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Core.Middleware.Builders;
using ResumePro.Services;

namespace ResumePro.Extensions;

public static class AppBuilderExtensions
{
    public static AppBuilder AddApplicationDependencies(this AppBuilder builder)
    {
        builder.Services.AddScoped<IResumeService, ResumeService>();

        return builder;
    }
    
}
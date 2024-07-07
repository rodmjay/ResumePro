#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.DependencyInjection.Extensions;
using ResumePro.Core.Middleware.Builders;
using ResumePro.Geography.Interfaces;
using ResumePro.Geography.Models;
using ResumePro.Geography.Services;

namespace ResumePro.Geography.Extensions;

public static class AppBuilderExtensions
{
    public static AppBuilder AddGeographyDependencies(this AppBuilder builder)
    {
        builder.Services.TryAddTransient<GeographyErrorDescriber>();
        builder.Services.TryAddScoped<ICountryService, CountryService>();

        return builder;
    }
}
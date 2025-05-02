#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.DependencyInjection.Extensions;
using ResumePro.Languages.Interfaces;
using ResumePro.Languages.Services;
using TechValley.Core.Builders;

namespace ResumePro.Languages.Extensions;

public static class AppBuilderExtensions
{
    public static AppBuilder AddLanguageDependencies(this AppBuilder builder)
    {
        builder.Services.TryAddScoped<ILanguageService, LanguageService>();

        return builder;
    }
}
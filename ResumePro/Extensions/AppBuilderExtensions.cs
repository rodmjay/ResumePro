#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ResumePro.Core.Middleware.Builders;
using ResumePro.Services;

namespace ResumePro.Extensions;

public static class AppBuilderExtensions
{
    public static AppBuilder AddGeographyDependencies(this AppBuilder builder)
    {
        builder.Services.TryAddTransient<GeographyErrorDescriber>();

        return builder;
    }

    public static AppBuilder RegisterAllServices(this AppBuilder builder, Assembly assembly)
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
}
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Core.Middleware.Builders;

namespace ResumePro.Extensions;

public static class AppBuilderExtensions
{
    private static AppBuilder RegisterAllErrorDescribers(this AppBuilder builder, Assembly assembly)
    {
        var typesWithErrorDescriberName = assembly.GetTypes()
            .Where(x => x.Name.Contains("ErrorDescriber")).ToList();

        foreach (var type in typesWithErrorDescriberName)
        {
            builder.Services.AddScoped(type);
        }

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
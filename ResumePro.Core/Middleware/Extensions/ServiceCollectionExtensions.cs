#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Core.Middleware.Builders;
using ResumePro.Core.Settings;
using Serilog;

namespace ResumePro.Core.Middleware.Extensions;

public static class ServiceCollectionExtensions
{
    private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
    {
        return $"[{nameof(ServiceCollectionExtensions)}.{callerName}] - {message}";
    }


    public static AppBuilder ConfigureApp(
        this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = new AppSettings();

        var settingsSection = configuration.GetSection("AppSettings");
        settingsSection.Bind(appSettings);

        Log.Logger.Debug(GetLogMessage($"Application: {appSettings.Name}"));

        services.Configure<AppSettings>(settingsSection);
        services.AddOptions();

        return new AppBuilder(services, appSettings, configuration);
    }
}
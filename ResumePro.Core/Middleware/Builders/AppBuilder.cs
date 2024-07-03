#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Core.Settings;

namespace ResumePro.Core.Middleware.Builders;

public class AppBuilder
{
    public AppBuilder(
        IServiceCollection services,
        AppSettings settings,
        IConfiguration configuration)
    {
        Services = services;
        Configuration = configuration;
        AppSettings = settings;
        AssembliesToMap = new List<string>();
        AzureServiceBusConnectionString = configuration.GetConnectionString("AzureServiceBusConnection");
    }

    public string AzureServiceBusConnectionString { get; set; }

    public List<string> AssembliesToMap { get; set; }
    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; }
    public string ConnectionString { get; set; }
    public AppSettings AppSettings { get; set; }

    public IServiceProvider Build()
    {
        return Services.BuildServiceProvider();
    }
}
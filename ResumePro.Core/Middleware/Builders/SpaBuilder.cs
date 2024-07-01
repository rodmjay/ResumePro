#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Core.Settings;

namespace ResumePro.Core.Middleware.Builders;

public class SpaBuilder
{
    public SpaBuilder(IServiceCollection services,
        AppSettings settings,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        Services = services;
        AppSettings = settings;
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }
    public IServiceCollection Services { get; }

    public AppSettings AppSettings { get; set; }
}
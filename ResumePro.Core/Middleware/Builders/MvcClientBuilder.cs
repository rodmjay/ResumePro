#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Core.Settings;

namespace ResumePro.Core.Middleware.Builders;

public class MvcClientBuilder
{
    public MvcClientBuilder(WebAppBuilder builder)
    {
        Services = builder.Services;
        AppSettings = builder.AppSettings;
        Configuration = builder.Configuration;
        Environment = builder.Environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }
    public AppSettings AppSettings { get; set; }

    public IServiceCollection Services { get; }
}
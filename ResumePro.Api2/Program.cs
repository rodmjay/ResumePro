#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics.CodeAnalysis;
using ResumePro.Api;
using ResumePro.Core.Middleware.Extensions;
using Serilog;

namespace ResumePro.Api2;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        BuildHost(args)
            .Init();
    }

    public static IHostBuilder BuildHost(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(HostBuilderExtensions.Configure)
            .UseSerilog()
            .ConfigureWebHostDefaults(builder =>
            {
                builder
                    .ConfigureLogging(HostBuilderExtensions.ConfigureLogging)
                    .UseStartup<Startup>();
            });
    }
}
#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ResumePro.Core.Extensions;

namespace ResumePro.Testing.Extensions;

public static class CustomWebHostBuilderExtensions
{
    public static void Configure<TFixture>(WebHostBuilderContext hostingContext,
        IConfigurationBuilder config)
    {
        var env = hostingContext.HostingEnvironment;
        var integrationTestAssembly = typeof(TFixture).Assembly;

        config
            .AddEmbeddedJsonFile(integrationTestAssembly, "appsettings.json", true);



        config
            .AddEnvironmentVariables()
            .Build();
    }
}
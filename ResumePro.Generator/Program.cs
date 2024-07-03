#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Context;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Extensions;
using ResumePro.Generator.Strategies;
using ResumePro.Services;
using ResumePro.Shared;

namespace ResumePro.Generator;

internal class Program
{
    private static readonly IServiceProvider ServiceProvider;

    static Program()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var services = new ServiceCollection();

        ServiceProvider = services!.ConfigureApp(configuration)
            .AddDatabase<ApplicationContext>()
            .AddAutomapperProfilesFromAssemblies()
            .AddApplicationDependencies()
            .Build();
    }

    private static void Main(string[] args)
    {
        if (int.TryParse(args[0], out var resumeId))
        {
            var resumeService = ServiceProvider.GetRequiredService<IResumeService>();

            var resume = resumeService.GetResume<ResumeDetails>(resumeId).Result;

            if (resume != null)
            {
                List<IResumeStrategy> strategies = new()
                {
                    new MarkupResumeStrategy(),
                    new PdfResumeStrategy()
                };

                foreach (var strategy in strategies) strategy.ExecuteOperation(resume);
            }
        }
        else
        {
            Console.WriteLine("Please pass in a integer value for resumeID");
        }

        Console.ReadLine();
    }
}
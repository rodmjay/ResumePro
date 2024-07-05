#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Context;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Extensions;
using ResumePro.Generator.Strategies;
using ResumePro.Interfaces;
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
        int resumeId = args.Length > 0 && int.TryParse(args[0], out int parsedResumeId) ? parsedResumeId : 1;

        int organizationId = args.Length > 1 && int.TryParse(args[1], out int parsedOrgValue) ? parsedOrgValue : 1;

        var resumeService = ServiceProvider.GetRequiredService<IResumeService>();

        var resume = resumeService.GetResume<ResumeDetails>(organizationId, 1, resumeId).Result;

        if (resume != null)
        {
            List<IResumeStrategy> strategies = new()
            {
                new MarkupResumeStrategy(new MarkupSettings()
                {
                    OutputToConsole = true,
                    UpdateReadme = true
                }),
                new PdfResumeStrategy(new PdfSettings()
                {
                    CreateUpdatePdf = true,
                    DisplayInExplorer = true,
                    FontFamily = "Verdana"
                })
            };

            foreach (var strategy in strategies) strategy.ExecuteOperation(resume);
        }

        

        Console.ReadLine();
    }
}
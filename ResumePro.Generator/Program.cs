#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Context;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Extensions;
using ResumePro.Generation;
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
            .RegisterAllServices(typeof(ApplicationContext).Assembly)
            .Build();
    }

    private static void Main(string[] args)
    {
        var organizationId = args.Length > 0 && int.TryParse(args[1], out var parsedOrgValue) ? parsedOrgValue : 1;
        var personaId = args.Length > 1 && int.TryParse(args[1], out var passedPersonId) ? passedPersonId : 1;
        var resumeId = args.Length > 2 && int.TryParse(args[2], out var parsedResumeId) ? parsedResumeId : 1;

        var resumeService = ServiceProvider.GetRequiredService<IResumeService>();

        var resume = resumeService.GetResume<ResumeDetails>(organizationId, personaId, resumeId).Result;

        if (resume != null)
        {
            List<IResumeGenerator> strategies = new()
            {
                new MarkupResumeGenerator(new MarkupSettings
                {
                    UpdateReadme = true
                }),
                new PdfResumeGenerator(new PdfSettings
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
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Context;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Entities;
using ResumePro.Extensions;
using ResumePro.Generation;
using ResumePro.Interfaces;
using ResumePro.Shared;
using System.Reflection.Emit;

namespace ResumePro.Generator;

internal class Program
{
    private static readonly IServiceProvider ServiceProvider;
    private const string ReadMePath = @"..\..\..\..\readme.md";

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

    static async Task Main(string[] args)
    {
        var organizationId = args.Length > 0 && int.TryParse(args[0], out var parsedOrgValue) ? parsedOrgValue : 1;
        var personaId = args.Length > 1 && int.TryParse(args[1], out var passedPersonId) ? passedPersonId : 1;
        var resumeId = args.Length > 2 && int.TryParse(args[2], out var parsedResumeId) ? parsedResumeId : 1;
        var templateId = args.Length > 3 && int.TryParse(args[3], out var parsedTemplateId) ? parsedTemplateId : 2;

        var resumeService = ServiceProvider.GetRequiredService<IResumeService>();

        var resume = await resumeService.GetResume<ResumeDetails>(organizationId, personaId, resumeId);
        
        // generate with service
        var generatedResume = await resumeService.Generate(resume, templateId);

        if (generatedResume.IsT0)
        {
            UpdateReadMe(generatedResume.AsT0.Body);
        }

        var pdfGenerator = new PdfResumeGenerator(new PdfSettings()
        {
            CreateUpdatePdf = true,
            DisplayInExplorer = true,
            FontFamily = "Verdana"
        });

        pdfGenerator.ExecuteOperation(resume);


        Console.ReadLine();
    }

    private static void UpdateReadMe(string resumeText)
    {
        var fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ReadMePath));

        if (File.Exists(fullPath))
        {
            File.WriteAllText(fullPath, resumeText);
            Console.WriteLine("readme file updated successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}
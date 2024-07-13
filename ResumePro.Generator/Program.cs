#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using ResumePro.Context;
using ResumePro.Core.Extensions;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Generation;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;

namespace ResumePro.Generator;

internal class Program
{
    private const string ReadMePath = @"..\..\..\..\readme.md";
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
            .RegisterHandlebarsExtensions()
            .AddAutomapperProfilesFromAssemblies()
            .RegisterAllServices(typeof(ApplicationContext).Assembly)
            .Build();
    }

    private static async Task Main(string[] args)
    {
        int GetArgValue(int position, int defaultValue)
        {
            var index = position - 1;
            return args.Length > index && int.TryParse(args[index], out var parsedValue) ? parsedValue : defaultValue;
        }

        string GetArgValueString(int position, string defaultValue)
        {
            var index = position - 1;
            return args.Length > index ? args[index] : defaultValue;
        }

        var organizationId = GetArgValue(1, 1);
        var personaId = GetArgValue(2, 1);
        var resumeId = GetArgValue(3, 1);
        var templateId = GetArgValueString(4, "markdown");

        var resumeService = ServiceProvider.GetRequiredService<IResumeService>();

        var resume = await resumeService.GetResume<ResumeDetails>(organizationId, personaId, resumeId);

        OneOf<GeneratedResume, Result> generatedResume = await resumeService.Generate(resume, templateId);

        if (generatedResume.IsT0)
        {
            Console.WriteLine(generatedResume.AsT0.Body);
            UpdateReadMe(generatedResume.AsT0.Body);
        }

        var pdfGenerator = new PdfResumeGenerator(new PdfSettings
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
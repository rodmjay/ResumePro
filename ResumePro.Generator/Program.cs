#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using ResumePro.Context;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Entities;
using ResumePro.Extensions;
using ResumePro.Generator.Strategies;
using ResumePro.Services;
using ResumePro.Shared;

namespace ResumePro.Generator;

internal interface IResumeStrategy
{
    void ExecuteOperation(ResumeDetails resume);
}

internal class Program
{

    private static readonly IServiceProvider serviceProvider;

    static Program()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var services = new ServiceCollection();

        serviceProvider = services!.ConfigureApp(configuration)
            .AddDatabase<ApplicationContext>()
            .AddAutomapperProfilesFromAssemblies()
            .AddApplicationDependencies()
            .Build();
    }

    private static void Main(string[] args)
    {
        if (int.TryParse(args[0], out int resumeId))
        {
            var resumeService = serviceProvider.GetRequiredService<IResumeService>();

            var resume = resumeService.GetResume<ResumeDetails>(resumeId).Result;

            List<IResumeStrategy> strategies = new List<IResumeStrategy>
            {
                new MarkupResumeStrategy(),
                new PdfResumeStrategy()
            };

            foreach (var strategy in strategies)
            {
                strategy.ExecuteOperation(resume);
            }
        }
        else
        {
            Console.WriteLine("Please pass in a integer value for resumeID");
        }

        Console.ReadLine();
    }
    
}
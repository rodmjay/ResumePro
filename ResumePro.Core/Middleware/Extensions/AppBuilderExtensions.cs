#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ResumePro.Core.Data;
using ResumePro.Core.Data.Interfaces;
using ResumePro.Core.Data.Repositories;
using ResumePro.Core.Middleware.Builders;
using Serilog;

namespace ResumePro.Core.Middleware.Extensions;

[ExcludeFromCodeCoverage]
public static class AppBuilderExtensions
{
    private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
    {
        return $"[{nameof(AppBuilderExtensions)}.{callerName}] - {message}";
    }


    public static WebAppBuilder ConfigureWebApp(this AppBuilder builder, IWebHostEnvironment environment)
    {
        Log.Logger.Debug(GetLogMessage($"Environment: {environment.EnvironmentName}"));

        return new WebAppBuilder(builder, environment);
    }


    public static AppBuilder AddDatabase<TContext>(
        this AppBuilder builder)
        where TContext : DbContext
    {
        //var dbConnectionString = builder.KeyVaultClient
        //    .GetSecretAsync(
        //        builder.Configuration.GetValue<string>("DbConnectionSecretUri"))
        //    .Result.Value;

        Log.Logger.Debug(GetLogMessage("Adding SQL Connection"));

        builder.ConnectionString =
            builder.Configuration.GetConnectionString(builder.AppSettings.Database.ConnectionStringName);
        if (!string.IsNullOrWhiteSpace(builder.ConnectionString))
        {
            Log.Logger.Debug(
                GetLogMessage($"Connection String: {builder.AppSettings.Database.ConnectionStringName}"));


            builder.Services.TryAddScoped(typeof(IUnitOfWorkAsync), typeof(UnitOfWork));
            builder.Services.TryAddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.TryAddScoped(typeof(IRepositoryAsync<>), typeof(Repository<>));
            builder.Services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));


            var dbContextOptions = new DbContextOptionsBuilder<TContext>()
                .UseSqlServer(builder.ConnectionString,
                    opts => { opts.CommandTimeout(builder.AppSettings.Database.Timeout); })
                .Options;


            builder.Services.TryAddSingleton(dbContextOptions);

            // Finally Add the Applications DbContext:
            builder.Services.AddDbContext<TContext>(options => { options.EnableSensitiveDataLogging(); });

            builder.Services.TryAddScoped(typeof(IDataContextAsync), typeof(TContext));
        }
        else
        {
            Log.Logger.Fatal(GetLogMessage(
                $"Unable to find Connection String: {builder.AppSettings.Database.ConnectionStringName}"));
            throw new ApplicationException(
                $"Unable to find Connection String: {builder.AppSettings.Database.ConnectionStringName}");
        }


        return builder;
    }

    public static AppBuilder AddAutomapperProfilesFromAssemblies(
        this AppBuilder builder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => x.FullName.StartsWith("ResumePro")).ToList();

        foreach (var assembly in assemblies)
            if (!builder.AssembliesToMap.Contains(assembly.FullName))
                builder.AssembliesToMap.Add(assembly.FullName);

        var config = new MapperConfiguration(x => x.AddMaps(builder.AssembliesToMap));

        var mapper = config.CreateMapper();

        builder.Services.TryAddSingleton(config);
        builder.Services.TryAddScoped(sp => mapper);

        return builder;
    }

    public static AppBuilder AddCaching(this AppBuilder builder)
    {
        builder.Services.AddDistributedMemoryCache();

        return builder;
    }
}
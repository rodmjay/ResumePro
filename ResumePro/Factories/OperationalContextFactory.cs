#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Configuration;
using ResumePro.Context;

namespace ResumePro.Factories;

public sealed class OperationalContextFactory : IApplicationContextFactory
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        // Used only for EF .NET Core CLI tools (update database/migrations etc.)
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("sharedSettings.json", false, true);

        var config = builder.Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
            .EnableSensitiveDataLogging()
            .UseSqlServer(config.GetConnectionString("DefaultConnection"));

        return new ApplicationContext(optionsBuilder.Options);
    }
}
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResumePro.Users.Contexts;

namespace ResumePro.Users.Factories;

public class OperationalContextFactory : IApplicationContextFactory
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        // Used only for EF .NET Core CLI tools (update database/migrations etc.)
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("sharedSettings.json", false, true);

        var config = builder.Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        
        // Only enable sensitive data logging in development/debug environments
        #if DEBUG
        optionsBuilder.EnableSensitiveDataLogging(true);
        #else
        optionsBuilder.EnableSensitiveDataLogging(false);
        #endif
        
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

        return new ApplicationContext(optionsBuilder.Options);
    }
}

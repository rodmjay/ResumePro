using System.Reflection;
using Bespoke.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumePro.Data.Contexts;
using Serilog;
using Serilog.Extensions.Logging;

namespace ResumePro.Infrastructure.PostgreSQL;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        // Configure Serilog for design-time operations
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        Log.Information("Initializing ApplicationContextFactory with explicit logging settings.");

        // Create DbContext options
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        
        // Check if connection string is provided in args
        string? connectionString = null;
        
        if (args != null && args.Length > 0)
        {
            // Check if the argument is a connection string
            if (args[0]?.Contains("Host=") == true || args[0]?.Contains("Server=") == true)
            {
                connectionString = args[0];
                Log.Information("Using connection string from command line: {ConnectionString}", connectionString);
            }
        }
        
        // If no connection string in args, try to get from configuration
        if (string.IsNullOrEmpty(connectionString))
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile("appsettings.Migrations.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
                
            connectionString = configuration.GetConnectionString("PostgreSQLConnection") ?? "";
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("PostgreSQL connection string not found in configuration.");
            }
            
            Log.Information("Using PostgreSQL provider with connection from config: {ConnectionString}", connectionString);
        }

        // Configure PostgreSQL with migrations assembly
        if (connectionString.Contains("Host=") || connectionString.Contains("Server="))
        {
            optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly("ResumePro.Infrastructure.PostgreSQL");
                npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        }
        else
        {
            // We only support PostgreSQL
            Log.Warning("Connection string does not appear to be PostgreSQL format. Please check your configuration.");
            throw new InvalidOperationException("Only PostgreSQL connections are supported.");
        }

        // Enable sensitive data logging in development
        optionsBuilder.EnableSensitiveDataLogging();
        
        // Create logger factory for DbContext
        var loggerFactory = new SerilogLoggerFactory(Log.Logger);
        optionsBuilder.UseLoggerFactory(loggerFactory);

        // Create DbSettings for ApplicationContext
        var dbSettings = new DbSettings
        {
            MigrationsAssembly = "ResumePro.Infrastructure.PostgreSQL",
            MaxRetryCount = 5,
            MaxRetryDelaySeconds = 10,
            Timeout = 30,
            ValidateSaves = true
        };
        
        var dbSettingsOptions = Options.Create(dbSettings);

        return new ApplicationContext(optionsBuilder.Options, dbSettingsOptions);
    }
}

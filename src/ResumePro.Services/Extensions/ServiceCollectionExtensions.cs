using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Services.Implementations;
using ResumePro.Services.Interfaces;

namespace ResumePro.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register error describers
        services.AddErrorDescribers();
        
        // Register all services
        services.AddScoped<IPeopleService, PeopleService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IResumeService, ResumeService>();
        services.AddScoped<ISchoolService, SchoolService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IIdGenerationService, IdGenerationService>();
        
        return services;
    }
}

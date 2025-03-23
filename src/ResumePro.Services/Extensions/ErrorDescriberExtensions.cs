using Microsoft.Extensions.DependencyInjection;
using ResumePro.Services.ErrorDescribers;

namespace ResumePro.Services.Extensions;

public static class ErrorDescriberExtensions
{
    public static IServiceCollection AddErrorDescribers(this IServiceCollection services)
    {
        services.AddScoped<PersonErrorDescriber>();
        services.AddScoped<GeographyErrorDescriber>();
        services.AddScoped<SkillErrorDescriber>();
        services.AddScoped<CompanyErrorDescriber>();
        services.AddScoped<ResumeErrorDescriber>();
        services.AddScoped<TemplateErrorDescriber>();
        services.AddScoped<SchoolErrorDescriber>();
        services.AddScoped<CertificationErrorDescriber>();
        services.AddScoped<DegreeErrorDescriber>();
        services.AddScoped<HighlightErrorDescriber>();
        services.AddScoped<LanguageErrorDescriber>();
        services.AddScoped<ProjectErrorDescriber>();
        services.AddScoped<ReferenceErrorDescriber>();
        
        return services;
    }
}

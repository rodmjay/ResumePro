using Bespoke.IntegrationTesting.Bases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ResumePro.Api;
using ResumePro.Api.Interfaces;
using ResumePro.IntegrationTests.Proxies;
using System;
using System.Threading.Tasks;

namespace ResumePro.IntegrationTests.Tests;

[Category("Integration")]
public abstract partial class BaseApiTest : IntegrationTest<BaseApiTest, Startup>
{
    protected ICertificationsController CertificationsController => new CertificationsProxy(ApiClient);
    protected ICompaniesController CompaniesController => new CompaniesProxy(ApiClient);
    protected ICompanySkillsController CompanySkillsController => new CompanySkillsProxy(ApiClient);
    protected IDegreesController DegreesController => new DegreesProxy(ApiClient);
    protected IFiltersController FiltersController => new FiltersProxy(ApiClient);
    protected IHighlightsController HighlightsController => new HighlightsProxy(ApiClient);
    protected IOrganizationSettingsController OrganizationSettingsController => new OrganizationSettingsProxy(ApiClient);
    protected IPeopleController PeopleController => new PeopleProxy(ApiClient);
    protected IPersonLanguagesController PersonLanguagesController => new PersonLanguagesProxy(ApiClient);
    protected IPersonSkillsController PersonSkillsController => new PersonSkillsProxy(ApiClient);
    protected IPositionsController PositionsController => new PositionsProxy(ApiClient);
    protected IProjectHighlightsController ProjectHighlightsController => new ProjectHighlightsProxy(ApiClient);
    protected IProjectsController ProjectsController => new ProjectsProxy(ApiClient);
    protected IReferencesController ReferencesController => new ReferencesProxy(ApiClient);
    protected IResumeController ResumeController => new ResumeProxy(ApiClient);
    protected IResumeSettingsController ResumeSettingsController => new ResumeSettingsProxy(ApiClient);
    protected IResumeSkillsController ResumeSkillsController => new ResumeSkillsProxy(ApiClient);
    protected ISchoolsController SchoolsController => new SchoolsProxy(ApiClient);
    protected ISkillsController SkillsController => new SkillsProxy(ApiClient);
    protected ITemplatesController TemplatesController => new TemplatesProxy(ApiClient);
    protected ITextController TextController => new TextProxy(ApiClient);
    protected IUserController UserController => new UserProxy(ApiClient);

    protected static bool IsRunningOnCI()
    {
        return Environment.GetEnvironmentVariable("CI") != null || 
               Environment.GetEnvironmentVariable("GITHUB_ACTIONS") != null;
    }

    [OneTimeSetUp]
    public virtual async Task SetupFixture()
    {
        try
        {
            // Always use PostgreSQL for tests
            var configuration = ServiceProvider.GetRequiredService<IConfiguration>();
            
            Console.WriteLine("Test configuration: Using PostgreSQL database provider");
            
            // Reset database using PostgreSQL
            await ResetDatabase();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during database setup: {ex.Message}");
            // Continue with tests even if database setup fails
            // This allows us to run tests that don't depend on the database
        }
    }

    [OneTimeTearDown]
    public virtual async Task TeardownFixture()
    {
        try
        {
            await DeleteDatabase();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during database teardown: {ex.Message}");
        }
    }
}

#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using IdentityModel.Client;
using NUnit.Framework;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Proxies;
using ResumePro.Testing.Bases;

namespace ResumePro.Api.Testing;

public abstract class BaseApiTest : IntegrationTest<BaseApiTest, Startup>
{
    protected IPeopleController PeopleProxy => new PeopleProxy(ApiClient);
    protected ISchoolsController SchoolsProxy => new SchoolsProxy(ApiClient);
    protected ICertificationsController CertificationsProxy => new CertificationsProxy(ApiClient);
    protected IDegreesController DegreesProxy => new DegreesProxy(ApiClient);
    protected IFiltersController FiltersProxy => new FiltersProxy(ApiClient);
    protected IProjectsController ProjectsProxy => new ProjectsProxy(ApiClient);
    protected IHighlightsController HighlightsProxy => new HighlightsProxy(ApiClient);
    protected ICompaniesController CompaniesProxy => new CompaniesProxy(ApiClient);
    protected ICompanySkillsController CompanySkillsProxy => new CompanySkillsProxy(ApiClient);
    protected IOrganizationSettingsController OrganizationSettingsProxy => new OrganizationSettingsProxy(ApiClient);
    protected IReferencesController ReferencesProxy => new ReferencesProxy(ApiClient);
    protected IResumeController ResumeProxy => new ResumeProxy(ApiClient);
    protected IResumeSkillsController ResumeSkillsProxy => new ResumeSkillsProxy(ApiClient);
    protected ISkillsController SkillsProxy => new SkillsProxy(ApiClient);
    protected ITemplatesController TemplatesProxy => new TemplatesProxy(ApiClient);
    protected IPersonSkillsController PersonSkillsProxy => new PersonSkillsProxy(ApiClient);
    protected IPersonLanguagesController PersonLanguagesProxy => new PersonLanguagesProxy(ApiClient);

    [OneTimeSetUp]
    public virtual async Task SetupFixture()
    {
        await ResetDatabase();
        var accessToken = await GetAccessToken("admin@admin.com", "ASDFasdf!");
        ApiClient.SetBearerToken(accessToken);
    }

    [OneTimeTearDown]
    public virtual async Task TeardownFixture()
    {
        await DeleteDatabase();
    }
}
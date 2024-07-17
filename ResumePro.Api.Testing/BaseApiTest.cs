#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using IdentityModel.Client;
using NUnit.Framework;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Proxies;
using ResumePro.Testing.Bases;
using HighlightsController = ResumePro.Api.Controllers.HighlightsController;

namespace ResumePro.Api.Testing;

public abstract class BaseApiTest : IntegrationTest<BaseApiTest, Startup>
{

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

    protected IPeopleController PeopleProxy => new PeopleProxy(ApiClient);
    protected ISchoolsController SchoolsProxy => new SchoolsProxy(ApiClient);
    protected ICertificationsController CertificationsProxy => new CertificationsProxy(ApiClient);
    protected IDegreesController DegreesProxy => new DegreesProxy(ApiClient);
    protected IFiltersController FiltersProxy => new FiltersProxy(ApiClient);
    protected IHighlightsController HighlightsProxy => new HighlightsProxy(ApiClient);
    protected IJobsController JobsProxy => new JobsProxy(ApiClient);
    protected IOrganizationSettingsController OrganizationSettingsProxy => new OrganizationSettingsProxy(ApiClient);
    protected IReferencesController ReferencesProxy => new ReferencesProxy(ApiClient);
    protected IResumeController ResumeProxy => new ResumeProxy(ApiClient);
    protected IResumeSkillsController ResumeSkillsProxy => new ResumeSkillsProxy(ApiClient);
    protected ISkillsController SkillsProxy => new SkillsProxy(ApiClient);

}
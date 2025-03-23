using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class CompanySkillsProxy : BaseProxy, ICompanySkillsController
{
    private const string RootUrl = "v1.0/people/{0}/companies/{1}/skills";

    public CompanySkillsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result> AddCompanySkill(int personId, int companyId, int skillId)
    {
        return await DoGetAsync<Result>($"{string.Format(RootUrl, personId, companyId)}/{skillId}");
    }

    public async Task<Result> DeleteCompanySkill(int personId, int companyId, int skillId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId, companyId)}/{skillId}");
    }
}

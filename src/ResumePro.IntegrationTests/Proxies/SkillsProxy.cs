using Bespoke.IntegrationTesting.Bases;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class SkillsProxy : BaseProxy, ISkillsController
{
    private const string RootUrl = "v1.0/skills";

    public SkillsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<SkillDto>> GetSkills()
    {
        return await DoGetAsync<List<SkillDto>>(RootUrl);
    }
}

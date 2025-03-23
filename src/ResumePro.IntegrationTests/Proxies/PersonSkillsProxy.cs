using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class PersonSkillsProxy : BaseProxy, IPersonSkillsController
{
    private const string RootUrl = "v1.0/people/{0}/skills";

    public PersonSkillsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<PersonaSkillDto>> GetSkills(int personId)
    {
        return await DoGetAsync<List<PersonaSkillDto>>(string.Format(RootUrl, personId));
    }

    public async Task<Result> ToggleSkill(int personId, int skillId)
    {
        return await DoGetAsync<Result>($"{string.Format(RootUrl, personId)}/{skillId}");
    }
}

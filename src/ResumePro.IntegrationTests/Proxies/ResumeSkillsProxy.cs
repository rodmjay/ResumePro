using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class ResumeSkillsProxy : BaseProxy, IResumeSkillsController
{
    private const string RootUrl = "v1.0/people/{0}/resume/{1}/skills";

    public ResumeSkillsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result> AddResumeSkill(int personId, int resumeId, int skillId)
    {
        return await DoGetAsync<Result>($"{string.Format(RootUrl, personId, resumeId)}/{skillId}");
    }

    public async Task<Result> DeleteResumeSkill(int personId, int resumeId, int skillId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId, resumeId)}/{skillId}");
    }
}

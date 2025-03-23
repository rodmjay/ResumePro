using Bespoke.IntegrationTesting.Bases;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class ResumeSettingsProxy : BaseProxy, IResumeSettingsController
{
    private const string RootUrl = "v1.0/people/{0}/resumes/{1}/settings";

    public ResumeSettingsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ActionResult<ResumeSettingsDto>> UpdateSettings(int personId, int resumeId,
        ResumeSettingsOptions options)
    {
        return await DoPutActionResultAsync<ResumeSettingsOptions, ResumeSettingsDto>(string.Format(RootUrl, personId, resumeId), options);
    }
}

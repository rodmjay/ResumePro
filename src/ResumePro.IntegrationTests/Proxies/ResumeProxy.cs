using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class ResumeProxy : BaseProxy, IResumeController
{
    private const string RootUrl = "v1.0/people/{0}/resumes";

    public ResumeProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ResumeDetails> GetResume(int personId, int resumeId)
    {
        return await DoGetAsync<ResumeDetails>($"{string.Format(RootUrl, personId)}/{resumeId}");
    }

    public async Task<List<ResumeDto>> GetResumes(int personId)
    {
        return await DoGetAsync<List<ResumeDto>>(string.Format(RootUrl, personId));
    }

    public async Task<ActionResult<ResumeDetails>> CreateResume(int personId, ResumeOptions options)
    {
        return await DoPostActionResultAsync<ResumeOptions, ResumeDetails>(string.Format(RootUrl, personId), options);
    }

    public async Task<ActionResult<ResumeDetails>> UpdateResume(int personId, int resumeId, ResumeOptions options)
    {
        return await DoPutActionResultAsync<ResumeOptions, ResumeDetails>($"{string.Format(RootUrl, personId)}/{resumeId}", options);
    }

    public async Task<Result> DeleteResume(int personId, int resumeId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId)}/{resumeId}");
    }
}

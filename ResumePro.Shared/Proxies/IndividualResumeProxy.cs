using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualResumeProxy(HttpClient httpClient) : BaseProxy(httpClient), IIndividualResumeController
{
    public async Task<ResumeDetails> GetResume(int resumeId)
    {
        return await DoGet<ResumeDetails>($"v1.0/resumes/{resumeId}")
            .ConfigureAwait(false);
    }

    public async Task<List<ResumeDto>> GetResumes()
    {
        return await DoGet<List<ResumeDto>>($"v1.0/resumes")
            .ConfigureAwait(false);
    }

    public async Task<string> Generate(int resumeId)
    {
        return await DoGet<string>($"v1.0/resumes/{resumeId}");
    }

    public async Task<ActionResult<ResumeDetails>> CreateResume( ResumeOptions options)
    {
        return await DoPostActionResult<ResumeOptions, ResumeDetails>($"v1.0/resumes", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<ResumeDetails>> UpdateResume( int resumeId, ResumeOptions options)
    {
        return await DoPutActionResult<ResumeOptions, ResumeDetails>($"v1.0/resumes/{resumeId}",
                options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteResume(int resumeId)
    {
        return await DoDelete<Result>($"v1.0/resumes/{resumeId}");
    }

    public async Task<IActionResult> PdfAnonymous(int resumeId, int organizationId)
    {
        return await DoActionResultGet($"v1.0/resumes/{resumeId}/pdf?organizationId={organizationId}");
    }
}
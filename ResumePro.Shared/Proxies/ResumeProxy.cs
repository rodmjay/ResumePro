#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class ResumeProxy(HttpClient httpClient) : BaseProxy(httpClient), IResumeController
{
    public async Task<ResumeDetails> GetResume(int personId, int resumeId)
    {
        return await DoGet<ResumeDetails>($"v1.0/people/{personId}/resumes/{resumeId}")
            .ConfigureAwait(false);
    }

    public async Task<List<ResumeDto>> GetResumes(int personId)
    {
        return await DoGet<List<ResumeDto>>($"v1.0/people/{personId}/resumes")
            .ConfigureAwait(false);
    }

    public async Task<string> Generate(int personId, int resumeId)
    {
        return await DoGet<string>($"v1.0/people/{personId}/resumes/{resumeId}");
    }

    public async Task<ActionResult<ResumeDetails>> CreateResume(int personId, ResumeOptions options)
    {
        return await DoPostActionResult<ResumeOptions, ResumeDetails>($"v1.0/people/{personId}/resumes", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<ResumeDetails>> UpdateResume(int personId, int resumeId, ResumeOptions options)
    {
        return await DoPutActionResult<ResumeOptions, ResumeDetails>($"v1.0/people/{personId}/resumes/{resumeId}",
                options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteResume(int personId, int resumeId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/resumes/{resumeId}");
    }

    public async Task<IActionResult> PdfAnonymous(int personId, int resumeId, int organizationId)
    {
        return await DoActionResultGet($"v1.0/people/{personId}/resumes/{resumeId}/pdf?organizationId={organizationId}");
    }
}
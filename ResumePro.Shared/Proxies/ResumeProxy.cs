#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class ResumeProxy : BaseProxy, IResumeController
{
    public ResumeProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ResumeDetails> Get(int personId, int resumeId)
    {
        return await DoGet<ResumeDetails>($"v1.0/people/{personId}/resumes/{resumeId}");
    }

    public async Task<IActionResult> Generate(int personId, int resumeId, string templateId)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> Download(int personId, int resumeId, int templateId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ResumeDto>> GetResumes(int personId, int resumeId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<ResumeDetails>> CreateResume(int personId, ResumeOptions options)
    {
        return await DoPost<ResumeOptions, ResumeDetails>($"v1.0/people/{personId}/resumes", options);
    }

    public async Task<ActionResult<ResumeDetails>> UpdateResume(int personId, int resumeId, ResumeOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteResume(int personId, int resumeId)
    {
        throw new NotImplementedException();
    }
}
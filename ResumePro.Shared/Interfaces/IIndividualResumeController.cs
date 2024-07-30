#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualResumeController
{
    Task<ResumeDetails> GetResume(int resumeId);

    Task<List<ResumeDto>> GetResumes();

    Task<string> Generate(int resumeId);

    Task<ActionResult<ResumeDetails>> CreateResume(
        ResumeOptions options);

    Task<ActionResult<ResumeDetails>> UpdateResume(
        int resumeId,
        ResumeOptions options);

    Task<Result> DeleteResume(
        int resumeId);

    Task<IActionResult> PdfAnonymous(int resumeId, int organizationId);
}
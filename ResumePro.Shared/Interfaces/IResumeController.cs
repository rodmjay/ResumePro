#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IResumeController
{
    Task<ResumeDetails> GetResume(int personId, int resumeId);

    Task<IActionResult> Generate(int personId, int resumeId,
         string templateId);

    Task<IActionResult> Download(int personId, int resumeId,
         int templateId);

    Task<List<ResumeDto>> GetResumes(int personId);

    Task<ActionResult<ResumeDetails>> CreateResume(int personId,
         ResumeOptions options);

    Task<ActionResult<ResumeDetails>> UpdateResume(int personId,
         int resumeId,
         ResumeOptions options);

    Task<Result> DeleteResume(int personId,
         int resumeId);
}
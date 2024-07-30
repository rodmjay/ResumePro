#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualResumeSettingsController
{

    Task<ActionResult<ResumeSettingsDto>> UpdateSettings(int resumeId,
        ResumeSettingsOptions options);
}
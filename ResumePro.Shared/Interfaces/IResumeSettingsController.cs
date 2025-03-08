#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

namespace ResumePro.Shared.Interfaces;

public interface IResumeSettingsController
{

    Task<ActionResult<ResumeSettingsDto>> UpdateSettings(
        int personId, int resumeId,
        ResumeSettingsOptions options);
}
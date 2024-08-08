#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualResumeSettingsProxy(HttpClient httpClient)
    : BaseProxy(httpClient), IIndividualResumeSettingsController
{
    public async Task<ActionResult<ResumeSettingsDto>> UpdateSettings(int resumeId, ResumeSettingsOptions options)
    {
        return await DoPutActionResult<ResumeSettingsOptions, ResumeSettingsDto>(
                $"v1.0/resumes/{resumeId}/settings", options)
            .ConfigureAwait(false);
    }
}
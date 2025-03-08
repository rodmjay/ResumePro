#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;

namespace ResumePro.Shared.Proxies;

public sealed class ResumeSettingsProxy : BaseProxy, IResumeSettingsController
{
    public ResumeSettingsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ActionResult<ResumeSettingsDto>> UpdateSettings(int personId, int resumeId, ResumeSettingsOptions options)
    {
        return await DoPutActionResult<ResumeSettingsOptions, ResumeSettingsDto>(
                $"v1.0/people/{personId}/resumes/{resumeId}/settings", options)
            .ConfigureAwait(false);
    }
}
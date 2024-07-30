#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Proxies;

public sealed class ResumeSkillsProxy(HttpClient httpClient) : BaseProxy(httpClient), IResumeSkillsController
{
    public async Task<Result> AddResumeSkill(int personId, int resumeId, int skillId)
    {
        return await DoPatch<Result>($"v1.0/people/{personId}/resumes/{resumeId}/skills/{skillId}")
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteResumeSkill(int personId, int resumeId, int skillId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/resumes/{resumeId}/skills/{skillId}")
            .ConfigureAwait(false);
    }
}
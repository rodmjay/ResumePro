#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion


namespace ResumePro.Shared.Proxies;

public sealed class ResumeSkillsProxy : BaseProxy, IResumeSkillsController
{
    public ResumeSkillsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result> AddResumeSkill(int personId, int resumeId, int skillId)
    {
        return await DoPatch<Result>($"v1.0/people/{personId}/resume/{resumeId}/skills/{skillId}")
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteResumeSkill(int personId, int resumeId, int skillId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/resume/{resumeId}/skills/{skillId}")
            .ConfigureAwait(false);
    }
}
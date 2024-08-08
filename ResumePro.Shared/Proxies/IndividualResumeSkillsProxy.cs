#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualResumeSkillsProxy(HttpClient httpClient)
    : BaseProxy(httpClient), IIndividualResumeSkillsController
{
    public async Task<Result> AddResumeSkill( int resumeId, int skillId)
    {
        return await DoPatch<Result>($"v1.0/resumes/{resumeId}/skills/{skillId}")
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteResumeSkill( int resumeId, int skillId)
    {
        return await DoDelete<Result>($"v1.0/resumes/{resumeId}/skills/{skillId}")
            .ConfigureAwait(false);
    }
}
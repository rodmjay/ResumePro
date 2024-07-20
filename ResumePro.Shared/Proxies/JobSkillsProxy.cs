#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Proxies;

public sealed class JobSkillsProxy(HttpClient httpClient) : BaseProxy(httpClient), IJobSkillsController
{
    public async Task<Result> AddJobSkill(int personId, int jobId, int skillId)
    {
        return await DoPatch<Result>($"v1.0/people/{personId}/jobs/{jobId}/skills/{skillId}")
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteJobSkill(int personId, int jobId, int skillId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/jobs/{jobId}/skills/{skillId}")
            .ConfigureAwait(false);
    }
}
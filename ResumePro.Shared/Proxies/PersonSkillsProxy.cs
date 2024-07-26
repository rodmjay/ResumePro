#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Shared.Proxies;

public sealed class PersonSkillsProxy(HttpClient httpClient) : BaseProxy(httpClient), IPersonSkillsController
{
    public async Task<List<PersonaSkillDto>> GetSkills(int personId)
    {
        return await DoGet<List<PersonaSkillDto>>($"v1.0/people/{personId}/skills")
            .ConfigureAwait(false);
    }

    public async Task<Result> ToggleSkill(int personId, int skillId)
    {
        return await DoPatch<Result>($"v1.0/people/{personId}/skills/{skillId}")
            .ConfigureAwait(false);
    }
}
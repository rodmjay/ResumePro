#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualSkillsProxy(HttpClient httpClient) : BaseProxy(httpClient), IIndividualSkillsController
{
    public async Task<List<PersonaSkillDto>> GetSkills()
    {
        return await DoGet<List<PersonaSkillDto>>($"v1.0/skills/personal")
            .ConfigureAwait(false);
    }

    public async Task<Result> ToggleSkill(int skillId)
    {
        return await DoPatch<Result>($"v1.0/skills/personal/{skillId}")
            .ConfigureAwait(false);
    }
}
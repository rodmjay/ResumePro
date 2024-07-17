#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Shared.Proxies;

public sealed class SkillsProxy(HttpClient httpClient) : BaseProxy(httpClient), ISkillsController
{
    public async Task<List<SkillDto>> GetSkills()
    {
        return await DoGet<List<SkillDto>>("v1.0/skills").ConfigureAwait(false);
    }
}
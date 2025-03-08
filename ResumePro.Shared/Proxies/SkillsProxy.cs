#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Proxies;

public sealed class SkillsProxy : BaseProxy, ISkillsController
{
    public SkillsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<SkillDto>> GetSkills()
    {
        return await DoGet<List<SkillDto>>("v1.0/skills")
            .ConfigureAwait(false);
    }
}
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Proxies;

public class SkillsProxy(HttpClient httpClient) : BaseProxy(httpClient), ISkillsController
{
    public async Task<List<SkillDto>> GetSkills()
    {
        throw new NotImplementedException();
    }
}
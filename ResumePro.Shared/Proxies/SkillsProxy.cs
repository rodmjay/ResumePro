#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Proxies;

public class SkillsProxy : BaseProxy, ISkillsController
{
    public SkillsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<SkillDto>> GetSkills()
    {
        throw new NotImplementedException();
    }
}
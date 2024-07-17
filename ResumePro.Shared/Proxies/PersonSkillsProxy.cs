#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class PersonSkillsProxy : BaseProxy, IPersonSkillsController
{
    public PersonSkillsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<PersonaSkillDto>> GetSkills(int personId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> AddOrUpdateSkill(int personId, PersonaSkillsOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeletePersonalSkill(int personId, int skillId)
    {
        throw new NotImplementedException();
    }
}
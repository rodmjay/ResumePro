#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Proxies;

public class ResumeSkillsProxy(HttpClient httpClient) : BaseProxy(httpClient), IResumeSkillsController
{
    public async Task<Result> AddResumeSkill(int personId, int resumeId, int skillId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteResumeSkill(int personId, int resumeId, int skillId)
    {
        throw new NotImplementedException();
    }
}
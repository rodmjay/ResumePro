#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;

namespace ResumePro.Shared.Interfaces;

public interface IResumeSkillsController
{
    Task<Result> AddResumeSkill(int personId, int resumeId,
        int skillId);

    Task<Result> DeleteResumeSkill(int personId, int resumeId,
        int skillId);
}
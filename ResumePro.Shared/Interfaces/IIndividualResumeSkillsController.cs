#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualResumeSkillsController
{
    Task<Result> AddResumeSkill(int resumeId,
        int skillId);

    Task<Result> DeleteResumeSkill(int resumeId,
        int skillId);
}
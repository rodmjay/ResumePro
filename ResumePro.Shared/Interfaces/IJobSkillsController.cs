#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;

namespace ResumePro.Shared.Interfaces;

public interface IJobSkillsController
{
    Task<Result> AddJobSkill(int personId, int jobId,
         int skillId);

    Task<Result> DeleteJobSkill(int personId, int jobId,
       int skillId);
}
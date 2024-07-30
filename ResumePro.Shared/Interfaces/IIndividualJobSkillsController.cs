#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualJobSkillsController
{
    Task<Result> AddJobSkill( int jobId,
        int skillId);

    Task<Result> DeleteJobSkill( int jobId,
        int skillId);
}
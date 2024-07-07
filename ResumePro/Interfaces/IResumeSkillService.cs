#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared.Common;

namespace ResumePro.Interfaces;

public interface IResumeSkillService : IService<ResumeSkill>
{
    Task<Result> AddResumeSkill(int organizationId, int personId, int resumeId, int skillId);

    Task<Result> DeleteResumeSkill(int organizationId, int personId, int resumeId, int skillId);
}
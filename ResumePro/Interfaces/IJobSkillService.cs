#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IJobSkillService : IService<JobSkill>
{
    Task<Result> AddJobSkill(int organizationId, int personId, int jobId, int skillId);
    Task<Result> DeleteJobSkill(int organizationId, int personId, int jobId, int skillId);
}
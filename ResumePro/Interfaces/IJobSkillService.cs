#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IJobSkillService : IService<JobSkill>
{
    Task<OneOf<JobSkillDto, Result>> AddJobSkill(int organizationId, int personId, int jobId);
}
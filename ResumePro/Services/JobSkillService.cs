#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class JobSkillService(IServiceProvider serviceProvider)
    : BaseService<JobSkill>(serviceProvider), IJobSkillService
{
    public Task<OneOf<JobSkillDto, Result>> AddJobSkill(int organizationId, int personId, int jobId)
    {
        throw new NotImplementedException();
    }
}
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class JobSkillService : BaseService<JobSkill>, IJobSkillService
{
    public JobSkillService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
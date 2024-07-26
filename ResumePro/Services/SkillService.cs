#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class SkillService(IServiceProvider serviceProvider) : BaseService<Skill>(serviceProvider), ISkillService
{
    private IQueryable<Skill> Skills => Repository.Queryable();

    public Task<List<T>> GetSkills<T>() where T : SkillDto
    {
        return Skills.AsNoTracking().ProjectTo<T>(Mapper).ToListAsync();
    }

    public Task<List<SkillDto>> GetSkillsDropdown()
    {
        return Skills.AsNoTracking().ProjectTo<SkillDto>(Mapper).ToListAsync();
    }
}
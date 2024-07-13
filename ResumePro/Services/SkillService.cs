#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class SkillService : BaseService<Skill>, ISkillService
{
    public SkillService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<Skill> Skills => Repository.Queryable();

    public Task<List<T>> GetSkills<T>() where T : SkillDto
    {
        return Skills.AsNoTracking().ProjectTo<T>(Mapper).ToListAsync();
    }

    public Task<List<DropdownItem>> GetSkillsDropdown()
    {
        return Skills.AsNoTracking().ProjectTo<DropdownItem>(Mapper).ToListAsync();
    }
}
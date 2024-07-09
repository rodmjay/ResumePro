#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;

namespace ResumePro.Services;

public class SkillService : BaseService<Skill>, ISkillService
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
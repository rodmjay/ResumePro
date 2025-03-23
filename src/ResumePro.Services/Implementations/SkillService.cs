using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Implementations;

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

    public Task<List<SkillDto>> GetSkillsDropdown()
    {
        return Skills.AsNoTracking().ProjectTo<SkillDto>(Mapper).ToListAsync();
    }
}
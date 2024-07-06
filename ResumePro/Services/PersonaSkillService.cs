#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Services;

public class PersonaSkillService : BaseService<PersonaSkill>, IPersonalSkillsService
{
    public PersonaSkillService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<PersonaSkill> PersonalSkills => Repository.Queryable();

    public Task<List<T>> GetPersonaSkills<T>(int organizationId, int personId) where T : PersonaSkillDto
    {
        return PersonalSkills.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public async Task<Result> AddOrUpdatePersonaSkill(int organizationId, int personId, PersonaSkillsOptions options)
    {
        var personalSkill =
            await PersonalSkills.Where(x =>
                    x.OrganizationId == organizationId && x.PersonaId == personId && x.SkillId == options.SkillId)
                .FirstOrDefaultAsync();

        if (personalSkill == null)
        {
            personalSkill = new PersonaSkill()
            {
                ObjectState = ObjectState.Added,
                OrganizationId = organizationId,
                PersonaId = personId,
                SkillId = options.SkillId,
                Rating = options.Rating,
            };
        }
        else
        {
            personalSkill.ObjectState = ObjectState.Modified;
            personalSkill.Rating = options.Rating;
        }

        var changes = Repository.InsertOrUpdateGraph(personalSkill, true);
        if (changes > 0)
        {
            return Result.Success();
        }

        return Result.Failed();
    }

    public async Task<Result> DeletePersonalSkill(int organizationId, int personId, int skillId)
    {
        var changes = await Repository.DeleteAsync(x =>
            x.OrganizationId == organizationId && x.PersonaId == personId && x.SkillId == skillId);

        return changes ? Result.Success() : Result.Failed();
    }
}
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Data.Interfaces;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Services;

public class PersonaSkillService : BaseService<PersonaSkill>, IPersonaSkillService
{
    private readonly IRepositoryAsync<Resume> _resumeRepo;

    public PersonaSkillService(IServiceProvider serviceProvider, IRepositoryAsync<Resume> resumeRepo) : base(serviceProvider)
    {
        _resumeRepo = resumeRepo;
    }

    private IQueryable<PersonaSkill> PersonalSkills => Repository.Queryable();
    private IQueryable<Resume> Resumes => _resumeRepo.Queryable();

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
            personalSkill = new PersonaSkill
            {
                ObjectState = ObjectState.Added,
                OrganizationId = organizationId,
                PersonaId = personId,
                SkillId = options.SkillId,
                Rating = options.Rating
            };
        }
        else
        {
            personalSkill.ObjectState = ObjectState.Modified;
            personalSkill.Rating = options.Rating;
        }

        bool isAdd = personalSkill.ObjectState == ObjectState.Added;

        var changes = Repository.InsertOrUpdateGraph(personalSkill, true);
        if (changes > 0)
        {
            if (isAdd)
            {
                List<Resume> resumes = await Resumes.Include(x => x.ResumeSettings)
                    .Include(x => x.Skills)
                    .ThenInclude(x => x.Skill)
                    .Where(x => x.PersonaId == personId && x.OrganizationId == organizationId)
                    .ToListAsync();

                foreach (Resume resume in resumes)
                {
                    if (resume.ResumeSettings is { AttachAllSkills: true })
                    {
                        resume.ObjectState = ObjectState.Modified;
                        resume.Skills.Add(new ResumeSkill()
                        {
                            SkillId = personalSkill.SkillId,
                            PersonaId = personalSkill.PersonaId,
                            ResumeId = resume.Id,
                            OrganizationId = organizationId,
                            ObjectState = ObjectState.Added
                        });
                    }
                    _resumeRepo.InsertOrUpdateGraph(resume, false);
                }

                _resumeRepo.Commit();
            }

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
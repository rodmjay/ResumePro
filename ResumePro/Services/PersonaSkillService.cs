#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class PersonaSkillService(IServiceProvider serviceProvider, IRepositoryAsync<Resume> resumeRepo)
    : BaseService<PersonaSkill>(serviceProvider), IPersonaSkillService
{
    private IQueryable<PersonaSkill> PersonalSkills => Repository.Queryable();
    private IQueryable<Resume> Resumes => resumeRepo.Queryable();

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

        var isAdd = personalSkill.ObjectState == ObjectState.Added;

        var changes = Repository.InsertOrUpdateGraph(personalSkill, true);
        if (changes > 0)
        {
            if (isAdd)
            {
                var resumes = await Resumes.Include(x => x.ResumeSettings)
                    .ThenInclude(x => x.OrganizationSettings)
                    .Include(x => x.Skills)
                    .ThenInclude(x => x.Skill)
                    .Where(x => x.PersonaId == personId && x.OrganizationId == organizationId)
                    .ToListAsync();

                foreach (var resume in resumes)
                {
                    var settings = Mapper.Map<ResumeSettings>(resume.ResumeSettings);
                    if (settings.AttachAllSkills.Value)
                    {
                        resume.ObjectState = ObjectState.Modified;
                        resume.Skills.Add(new ResumeSkill
                        {
                            SkillId = personalSkill.SkillId,
                            PersonaId = personalSkill.PersonaId,
                            ResumeId = resume.Id,
                            OrganizationId = organizationId,
                            ObjectState = ObjectState.Added
                        });
                    }

                    resumeRepo.InsertOrUpdateGraph(resume);
                }

                resumeRepo.Commit();
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
using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Bespoke.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class PersonaSkillService : BaseService<PersonaSkill>, IPersonaSkillService
{
    private readonly IRepositoryAsync<Resume> _resumeRepo;

    public PersonaSkillService(IServiceProvider serviceProvider, IRepositoryAsync<Resume> resumeRepo) : base(
        serviceProvider)
    {
        _resumeRepo = resumeRepo;
    }

    private IQueryable<PersonaSkill> PersonalSkills => Repository.Queryable();
    private IQueryable<Resume> Resumes => _resumeRepo.Queryable();

    public Task<List<T>> GetPersonaSkills<T>(int organizationId, int personId) where T : PersonaSkillDto
    {
        return PersonalSkills.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public async Task<Result> TogglePersonalSkill(int organizationId, int personId, int skillId)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, SkillId: {@skillId}"),
            organizationId, personId, skillId);

        var personalSkill =
            await PersonalSkills.Where(x =>
                    x.OrganizationId == organizationId && x.PersonId == personId && x.SkillId == skillId)
                .FirstOrDefaultAsync();

        if (personalSkill == null)
            personalSkill = new PersonaSkill
            {
                ObjectState = ObjectState.Added,
                OrganizationId = organizationId,
                PersonId = personId,
                SkillId = skillId
            };
        else
            personalSkill.ObjectState = ObjectState.Deleted;

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
                    .Where(x => x.PersonId == personId && x.OrganizationId == organizationId)
                    .ToListAsync();

                foreach (var resume in resumes)
                {
                    var settings = Mapper.Map<ResumeSettingsDto>(resume.ResumeSettings);
                    if (settings.AttachAllSkills)
                    {
                        resume.ObjectState = ObjectState.Modified;
                        resume.Skills.Add(new ResumeSkill
                        {
                            SkillId = personalSkill.SkillId,
                            PersonId = personalSkill.PersonId,
                            ResumeId = resume.Id,
                            OrganizationId = organizationId,
                            ObjectState = ObjectState.Added
                        });
                    }

                    _resumeRepo.InsertOrUpdateGraph(resume);
                }

                _resumeRepo.Commit();
            }

            return Result.Success();
        }

        return Result.Failed();
    }

    public async Task<Result> DeletePersonalSkill(int organizationId, int personId, int skillId)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, SkillId: {@skillId}"),
            organizationId, personId, skillId);

        var changes = await Repository.DeleteAsync(x =>
            x.OrganizationId == organizationId && x.PersonId == personId && x.SkillId == skillId);

        return changes ? Result.Success() : Result.Failed();
    }
}
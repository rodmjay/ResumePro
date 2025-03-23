using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.Interfaces;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ResumeSkillService : BaseService<ResumeSkill>, IResumeSkillService
{
    public ResumeSkillService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<ResumeSkill> ResumeSkills => Repository.Queryable();

    public async Task<Result> AddResumeSkill(int organizationId, int personId, int resumeId,
        int skillId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, ResumeId: {@resumeId}, SkillId: {@skillId}"),
            organizationId, personId, resumeId, skillId);

        var resumeSkill = await ResumeSkills.Where(x => x.OrganizationId == organizationId
                                                        && x.ResumeId == resumeId
                                                        && x.PersonId == personId
                                                        && x.SkillId == skillId)
            .FirstOrDefaultAsync();

        if (resumeSkill != null) return Result.Failed();

        resumeSkill = new ResumeSkill
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            PersonId = personId,
            ResumeId = resumeId
        };

        var changes = Repository.InsertOrUpdateGraph(resumeSkill, true);
        if (changes > 0) return Result.Success();

        return Result.Failed();
    }

    public Task<Result> DeleteResumeSkill(int organizationId, int personId, int resumeId, int skillId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, ResumeId: {@resumeId}, SkillId: {@skillId}"),
            organizationId, personId, resumeId, skillId);

        return Task.FromResult(Result.Failed());
    }
}
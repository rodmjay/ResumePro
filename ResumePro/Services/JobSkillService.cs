#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Logging;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class JobSkillService(IServiceProvider serviceProvider)
    : BaseService<JobSkill>(serviceProvider), IJobSkillService
{
    private IQueryable<JobSkill> JobSkills => Repository.Queryable();

    public async Task<Result> AddJobSkill(int organizationId, int personId, int jobId, int skillId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, SkillId: {@skillId}"),
            organizationId, personId, jobId, skillId);

        var jobSkill = await JobSkills.Where(x => x.OrganizationId == organizationId
                                                  && x.PersonaId == personId
                                                  && x.JobId == jobId
                                                  && x.SkillId == skillId)
            .FirstOrDefaultAsync();

        if (jobSkill != null) return Result.Failed();

        jobSkill = new JobSkill
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            PersonaId = personId,
            JobId = jobId,
            SkillId = skillId
        };

        var changes = Repository.InsertOrUpdateGraph(jobSkill, true);
        if (changes > 0) return Result.Success();

        return Result.Failed();
    }

    public async Task<Result> DeleteJobSkill(int organizationId, int personId, int jobId, int skillId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, ResumeId: {@resumeId}, SkillId: {@skillId}"),
            organizationId, personId, jobId, skillId);

        var jobSkill = await JobSkills.Where(x => x.OrganizationId == organizationId
                                                  && x.PersonaId == personId
                                                  && x.JobId == jobId
                                                  && x.SkillId == skillId)
            .FirstOrDefaultAsync();

        if (jobSkill == null) return Result.Failed();

        jobSkill.ObjectState = ObjectState.Deleted;

        var changes = Repository.InsertOrUpdateGraph(jobSkill, true);
        if (changes > 0) return Result.Success();

        return Result.Failed();
    }
}
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Logging;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class CompanySkillService(IServiceProvider serviceProvider)
    : BaseService<CompanySkill>(serviceProvider), ICompanySkillService
{
    private IQueryable<CompanySkill> JobSkills => Repository.Queryable();

    public async Task<Result> AddCompanySkill(int organizationId, int personId, int companyId, int skillId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, SkillId: {@skillId}"),
            organizationId, personId, companyId, skillId);

        var jobSkill = await JobSkills.Where(x => x.OrganizationId == organizationId
                                                  && x.PersonId == personId
                                                  && x.CompanyId == companyId
                                                  && x.SkillId == skillId)
            .FirstOrDefaultAsync();

        if (jobSkill != null) return Result.Failed();

        jobSkill = new CompanySkill
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            PersonId = personId,
            CompanyId = companyId,
            SkillId = skillId
        };

        var changes = Repository.InsertOrUpdateGraph(jobSkill, true);
        if (changes > 0) return Result.Success();

        return Result.Failed();
    }

    public async Task<Result> DeleteCompanySkill(int organizationId, int personId, int companyId, int skillId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, ResumeId: {@resumeId}, SkillId: {@skillId}"),
            organizationId, personId, companyId, skillId);

        var jobSkill = await JobSkills.Where(x => x.OrganizationId == organizationId
                                                  && x.PersonId == personId
                                                  && x.CompanyId == companyId
                                                  && x.SkillId == skillId)
            .FirstOrDefaultAsync();

        if (jobSkill == null) return Result.Failed();

        jobSkill.ObjectState = ObjectState.Deleted;

        var changes = Repository.InsertOrUpdateGraph(jobSkill, true);
        if (changes > 0) return Result.Success();

        return Result.Failed();
    }
}
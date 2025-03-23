using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.Interfaces;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class CompanySkillService : BaseService<CompanySkill>, ICompanySkillService
{
    public CompanySkillService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

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
namespace ResumePro.Api.Interfaces;

public interface ICompanySkillsController
{
    Task<Result> AddCompanySkill(int personId, int companyId,
        int skillId);

    Task<Result> DeleteCompanySkill(int personId, int companyId,
        int skillId);
}
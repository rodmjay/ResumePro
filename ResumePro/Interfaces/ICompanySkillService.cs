#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Interfaces;

public interface ICompanySkillService : IService<CompanySkill>
{
    Task<Result> AddCompanySkill(int organizationId, int personId, int companyId, int skillId);
    Task<Result> DeleteCompanySkill(int organizationId, int personId, int companyId, int skillId);
}
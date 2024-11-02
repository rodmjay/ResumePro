#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;

namespace ResumePro.Shared.Interfaces;

public interface ICompanySkillsController
{
    Task<Result> AddCompanySkill(int personId, int companyId,
        int skillId);

    Task<Result> DeleteCompanySkill(int personId, int companyId,
        int skillId);
}
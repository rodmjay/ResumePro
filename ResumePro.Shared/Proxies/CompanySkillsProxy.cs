#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Proxies;

public sealed class CompanySkillsProxy : BaseProxy, ICompanySkillsController
{
    public CompanySkillsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result> AddCompanySkill(int personId, int companyId, int skillId)
    {
        return await DoPatch<Result>($"v1.0/people/{personId}/jobs/{companyId}/skills/{skillId}")
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteCompanySkill(int personId, int companyId, int skillId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/jobs/{companyId}/skills/{skillId}")
            .ConfigureAwait(false);
    }
}
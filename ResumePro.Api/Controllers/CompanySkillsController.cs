#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies/{companyId}/skills")]
public sealed class CompanySkillsController(IServiceProvider serviceProvider, ICompanySkillService service)
    : BaseController(serviceProvider), ICompanySkillsController
{
    [HttpPatch("{skillId}")]
    public async Task<Result> AddCompanySkill([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int skillId)
    {
        return await service.AddCompanySkill(OrganizationId, personId, companyId, skillId)
            .ConfigureAwait(false);
    }

    [HttpDelete("{skillId}")]
    public async Task<Result> DeleteCompanySkill([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int skillId)
    {
        return await service.DeleteCompanySkill(OrganizationId, personId, companyId, skillId)
            .ConfigureAwait(false);
    }
}
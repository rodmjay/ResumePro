#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs/{jobId}/skills")]
public sealed class JobSkillsController(IServiceProvider serviceProvider, IJobSkillService service)
    : BaseController(serviceProvider), IJobSkillsController
{
    [HttpPatch("{skillId}")]
    public async Task<Result> AddJobSkill([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int skillId)
    {
        return await service.AddJobSkill(OrganizationId, personId, jobId, skillId)
            .ConfigureAwait(false);
    }

    [HttpDelete("{skillId}")]
    public async Task<Result> DeleteJobSkill([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int skillId)
    {
        return await service.DeleteJobSkill(OrganizationId, personId, jobId, skillId)
            .ConfigureAwait(false);
    }
}
#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/jobs/{jobId}/skills")]
public sealed class JobSkillsController(IServiceProvider serviceProvider, IJobSkillService service)
    : BaseController(serviceProvider), IIndividualJobSkillsController
{
    [HttpPatch("{skillId}")]
    public async Task<Result> AddJobSkill([FromRoute] int jobId,
        [FromRoute] int skillId)
    {
        return await service.AddJobSkill(OrganizationId, UserId, jobId, skillId)
            .ConfigureAwait(false);
    }

    [HttpDelete("{skillId}")]
    public async Task<Result> DeleteJobSkill([FromRoute] int jobId,
        [FromRoute] int skillId)
    {
        return await service.DeleteJobSkill(OrganizationId, UserId, jobId, skillId)
            .ConfigureAwait(false);
    }
}
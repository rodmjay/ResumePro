#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/resume/{resumeId}/skills")]
public sealed class ResumeSkillsController(IServiceProvider serviceProvider, IResumeSkillService resumeSkillService)
    : BaseController(serviceProvider), IResumeSkillsController
{
    [HttpPatch("{skillId}")]
    public async Task<Result> AddResumeSkill([FromRoute] int personId, [FromRoute] int resumeId,
        [FromRoute] int skillId)
    {
        return await resumeSkillService.AddResumeSkill(OrganizationId, personId, resumeId, skillId)
            .ConfigureAwait(false);
    }

    [HttpDelete("{skillId}")]
    public async Task<Result> DeleteResumeSkill([FromRoute] int personId, [FromRoute] int resumeId,
        [FromRoute] int skillId)
    {
        return await resumeSkillService.DeleteResumeSkill(OrganizationId, personId, resumeId, skillId)
            .ConfigureAwait(false);
    }
}
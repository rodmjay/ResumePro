#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/resumes/{resumeId}/skills")]
public sealed class ResumeSkillsController(IServiceProvider serviceProvider, IResumeSkillService resumeSkillService)
    : BaseController(serviceProvider), IIndividualResumeSkillsController
{
    [HttpPatch("{skillId}")]
    public async Task<Result> AddResumeSkill([FromRoute] int resumeId,
        [FromRoute] int skillId)
    {
        return await resumeSkillService.AddResumeSkill(OrganizationId, UserId, resumeId, skillId)
            .ConfigureAwait(false);
    }

    [HttpDelete("{skillId}")]
    public async Task<Result> DeleteResumeSkill([FromRoute] int resumeId,
        [FromRoute] int skillId)
    {
        return await resumeSkillService.DeleteResumeSkill(OrganizationId, UserId, resumeId, skillId)
            .ConfigureAwait(false);
    }
}
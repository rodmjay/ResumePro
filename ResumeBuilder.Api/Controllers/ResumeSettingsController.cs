#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/resumes/{resumeId}/settings")]
public sealed class ResumeSettingsController(IServiceProvider serviceProvider, IResumeSettingsService service) 
    : BaseController(serviceProvider), IIndividualResumeSettingsController
{
    [HttpPut]
    public async Task<ActionResult<ResumeSettingsDto>> UpdateSettings([FromRoute] int resumeId,
        [FromBody] ResumeSettingsOptions options)
    {
        var result = await service.AddOrUpdateUpdateResumeSettings(OrganizationId, UserId, resumeId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }
}
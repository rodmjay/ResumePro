#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs/{jobId}/skills")]
public sealed class JobSkillsController(IServiceProvider serviceProvider, IJobSkillService service)
    : BaseController(serviceProvider)
{
    [HttpPost]
    public async Task<ActionResult<JobSkillDto>> CreateJobSkill([FromRoute] int personId, [FromRoute] int jobId)
    {
        throw new NotImplementedException();
    }
}
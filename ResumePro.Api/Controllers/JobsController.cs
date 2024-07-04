#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs")]
public class JobsController : BaseController
{
    private readonly IJobService _jobService;

    public JobsController(IServiceProvider serviceProvider, IJobService jobService) : base(serviceProvider)
    {
        _jobService = jobService;
    }

    [HttpPost]
    public async Task<ActionResult<JobDetails>> CreateJob([FromRoute] int personId, [FromBody] CreateJobOptions options)
    {
        var result = await _jobService.CreateJob(OrganizationId, personId, options);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.IsT1);
    }
}
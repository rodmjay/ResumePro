#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs")]
public sealed class JobsController(IServiceProvider serviceProvider, IJobService jobService)
    : BaseController(serviceProvider), IJobsController
{
    [HttpGet]
    public async Task<List<JobDetails>> GetJobs([FromRoute] int personId)
    {
        return await jobService.GetJobs<JobDetails>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpGet("{jobId}")]
    public async Task<JobDetails> GetJob([FromRoute] int personId, [FromRoute] int jobId)
    {
        return await jobService.GetJob<JobDetails>(OrganizationId, personId, jobId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<JobDetails>> CreateJob([FromRoute] int personId, [FromBody] JobOptions options)
    {
        var result = await jobService.CreateJob(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{jobId}")]
    public async Task<ActionResult<JobDetails>> UpdateJob([FromRoute] int personId, [FromRoute] int jobId,
        [FromBody] JobOptions options)
    {
        var result = await jobService.UpdateJob(OrganizationId, personId, jobId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{jobId}")]
    public async Task<Result> DeleteJob([FromRoute] int personId, [FromRoute] int jobId)
    {
        return await jobService.DeleteJob(OrganizationId, personId, jobId)
            .ConfigureAwait(false);
    }
}
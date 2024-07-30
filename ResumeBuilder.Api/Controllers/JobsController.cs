#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/jobs")]
public sealed class JobsController(IServiceProvider serviceProvider, IJobService jobService)
    : BaseController(serviceProvider), IIndividualJobsController
{
    [HttpGet]
    public async Task<List<JobDetails>> GetJobs()
    {
        return await jobService.GetJobs<JobDetails>(OrganizationId, UserId)
            .ConfigureAwait(false);
    }

    [HttpGet("{jobId}")]
    public async Task<JobDetails> GetJob([FromRoute] int jobId)
    {
        return await jobService.GetJob<JobDetails>(OrganizationId, UserId, jobId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<JobDetails>> CreateJob([FromBody] JobOptions options)
    {
        var result = await jobService.CreateJob(OrganizationId, UserId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{jobId}")]
    public async Task<ActionResult<JobDetails>> UpdateJob([FromRoute] int jobId,
        [FromBody] JobOptions options)
    {
        var result = await jobService.UpdateJob(OrganizationId, UserId, jobId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{jobId}")]
    public async Task<Result> DeleteJob([FromRoute] int jobId)
    {
        return await jobService.DeleteJob(OrganizationId, UserId, jobId)
            .ConfigureAwait(false);
    }
}
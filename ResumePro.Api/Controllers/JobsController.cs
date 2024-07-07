#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs")]
public class JobsController : BaseController
{
    private readonly IJobService _jobService;

    public JobsController(IServiceProvider serviceProvider, IJobService jobService) : base(serviceProvider)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<List<JobDetails>> GetJobs([FromRoute] int personId)
    {
        return await _jobService.GetJobs<JobDetails>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpGet("{jobId}")]
    public async Task<JobDetails> GetJob([FromRoute] int personId, [FromRoute] int jobId)
    {
        return await _jobService.GetJob<JobDetails>(OrganizationId, personId, jobId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<JobDetails>> CreateJob([FromRoute] int personId, [FromBody] JobOptions options)
    {
        var result = await _jobService.CreateJob(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{jobId}")]
    public async Task<ActionResult<JobDetails>> UpdateJob([FromRoute] int personId, [FromRoute] int jobId,
        [FromBody] JobOptions options)
    {
        var result = await _jobService.UpdateJob(OrganizationId, personId, jobId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{jobId}")]
    public async Task<Result> DeleteJob([FromRoute] int personId, [FromRoute] int jobId)
    {
        return await _jobService.DeleteJob(OrganizationId, personId, jobId)
            .ConfigureAwait(false);
    }
}
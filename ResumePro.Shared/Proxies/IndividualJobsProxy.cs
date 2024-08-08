#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualJobsProxy(HttpClient httpClient) : BaseProxy(httpClient), IIndividualJobsController
{
    public async Task<List<JobDetails>> GetJobs()
    {
        return await DoGet<List<JobDetails>>($"v1.0/jobs")
            .ConfigureAwait(false);
    }

    public async Task<JobDetails> GetJob(int jobId)
    {
        return await DoGet<JobDetails>($"v1.0/jobs/{jobId}")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<JobDetails>> CreateJob(JobOptions options)
    {
        return await DoPostActionResult<JobOptions, JobDetails>($"v1.0/jobs", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<JobDetails>> UpdateJob( int jobId, JobOptions options)
    {
        return await DoPutActionResult<JobOptions, JobDetails>($"v1.0/jobs/{jobId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteJob(int jobId)
    {
        return await DoDelete<Result>($"v1.0/jobs/{jobId}")
            .ConfigureAwait(false);
    }
}
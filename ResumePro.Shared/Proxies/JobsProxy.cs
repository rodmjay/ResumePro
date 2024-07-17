#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class JobsProxy(HttpClient httpClient) : BaseProxy(httpClient), IJobsController
{
    public async Task<List<JobDetails>> GetJobs(int personId)
    {
        return await DoGet<List<JobDetails>>($"v1.0/people/{personId}/jobs")
            .ConfigureAwait(false);
    }

    public async Task<JobDetails> GetJob(int personId, int jobId)
    {
        return await DoGet<JobDetails>($"v1.0/people/{personId}/jobs/{jobId}")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<JobDetails>> CreateJob(int personId, JobOptions options)
    {
        return await DoPostActionResult<JobOptions, JobDetails>($"v1.0/people/{personId}/jobs", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<JobDetails>> UpdateJob(int personId, int jobId, JobOptions options)
    {
        return await DoPutActionResult<JobOptions, JobDetails>($"v1.0/people/{personId}/jobs/{jobId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteJob(int personId, int jobId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/jobs/{jobId}")
            .ConfigureAwait(false);
    }
}